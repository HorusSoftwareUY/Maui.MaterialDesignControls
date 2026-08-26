using System.Diagnostics;
using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SkiaSharp;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

public partial class BenchmarkWavesViewModel : BaseViewModel
{
    // Aurora / "liquid" color waves: domain-warped fBm noise shaded with a
    // magenta/green/blue palette over black. Runs as an SkSL runtime effect.
    private const string ShaderSource = @"
uniform float  iTime;
uniform float2 iResolution;

// Sinless hash (Dave Hoskins). The classic fract(sin(dot(...)) * 43758.5)
// breaks on mobile GPUs: their hardware sin() loses precision for large
// arguments, degrading the noise into visible bands. This variant only uses
// fract/dot on values that stay in [0,1), so CPU and GPU render identically.
float hash(float2 p)
{
    float3 p3 = fract(float3(p.x, p.y, p.x) * 0.1031);
    p3 += dot(p3, p3.yzx + 33.33);
    return fract((p3.x + p3.y) * p3.z);
}

float noise(float2 p)
{
    float2 i = floor(p);
    float2 f = fract(p);
    float2 u = f * f * (3.0 - 2.0 * f);
    float a = hash(i);
    float b = hash(i + float2(1.0, 0.0));
    float c = hash(i + float2(0.0, 1.0));
    float d = hash(i + float2(1.0, 1.0));
    return mix(mix(a, b, u.x), mix(c, d, u.x), u.y);
}

float fbm(float2 p)
{
    float v = 0.0;
    float amp = 0.5;
    for (int i = 0; i < 4; i++)
    {
        v += amp * noise(p);
        p = p * 2.03 + float2(11.5, 7.7);
        amp *= 0.5;
    }
    return v;
}

half4 main(float2 fragCoord)
{
    float2 uv = fragCoord / iResolution;
    float2 p = uv;
    p.x *= iResolution.x / iResolution.y;
    p *= 2.6;

    float t = iTime * 0.12;

    // Domain warping: q and r bend the noise field so the color bands
    // flow like ink instead of scrolling uniformly.
    float2 q;
    q.x = fbm(p + float2(0.0, t));
    q.y = fbm(p + float2(5.2, 1.3) - t * 0.7);

    float2 r;
    r.x = fbm(p + 3.0 * q + float2(1.7, 9.2) + t * 0.4);
    r.y = fbm(p + 3.0 * q + float2(8.3, 2.8) - t * 0.3);

    float f = fbm(p + 3.0 * r);

    float3 col = float3(0.0);
    col = mix(col, float3(0.05, 0.15, 0.50), smoothstep(0.15, 0.75, q.y));
    col = mix(col, float3(0.05, 0.55, 0.25), smoothstep(0.30, 0.90, r.x));
    col = mix(col, float3(0.65, 0.08, 0.55), smoothstep(0.45, 0.95, f));
    col += float3(0.9, 0.3, 0.8) * pow(max(f - 0.55, 0.0) * 2.2, 2.0) * 0.6;

    // Keep plenty of black between crests so the waves read as ribbons.
    float lum = smoothstep(0.25, 0.65, f * 0.6 + q.y * 0.4);
    col *= lum * 1.6;

    float2 v = uv - 0.5;
    col *= 1.0 - dot(v, v) * 0.6;

    // Dither: hides banding on GPUs that evaluate SkSL in half precision.
    col += (hash(fragCoord + iTime) - 0.5) * (2.0 / 255.0);

    return half4(half3(clamp(col, 0.0, 1.0)), 1.0);
}";

    private static SKRuntimeEffect _effect;
    private static string _effectError;

    private SKSurface _offscreen;
    private SKImageInfo _offscreenInfo;
    private readonly Stopwatch _clock = new();
    private readonly Stopwatch _frameSw = new();

    // Rolling stats (same scheme as BenchmarkSkiaViewModel)
    private readonly Queue<double> _recentFps = new(120);
    private double _minFps = double.MaxValue;
    private long _frameCount;

    public override string Title => Models.Pages.BenchmarkWaves;

    [ObservableProperty] private bool _isRunning;

    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(ScaleButtonsEnabled))]
    private bool _useGpu = true;

    /// <summary>
    /// The render-scale buttons only apply to the CPU backend: the GPU path
    /// always draws at full resolution, so they are disabled there instead of
    /// looking active while doing nothing.
    /// </summary>
    public bool ScaleButtonsEnabled => !UseGpu;
    [ObservableProperty] private string _toggleButtonText = "Stop";
    [ObservableProperty] private string _backendButtonText = "Mode: GPU";
    [ObservableProperty] private string _fpsText    = "FPS: —";
    [ObservableProperty] private string _avgFpsText = "avg: —";
    [ObservableProperty] private string _minFpsText = "min: —";
    [ObservableProperty] private string _drawText   = "draw: —";
    [ObservableProperty] private string _resText    = "res: —";
    [ObservableProperty] private float  _renderScale = 1f / 3f;

    private string _lastResText;

    // Called by SKCanvasView/SKGLView.PaintSurface — returns true if we should
    // invalidate again (CPU path only; the GL view uses its native render loop).
    //
    // gpu == true  → the canvas is GPU-backed: draw the shader directly at full
    //                resolution; SkSL is compiled for the GPU and runs parallel.
    // gpu == false → CPU raster: SkSL is interpreted per pixel single-threaded,
    //                so render into a small offscreen surface (RenderScale) and
    //                upscale with linear filtering.
    public bool OnPaintSurface(SKCanvas canvas, int width, int height, bool gpu)
    {
        if (!IsRunning || gpu != UseGpu) return false;

        if (!EnsureEffect())
        {
            IsRunning = false;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ToggleButtonText = "Start";
                FpsText = "shader error";
            });
            return false;
        }

        var drawSw = Stopwatch.StartNew();

        if (gpu)
        {
            PaintShader(canvas, width, height);
            SetResText($"res: {width}×{height} (GPU)");
        }
        else
        {
            var rw = Math.Max(1, (int)(width * RenderScale));
            var rh = Math.Max(1, (int)(height * RenderScale));
            if (_offscreen == null || _offscreenInfo.Width != rw || _offscreenInfo.Height != rh)
            {
                _offscreen?.Dispose();
                _offscreenInfo = new SKImageInfo(rw, rh, SKImageInfo.PlatformColorType, SKAlphaType.Opaque);
                _offscreen = SKSurface.Create(_offscreenInfo);
            }

            PaintShader(_offscreen.Canvas, rw, rh);

            using var image = _offscreen.Snapshot();
            canvas.DrawImage(image, SKRect.Create(width, height), new SKSamplingOptions(SKFilterMode.Linear));
            SetResText($"res: {rw}×{rh} (CPU)");
        }

        drawSw.Stop();
        UpdateStats(drawSw.Elapsed.TotalMilliseconds);

        return true;
    }

    private void PaintShader(SKCanvas canvas, int width, int height)
    {
        var uniforms = new SKRuntimeEffectUniforms(_effect)
        {
            ["iTime"] = (float)_clock.Elapsed.TotalSeconds,
            ["iResolution"] = new[] { (float)width, (float)height },
        };

        using var shader = _effect.ToShader(uniforms);
        using var paint = new SKPaint { Shader = shader };
        canvas.DrawRect(SKRect.Create(width, height), paint);
    }

    private void SetResText(string text)
    {
        if (text == _lastResText) return;
        _lastResText = text;
        MainThread.BeginInvokeOnMainThread(() => ResText = text);
    }

    private static bool EnsureEffect()
    {
        if (_effect != null) return true;
        if (_effectError != null) return false;

        _effect = SKRuntimeEffect.CreateShader(ShaderSource, out var errors);
        if (_effect == null)
        {
            _effectError = errors ?? "unknown SkSL compile error";
            Logger.LogInfo($"BenchmarkWaves shader compile failed: {_effectError}");
            return false;
        }
        return true;
    }

    private void UpdateStats(double drawMs)
    {
        _frameCount++;
        double fps = 0;
        if (_frameSw.IsRunning)
        {
            var elapsed = _frameSw.Elapsed.TotalSeconds;
            if (elapsed > 0) fps = 1.0 / elapsed;
        }
        _frameSw.Restart();

        if (fps <= 0) return;

        _recentFps.Enqueue(fps);
        if (_recentFps.Count > 120) _recentFps.Dequeue();
        if (fps < _minFps) _minFps = fps;

        // Update UI labels every 10 frames to avoid flooding. The GL view paints
        // on its native render thread, so marshal the property sets to the UI thread.
        if (_frameCount % 10 == 0)
        {
            var avg = _recentFps.Average();
            var min = _minFps < double.MaxValue ? _minFps.ToString("F0") : "—";
            MainThread.BeginInvokeOnMainThread(() =>
            {
                FpsText    = $"FPS: {fps:F0}";
                AvgFpsText = $"avg: {avg:F0}";
                MinFpsText = $"min: {min}";
                DrawText   = $"draw: {drawMs:F2} ms";
            });
        }
    }

    [ICommand]
    private void ToggleBackend()
    {
        UseGpu = !UseGpu;
        BackendButtonText = UseGpu ? "Mode: GPU" : "Mode: CPU";
        ResetStats();
    }

    [ICommand]
    private void ToggleRunning()
    {
        if (IsRunning)
        {
            Stop();
        }
        else
        {
            Start();
        }
    }

    [ICommand]
    private void SetScale(float scale)
    {
        RenderScale = scale;
        ResetStats();
    }

    public void Start()
    {
        if (IsRunning) return;
        ResetStats();
        _clock.Start();
        ToggleButtonText = "Stop";
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
        ToggleButtonText = "Start";
        _clock.Stop();
        _frameSw.Stop();
    }

    public override void Disappearing()
    {
        base.Disappearing();
        Stop();
        // Page instances are singletons; release the offscreen surface while
        // the page is not visible.
        _offscreen?.Dispose();
        _offscreen = null;
        _offscreenInfo = default;
    }

    private void ResetStats()
    {
        _recentFps.Clear();
        _minFps     = double.MaxValue;
        _frameCount = 0;
        _frameSw.Reset();
        _lastResText = null;
        FpsText    = "FPS: —";
        AvgFpsText = "avg: —";
        MinFpsText = "min: —";
        DrawText   = "draw: —";
    }
}
