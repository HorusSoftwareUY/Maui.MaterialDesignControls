using System.Diagnostics;
using System.Runtime.InteropServices;
using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SkiaSharp;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

/// <summary>
/// "Fluid ink": auto-splats of rainbow ink that swirl and dissipate, in the
/// style of the classic WebGL fluid simulation (ink preset, autoSplat mode).
///
/// Unlike the other Skia pages this one is *stateful*: a feedback (ping-pong)
/// pair of offscreen surfaces holds the dye texture. Each frame:
///   1. Advection pass — an SkSL effect samples the previous dye texture (child
///      shader) displaced along a curl-noise velocity field, with dissipation.
///      Curl noise gives divergence-free, fluid-looking motion without solving
///      Navier-Stokes.
///   2. Splats — managed C# schedules ink drops (deterministic seed) drawn as
///      radial gradients with additive blending.
///   3. Present — the dye texture is drawn to screen, plus an optional bloom
///      pass (blurred copy, Plus blend) in GPU mode.
///
/// As a benchmark it stresses what no other page does: texture feedback
/// sampling and Skia's surface pipeline, with the same phase instrumentation
/// (update / uniforms / draw, percentiles, GC) as the Liquid page.
/// </summary>
public partial class BenchmarkFluidViewModel : BaseViewModel
{
    private const double BenchSeconds = 30;
    private const float GpuSimScale = 0.5f;   // dye texture: half screen res on GPU
    private const float CpuSimScale = 0.25f;  // quarter res on CPU raster

    private const string AdvectSource = @"
uniform shader dye;
uniform float2 simRes;
uniform float  iTime;
uniform float  swirlPx;   // velocity scale in sim px/second
uniform float  fadeK;     // per-frame dissipation multiplier
uniform float  dtU;       // seconds since previous frame (clamped)

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

float fbm2(float2 p)
{
    return 0.667 * noise(p) + 0.333 * noise(p * 2.03 + float2(11.5, 7.7));
}

// Curl of an evolving scalar noise field at the given spatial scale:
// divergence-free velocity, i.e. fluid-looking motion for free.
float2 curlAt(float2 uvA, float scale, float2 drift)
{
    float2 np = uvA * scale + drift;
    float e = 0.05;
    float nl = fbm2(np - float2(e, 0.0));
    float nr = fbm2(np + float2(e, 0.0));
    float nd = fbm2(np - float2(0.0, e));
    float nu = fbm2(np + float2(0.0, e));
    return float2(nu - nd, -(nr - nl)) / (2.0 * e);
}

half4 main(float2 c)
{
    float2 uv = c / simRes;
    float aspect = simRes.x / simRes.y;
    float2 uvA = float2(uv.x * aspect, uv.y);

    // Two turbulence scales: a broad slow flow carries the ink around, and a
    // finer, faster layer shreds it into the wispy filaments of real ink.
    float2 vel = curlAt(uvA, 3.0, float2(iTime * 0.05, -iTime * 0.03))
               + 0.45 * curlAt(uvA, 9.0, float2(-iTime * 0.09, iTime * 0.06));

    // Semi-Lagrangian backtrace: fetch the dye where this pixel came from.
    float2 src = c - vel * swirlPx * dtU;
    half4 d = dye.eval(src);
    return d * fadeK;
}";

    private static SKRuntimeEffect _advect;
    private static string _advectError;

    // Reference look (brightness 0.15, glowCut 0.6): aged ink is darkened to
    // smoke, and only values above the cut contribute to bloom.
    // Built lazily rather than in static field initializers: these are cosmetic,
    // and a throwing type initializer would poison the whole ViewModel type.
    private static SKColorFilter _darken;
    private static SKColorFilter _glowCut;
    private static bool _filtersInitialized;

    private static void EnsureFilters()
    {
        if (_filtersInitialized) return;
        _filtersInitialized = true;

        try
        {
            _darken = SKColorFilter.CreateColorMatrix(new float[]
            {
                0.55f, 0, 0, 0, 0,
                0, 0.55f, 0, 0, 0,
                0, 0, 0.55f, 0, 0,
                0, 0, 0, 1, 0,
            });

            // Per-channel transfer curve: zero below the threshold, then ramp
            // to full. CreateTable requires all four tables, so alpha gets an
            // identity ramp. The counters must be int — a byte one can never
            // reach 256, it wraps at 255 and spins forever.
            const int threshold = 140;
            var alpha = new byte[256];
            var ramp = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                alpha[i] = (byte)i;
                if (i >= threshold)
                    ramp[i] = (byte)((i - threshold) * 255 / (255 - threshold));
            }
            _glowCut = SKColorFilter.CreateTable(alpha, ramp, ramp, ramp);
        }
        catch (Exception ex)
        {
            // Cosmetic only: a null filter just means "no filter", so the page
            // keeps working without the darkening / glow cut.
            Logger.LogException(ex);
        }
    }

    // Ping-pong dye surfaces. On GPU they live on the view's GRContext so the
    // feedback loop never leaves the GPU; on CPU they are raster surfaces.
    private SKSurface _dyeA, _dyeB;
    private SKImageInfo _dyeInfo;
    private GRRecordingContext _dyeContext;

    private readonly Stopwatch _clock = new();
    private readonly Stopwatch _frameSw = new();
    private double _lastFrameTime;
    private float _simTime;
    private double _splatAccumulator;
    private Random _rng = new(42);
    private float _hue;

    // Rolling stats + benchmark capture (same scheme as the other Skia benchmark pages)
    private readonly List<double> _recentWorkMs = new(240);
    private long _frameCount;
    private bool _benchActive;
    private double _benchStart;
    private readonly List<double> _benchFrame = new(4096);
    private readonly List<double> _benchUpdate = new(4096);
    private readonly List<double> _benchUniforms = new(4096);
    private readonly List<double> _benchDraw = new(4096);
    private int _gc0, _gc1, _gc2;
    private long _allocStart;
    private string _lastResText;

    public override string Title => Models.Pages.BenchmarkFluid;

    [ObservableProperty] private bool _isRunning;
    [ObservableProperty] private bool _useGpu = true;
    [ObservableProperty] private string _toggleButtonText = "Stop";
    [ObservableProperty] private string _backendButtonText = "Mode: GPU";
    [ObservableProperty] private string _fpsText    = "FPS: —";
    [ObservableProperty] private string _p95Text    = "p95: —";
    [ObservableProperty] private string _updateText = "upd: —";
    [ObservableProperty] private string _drawText   = "draw: —";
    [ObservableProperty] private string _resText    = "res: —";
    [ObservableProperty] private string _benchText  = "";

    // Sliders (defaults tuned to the reference: ink style, autoSplat 0.6, glow 0.8)
    [ObservableProperty] private float _swirl = 0.5f;      // 0..1 → flow strength
    [ObservableProperty] private float _fade = 0.75f;      // 0..1 → dye lifetime
    [ObservableProperty] private float _splatRate = 0.6f;  // 0..1 → splats per second
    [ObservableProperty] private float _glow = 0.8f;       // 0..1 → bloom intensity

    #region Rendering

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

        EnsureFilters();

        var now = _clock.Elapsed.TotalSeconds;
        var dt = (float)Math.Clamp(_lastFrameTime > 0 ? now - _lastFrameTime : 1.0 / 60, 0.0, 1.0 / 30);
        _lastFrameTime = now;
        _simTime += dt;

        // Sim surfaces (ping-pong pair), recreated on size/backend/context change.
        var scale = gpu ? GpuSimScale : CpuSimScale;
        var rw = Math.Max(1, (int)(width * scale));
        var rh = Math.Max(1, (int)(height * scale));
        var ctx = gpu ? canvas.Context as GRRecordingContext : null;
        if (_dyeA == null || _dyeInfo.Width != rw || _dyeInfo.Height != rh || _dyeContext != ctx)
        {
            DisposeSimSurfaces();
            _dyeInfo = new SKImageInfo(rw, rh, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            _dyeContext = ctx;
            _dyeA = ctx != null ? SKSurface.Create(ctx, false, _dyeInfo) : SKSurface.Create(_dyeInfo);
            _dyeB = ctx != null ? SKSurface.Create(ctx, false, _dyeInfo) : SKSurface.Create(_dyeInfo);
            _dyeA.Canvas.Clear(SKColors.Black);
            _dyeB.Canvas.Clear(SKColors.Black);
            SetResText($"res: {rw}×{rh} ({(gpu ? "GPU" : "CPU")})");
        }

        // Phase 1 — managed update: splat scheduling (deterministic via seed).
        var updSw = Stopwatch.StartNew();
        var splats = ScheduleSplats(dt, rw, rh);
        updSw.Stop();

        // Phase 2 — uniforms + child shader wiring.
        var uniSw = Stopwatch.StartNew();
        using var prevImage = _dyeA.Snapshot();
        using var prevShader = prevImage.ToShader(
            SKShaderTileMode.Clamp, SKShaderTileMode.Clamp,
            new SKSamplingOptions(SKFilterMode.Linear));

        var uniforms = new SKRuntimeEffectUniforms(_advect)
        {
            ["simRes"] = new[] { (float)rw, (float)rh },
            ["iTime"] = _simTime,
            ["swirlPx"] = 15f + Swirl * 220f,
            ["fadeK"] = MathF.Exp(-dt / (0.4f + Fade * 4f)),
            ["dtU"] = dt,
        };
        var children = new SKRuntimeEffectChildren(_advect) { ["dye"] = prevShader };
        uniSw.Stop();

        // Phase 3 — native draw: advect into B, stamp splats, present, swap.
        var drawSw = Stopwatch.StartNew();
        using (var advShader = _advect.ToShader(uniforms, children))
        using (var advPaint = new SKPaint { Shader = advShader, BlendMode = SKBlendMode.Src })
        {
            _dyeB.Canvas.DrawRect(SKRect.Create(rw, rh), advPaint);
        }

        DrawSplats(_dyeB.Canvas, splats);

        using (var dyeImage = _dyeB.Snapshot())
        {
            canvas.Clear(SKColors.Black);
            var dest = SKRect.Create(width, height);
            var sampling = new SKSamplingOptions(SKFilterMode.Linear);

            // Base pass, darkened: in the reference (brightness 0.15) the aged
            // ink reads as muted smoke — only fresh splats are allowed to shine.
            using (var basePaint = new SKPaint { ColorFilter = _darken })
            {
                canvas.DrawImage(dyeImage, dest, sampling, basePaint);
            }

            // Bloom with a glow cut: the table filter zeroes everything below
            // the threshold, so only bright fresh cores glow — aged smoke does
            // not. GPU only: a full-screen blur per frame on CPU raster would
            // dwarf the simulation itself.
            if (gpu && Glow > 0.05f)
            {
                using var bloomPaint = new SKPaint
                {
                    BlendMode = SKBlendMode.Plus,
                    ColorFilter = _glowCut,
                    ImageFilter = SKImageFilter.CreateBlur(6f + Glow * 18f, 6f + Glow * 18f),
                    Color = SKColors.White.WithAlpha((byte)(Glow * 200)),
                };
                canvas.DrawImage(dyeImage, dest, sampling, bloomPaint);
            }
        }

        (_dyeA, _dyeB) = (_dyeB, _dyeA);
        drawSw.Stop();

        UpdateStats(updSw.Elapsed.TotalMilliseconds,
                    uniSw.Elapsed.TotalMilliseconds,
                    drawSw.Elapsed.TotalMilliseconds,
                    now);

        return true;
    }

    private readonly record struct Splat(float X, float Y, float Radius, float DirX, float DirY, SKColor Color);

    private List<Splat> ScheduleSplats(float dt, int rw, int rh)
    {
        var splats = new List<Splat>();
        var perSecond = 0.2f + SplatRate * 3.8f;
        _splatAccumulator += dt * perSecond;

        while (_splatAccumulator >= 1)
        {
            _splatAccumulator -= 1;

            // Rainbow hue cycling, like the reference's rainbow mode.
            _hue = (_hue + 47f) % 360f;   // golden-ish step: consecutive splats contrast
            var color = SKColor.FromHsv(_hue, 85f, 100f);

            var angle = (float)(_rng.NextDouble() * Math.PI * 2);
            splats.Add(new Splat(
                X: rw * (0.12f + 0.76f * (float)_rng.NextDouble()),
                Y: rh * (0.12f + 0.76f * (float)_rng.NextDouble()),
                Radius: Math.Min(rw, rh) * (0.06f + 0.05f * (float)_rng.NextDouble()),
                DirX: MathF.Cos(angle),
                DirY: MathF.Sin(angle),
                Color: color));
        }
        return splats;
    }

    private static void DrawSplats(SKCanvas canvas, List<Splat> splats)
    {
        foreach (var s in splats)
        {
            // Three overlapping drops along a random direction fake the injection
            // momentum of the reference sim (our velocity field is procedural,
            // so splats cannot push the flow — but the streak reads the same).
            for (var i = 0; i < 3; i++)
            {
                var t = i / 2f;
                var cx = s.X + s.DirX * s.Radius * 1.2f * t;
                var cy = s.Y + s.DirY * s.Radius * 1.2f * t;
                var r = s.Radius * (1f - 0.25f * t);
                using var paint = new SKPaint
                {
                    BlendMode = SKBlendMode.Plus,
                    Shader = SKShader.CreateRadialGradient(
                        new SKPoint(cx, cy), r,
                        new[] { s.Color.WithAlpha(235), s.Color.WithAlpha(0) },
                        SKShaderTileMode.Clamp),
                };
                canvas.DrawCircle(cx, cy, r, paint);
            }
        }
    }

    private static bool EnsureEffect()
    {
        if (_advect != null) return true;
        if (_advectError != null) return false;

        _advect = SKRuntimeEffect.CreateShader(AdvectSource, out var errors);
        if (_advect == null)
        {
            _advectError = errors ?? "unknown SkSL compile error";
            Logger.LogInfo($"BenchmarkFluid shader compile failed: {_advectError}");
            return false;
        }
        return true;
    }

    private void SetResText(string text)
    {
        if (text == _lastResText) return;
        _lastResText = text;
        MainThread.BeginInvokeOnMainThread(() => ResText = text);
    }

    private void DisposeSimSurfaces()
    {
        _dyeA?.Dispose(); _dyeA = null;
        _dyeB?.Dispose(); _dyeB = null;
        _dyeContext = null;
        _dyeInfo = default;
    }

    #endregion

    #region Stats & benchmark (same scheme as the other Skia benchmark pages)

    private void UpdateStats(double updMs, double uniMs, double drawMs, double now)
    {
        _frameCount++;
        double fps = 0;
        if (_frameSw.IsRunning)
        {
            var e = _frameSw.Elapsed.TotalSeconds;
            if (e > 0) fps = 1.0 / e;
        }
        _frameSw.Restart();

        var workMs = updMs + uniMs + drawMs;
        _recentWorkMs.Add(workMs);
        if (_recentWorkMs.Count > 240) _recentWorkMs.RemoveAt(0);

        if (_benchActive)
        {
            _benchFrame.Add(workMs);
            _benchUpdate.Add(updMs);
            _benchUniforms.Add(uniMs);
            _benchDraw.Add(drawMs);
            if (now - _benchStart >= BenchSeconds)
                CompleteBenchmark();
        }

        if (_frameCount % 15 == 0)
        {
            var p95 = Percentile(_recentWorkMs, 0.95);
            var benchLeft = _benchActive ? $"bench: {BenchSeconds - (now - _benchStart):F0}s left" : null;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                FpsText    = $"FPS: {fps:F0}";
                P95Text    = $"p95: {p95:F1} ms";
                UpdateText = $"upd: {updMs:F2} ms";
                DrawText   = $"draw: {drawMs:F2} ms";
                if (benchLeft != null) BenchText = benchLeft;
            });
        }
    }

    private static double Percentile(List<double> source, double p)
    {
        if (source.Count == 0) return 0;
        var sorted = source.ToArray();
        Array.Sort(sorted);
        var idx = Math.Clamp((int)Math.Ceiling(p * sorted.Length) - 1, 0, sorted.Length - 1);
        return sorted[idx];
    }

    [ICommand]
    private void RunBenchmark()
    {
        if (_benchActive) return;

        // Deterministic re-run: fixed seed and a fresh dye texture.
        _rng = new Random(42);
        _hue = 0;
        _splatAccumulator = 0;
        _simTime = 0;
        _dyeA?.Canvas.Clear(SKColors.Black);
        _dyeB?.Canvas.Clear(SKColors.Black);

        _benchFrame.Clear(); _benchUpdate.Clear(); _benchUniforms.Clear(); _benchDraw.Clear();
        _gc0 = GC.CollectionCount(0);
        _gc1 = GC.CollectionCount(1);
        _gc2 = GC.CollectionCount(2);
        _allocStart = GC.GetTotalAllocatedBytes();
        _benchStart = _clock.Elapsed.TotalSeconds;
        _benchActive = true;
        BenchText = $"bench: {BenchSeconds:F0}s left";

        if (!IsRunning) Start();
    }

    private void CompleteBenchmark()
    {
        _benchActive = false;

        var gc0 = GC.CollectionCount(0) - _gc0;
        var gc1 = GC.CollectionCount(1) - _gc1;
        var gc2 = GC.CollectionCount(2) - _gc2;
        var allocBytes = GC.GetTotalAllocatedBytes() - _allocStart;
        var frames = _benchFrame.Count;
        var avgFps = frames / BenchSeconds;

        var runtime = RuntimeInformation.FrameworkDescription;
        var flavor = AppInfo.Current.PackageName.EndsWith(".mono") ? "mono" : "coreclr";
        var backend = UseGpu ? "gpu" : "cpu";

        var json = FormattableString.Invariant($$"""
{
  "benchmark": "fluid-ink",
  "flavor": "{{flavor}}",
  "runtime": "{{runtime}}",
  "backend": "{{backend}}",
  "swirl": {{Swirl:F2}},
  "fade": {{Fade:F2}},
  "splatRate": {{SplatRate:F2}},
  "glow": {{Glow:F2}},
  "device": "{{DeviceInfo.Current.Manufacturer}} {{DeviceInfo.Current.Model}}",
  "os": "{{DeviceInfo.Current.Platform}} {{DeviceInfo.Current.VersionString}}",
  "durationSeconds": {{BenchSeconds:F0}},
  "frames": {{frames}},
  "avgFps": {{avgFps:F1}},
  "workMs": { "p50": {{Percentile(_benchFrame, 0.50):F3}}, "p95": {{Percentile(_benchFrame, 0.95):F3}}, "p99": {{Percentile(_benchFrame, 0.99):F3}} },
  "updateMs": { "p50": {{Percentile(_benchUpdate, 0.50):F3}}, "p95": {{Percentile(_benchUpdate, 0.95):F3}}, "p99": {{Percentile(_benchUpdate, 0.99):F3}} },
  "uniformsMs": { "p50": {{Percentile(_benchUniforms, 0.50):F3}}, "p95": {{Percentile(_benchUniforms, 0.95):F3}}, "p99": {{Percentile(_benchUniforms, 0.99):F3}} },
  "drawMs": { "p50": {{Percentile(_benchDraw, 0.50):F3}}, "p95": {{Percentile(_benchDraw, 0.95):F3}}, "p99": {{Percentile(_benchDraw, 0.99):F3}} },
  "gc": { "gen0": {{gc0}}, "gen1": {{gc1}}, "gen2": {{gc2}}, "allocatedBytes": {{allocBytes}} }
}
""");

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {
                var file = Path.Combine(FileSystem.CacheDirectory,
                    $"fluid-bench-{flavor}-{backend}-{DateTime.Now:yyyyMMdd-HHmmss}.json");
                await File.WriteAllTextAsync(file, json);
                BenchText = $"bench done: {avgFps:F0} fps · p99 {Percentile(_benchFrame, 0.99):F1} ms · GC {gc0}/{gc1}/{gc2}";

                await Share.Default.RequestAsync(new ShareFileRequest
                {
                    Title = "Fluid benchmark result",
                    File = new ShareFile(file),
                });
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                BenchText = "bench done (share failed, see log)";
            }
        });
    }

    #endregion

    #region Lifecycle & commands

    [ICommand]
    private void ToggleRunning()
    {
        if (IsRunning) Stop();
        else Start();
    }

    [ICommand]
    private void ToggleBackend()
    {
        UseGpu = !UseGpu;
        BackendButtonText = UseGpu ? "Mode: GPU" : "Mode: CPU";
        DisposeSimSurfaces();   // sim res / context differ per backend
        ResetStats();
    }

    public void Start()
    {
        if (IsRunning) return;
        ResetStats();
        _clock.Start();
        _lastFrameTime = 0;
        // A little opening burst so the screen is never empty on entry.
        _splatAccumulator = 3;
        ToggleButtonText = "Stop";
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
        _benchActive = false;
        ToggleButtonText = "Start";
        _clock.Stop();
        _frameSw.Stop();
    }

    public override void Disappearing()
    {
        base.Disappearing();
        Stop();
        DisposeSimSurfaces();
    }

    private void ResetStats()
    {
        _recentWorkMs.Clear();
        _frameCount = 0;
        _frameSw.Reset();
        _lastResText = null;
        FpsText    = "FPS: —";
        P95Text    = "p95: —";
        UpdateText = "upd: —";
        DrawText   = "draw: —";
    }

    #endregion
}
