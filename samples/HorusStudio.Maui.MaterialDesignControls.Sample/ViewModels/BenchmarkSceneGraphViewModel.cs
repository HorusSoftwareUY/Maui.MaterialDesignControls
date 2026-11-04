using System.Diagnostics;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SkiaSharp;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

/// <summary>
/// "Scene graph": a retained hierarchy of sprites whose world transforms are
/// recomputed every frame, then drawn.
///
/// The point of this page is the <see cref="UsePolymorphic"/> toggle. It runs
/// the *same* simulation, producing the same pixels, through two code shapes:
///
///   • Polymorphic — a tree of node objects behind an abstract base. Every node
///     costs a virtual call on update and another on draw.
///   • Structs — the identical maths over flat arrays in a monomorphic loop,
///     with a jump table instead of indirect dispatch.
///
/// That is the variable under test. Tiered JIT with dynamic PGO can inline and
/// devirtualise the first shape by observing which type actually shows up;
/// Mono's AOT cannot, because it has to commit at build time. Raw float loops
/// (the second shape) compile well under both, so the gap there should be much
/// smaller. Comparing the two toggles on one runtime, and then across runtimes,
/// is what makes the point.
///
/// Deliberate constraints, so the number means what it claims:
///   • GPU work is one textured quad per sprite, identical in both modes.
///   • Zero allocations per frame on the measured path — the sprite atlas and
///     every array are built once. A benchmark that allocates measures the GC,
///     and the two runtimes have very different collectors.
///   • Milliseconds of managed CPU per phase are the headline, not FPS: vsync
///     pins both runtimes at 60 and hides whatever headroom is left.
/// </summary>
public partial class BenchmarkSceneGraphViewModel : BaseViewModel
{
    private const int MaxNodes = 20_000;
    private const int SpriteVariants = 8;
    private const int Seed = 42;
    private const int StatsEveryFrames = 15;
    private const float SpriteSize = 32f;
    private const float HalfSprite = SpriteSize / 2f;
    private const float RadToDeg = 180f / MathF.PI;

    /// <summary>Children per group. Shallow and wide: a deep tree would spend
    /// its time in recursion overhead rather than in dispatch.</summary>
    private const int Fanout = 16;

    private static readonly int[] LevelSizes = { 1_000, 5_000, 10_000, 20_000 };

    private static readonly SKColor[] Palette =
    {
        new(0x7E, 0x57, 0xC2), new(0x26, 0xA6, 0x9A), new(0xFF, 0xA7, 0x26), new(0xEF, 0x53, 0x50),
        new(0x42, 0xA5, 0xF5), new(0x66, 0xBB, 0x6A), new(0xEC, 0x40, 0x7A), new(0xFF, 0xEE, 0x58),
    };

    // ---------------------------------------------------------------- scene

    /// <summary>
    /// Flat description of the scene, generated once from a fixed seed. Both
    /// modes are built from this, so they simulate byte-identical input.
    /// </summary>
    private readonly int[] _parent = new int[MaxNodes];
    private readonly float[] _localX = new float[MaxNodes];
    private readonly float[] _localY = new float[MaxNodes];
    private readonly float[] _spin = new float[MaxNodes];
    private readonly float[] _scale = new float[MaxNodes];
    private readonly float[] _phase = new float[MaxNodes];
    private readonly byte[] _kind = new byte[MaxNodes];
    private readonly byte[] _variant = new byte[MaxNodes];

    // World transforms, written by whichever mode is active.
    private readonly float[] _worldX = new float[MaxNodes];
    private readonly float[] _worldY = new float[MaxNodes];
    private readonly float[] _worldR = new float[MaxNodes];
    private readonly float[] _worldS = new float[MaxNodes];
    private readonly bool[] _visible = new bool[MaxNodes];

    private SceneNode[] _nodes = Array.Empty<SceneNode>();
    private SKPaint _spritePaint;
    private SKPaint _edgePaint;

    // Every sprite reaches the GPU through a single DrawAtlas call, so the
    // per-frame native crossings stay constant instead of scaling with node
    // count. Both arrays are sized once and refilled in place.
    private SKImage _atlas;
    private SKImage _atlasTexture;
    private bool _textureUploadFailed;
    private readonly SKRect[] _variantRects = new SKRect[SpriteVariants];
    // Sized to the exact sprite count when the scene is built: DrawAtlas takes
    // whole arrays, so a spare tail would draw stale instances.
    private SKRotationScaleMatrix[] _xforms = Array.Empty<SKRotationScaleMatrix>();
    private SKRect[] _texs = Array.Empty<SKRect>();

    private int _count = LevelSizes[1];
    private bool _sceneBuilt;

    // ------------------------------------------------------------- timing

    private static readonly double TicksToMs = 1000.0 / Stopwatch.Frequency;

    private long _lastFrameTicks;

    /// <summary>
    /// Origin for the animation clock. Time must be measured from here rather
    /// than from the raw timestamp: on a device up for days the absolute value
    /// is ~2e5 seconds, where a float's epsilon (~0.024 s) is larger than a
    /// 60fps frame step — the clock would stop advancing and the scene would sit
    /// still, faster frame rates freezing sooner than slow ones.
    /// </summary>
    private long _startTicks;
    private long _frameCount;
    private double _updMsAccum, _drawMsAccum, _fpsAccum;
    private int _statsFrames;
    private double _minFps = double.MaxValue;

    public override string Title => Models.Pages.BenchmarkSceneGraph;

    [ObservableProperty] private bool _isRunning = true;
    [ObservableProperty] private bool _useGpu = true;

    /// <summary>The variable under test. See the class remarks.</summary>
    [ObservableProperty] private bool _usePolymorphic = true;

    [ObservableProperty] private string _toggleButtonText = "Stop";
    [ObservableProperty] private string _backendButtonText = "Mode: GPU";
    [ObservableProperty] private string _shapeButtonText = "Nodes: interfaces";
    [ObservableProperty] private string _countText = "5.000 nodes";
    [ObservableProperty] private string _fpsText = "FPS: —";
    [ObservableProperty] private string _minFpsText = "min: —";
    [ObservableProperty] private string _updateText = "update: —";
    [ObservableProperty] private string _drawText = "draw: —";

    public override void Initialize() { }

    // ------------------------------------------------------------ commands

    [ICommand]
    private void ToggleRunning()
    {
        IsRunning = !IsRunning;
        ToggleButtonText = IsRunning ? "Stop" : "Start";
        if (IsRunning) ResetStats();
    }

    [ICommand]
    private void ToggleBackend()
    {
        UseGpu = !UseGpu;
        BackendButtonText = UseGpu ? "Mode: GPU" : "Mode: CPU";
        ResetStats();
    }

    [ICommand]
    private void ToggleShape()
    {
        UsePolymorphic = !UsePolymorphic;
        ShapeButtonText = UsePolymorphic ? "Nodes: interfaces" : "Nodes: structs";
        ResetStats();
    }

    [ICommand]
    private void SetCount(int count)
    {
        _count = Math.Clamp(count, LevelSizes[0], MaxNodes);
        CountText = $"{_count:N0} nodes";
        _sceneBuilt = false;
        ResetStats();
    }

    // --------------------------------------------------------------- frame

    /// <summary>
    /// Returns true while the CPU view should keep asking for frames. The GL
    /// view drives itself through its native render loop.
    /// </summary>
    public bool OnPaintSurface(SKCanvas canvas, int width, int height, bool gpu,
                               GRContext context = null)
    {
        canvas.Clear(new SKColor(0x0E, 0x0E, 0x12));

        if (!IsRunning || width <= 0 || height <= 0)
            return IsRunning;

        EnsureAtlas();
        if (!_sceneBuilt) BuildScene(width, height);

        var now = Stopwatch.GetTimestamp();
        var frameMs = _lastFrameTicks == 0 ? 16.7 : (now - _lastFrameTicks) * TicksToMs;
        _lastFrameTicks = now;

        if (_startTicks == 0) _startTicks = now;
        var time = (float)((now - _startTicks) * TicksToMs * 0.001);

        var t0 = Stopwatch.GetTimestamp();
        if (UsePolymorphic) UpdatePolymorphic(time, width, height);
        else UpdateStructs(time, width, height);
        var t1 = Stopwatch.GetTimestamp();

        // Edges make the hierarchy legible. They read the same flat world arrays
        // both modes write, so the work is byte-identical between them — which is
        // why it sits outside the measured draw phase: including it would only
        // dilute the ratio under comparison.
        DrawEdges(canvas);

        var t2 = Stopwatch.GetTimestamp();
        if (UsePolymorphic) EmitPolymorphic(); else EmitStructs();
        var t3 = Stopwatch.GetTimestamp();

        // One call for every sprite. The GPU cost is therefore constant and
        // identical between modes, which is what lets `draw` above measure only
        // the managed emit loop — the half that actually differs. Off-screen
        // instances are clipped by the GPU rather than culled here, so the
        // instance count stays fixed per level.
        if (_xforms.Length > 0)
            canvas.DrawAtlas(AtlasFor(gpu, context), _texs, _xforms, _spritePaint);

        Accumulate((t1 - t0) * TicksToMs, (t3 - t2) * TicksToMs, frameMs);

        return IsRunning && !gpu;
    }

    /// <summary>
    /// Rolling stats, flushed to the UI every <see cref="StatsEveryFrames"/>
    /// frames. Formatting allocates strings, so it stays off the per-frame path.
    /// </summary>
    private void Accumulate(double updMs, double drawMs, double frameMs)
    {
        _frameCount++;
        _updMsAccum += updMs;
        _drawMsAccum += drawMs;

        var fps = frameMs > 0.01 ? 1000.0 / frameMs : 0;
        _fpsAccum += fps;

        // Ignore the first few frames: surface creation and scene build land there.
        if (_frameCount > 10 && fps > 0 && fps < _minFps) _minFps = fps;

        if (++_statsFrames < StatsEveryFrames) return;

        var n = _statsFrames;
        var fpsText = $"FPS: {_fpsAccum / n:F0}";
        var minText = _minFps < double.MaxValue ? $"min: {_minFps:F0}" : "min: —";
        var updText = $"update: {_updMsAccum / n:F2} ms";
        var drwText = $"draw: {_drawMsAccum / n:F2} ms";

        // The GL backend paints on its own render thread, where assigning to a
        // bound property never reaches the UI. Hopping here — once every
        // StatsEveryFrames, never per frame — keeps the readout alive on both
        // backends without touching the measured path.
        MainThread.BeginInvokeOnMainThread(() =>
        {
            FpsText = fpsText;
            MinFpsText = minText;
            UpdateText = updText;
            DrawText = drwText;
        });

        _statsFrames = 0;
        _updMsAccum = _drawMsAccum = _fpsAccum = 0;
    }

    private void ResetStats()
    {
        _frameCount = 0;
        _statsFrames = 0;
        _updMsAccum = _drawMsAccum = _fpsAccum = 0;
        _minFps = double.MaxValue;
        _lastFrameTicks = 0;
        _startTicks = 0;
    }

    // ------------------------------------------------------- polymorphic

    private void UpdatePolymorphic(float time, int width, int height)
    {
        var nodes = _nodes;
        for (var i = 0; i < _count; i++)
        {
            var p = _parent[i];
            nodes[i].Update(time, i, p < 0 ? 0f : _worldX[p], p < 0 ? 0f : _worldY[p],
                            p < 0 ? 0f : _worldR[p], p < 0 ? 1f : _worldS[p],
                            _worldX, _worldY, _worldR, _worldS, _visible, width, height);
        }
    }

    /// <summary>One virtual call per node to fill its atlas slot.</summary>
    private int EmitPolymorphic()
    {
        var n = 0;
        var nodes = _nodes;
        for (var i = 0; i < _count; i++)
            if (nodes[i].Emit(i, _worldX, _worldY, _worldR, _worldS,
                              _variant, _variantRects, _xforms, _texs, n)) n++;
        return n;
    }

    /// <summary>
    /// Base of the polymorphic path. Every call site below is virtual on
    /// purpose — that indirection is the thing being measured.
    /// </summary>
    private abstract class SceneNode
    {
        public abstract void Update(float time, int i, float px, float py, float pr, float ps,
                                    float[] wx, float[] wy, float[] wr, float[] ws, bool[] vis,
                                    int width, int height);

        public virtual bool Emit(int i, float[] wx, float[] wy, float[] wr, float[] ws,
                                 byte[] variant, SKRect[] variantRects,
                                 SKRotationScaleMatrix[] xforms, SKRect[] texs, int slot)
        {
            xforms[slot] = SKRotationScaleMatrix.CreateDegrees(
                ws[i], wr[i] * RadToDeg, wx[i], wy[i], HalfSprite, HalfSprite);
            texs[slot] = variantRects[variant[i]];
            return true;
        }
    }

    /// <summary>Carries children but draws nothing — the transform-only node
    /// every real scene graph has.</summary>
    private sealed class GroupNode : SceneNode
    {
        private readonly float _lx, _ly, _spin;

        public GroupNode(float lx, float ly, float spin) { _lx = lx; _ly = ly; _spin = spin; }

        public override void Update(float time, int i, float px, float py, float pr, float ps,
                                    float[] wx, float[] wy, float[] wr, float[] ws, bool[] vis,
                                    int width, int height)
        {
            var r = pr + _spin * time;
            var c = MathF.Cos(pr);
            var s = MathF.Sin(pr);
            wx[i] = px + (_lx * c - _ly * s) * ps;
            wy[i] = py + (_lx * s + _ly * c) * ps;
            wr[i] = r;
            ws[i] = ps;
            vis[i] = false;
        }

        public override bool Emit(int i, float[] wx, float[] wy, float[] wr, float[] ws,
                                  byte[] variant, SKRect[] variantRects,
                                  SKRotationScaleMatrix[] xforms, SKRect[] texs, int slot)
            => false;
    }

    private sealed class SpinNode : SceneNode
    {
        private readonly float _lx, _ly, _spin, _scale;

        public SpinNode(float lx, float ly, float spin, float scale)
        { _lx = lx; _ly = ly; _spin = spin; _scale = scale; }

        public override void Update(float time, int i, float px, float py, float pr, float ps,
                                    float[] wx, float[] wy, float[] wr, float[] ws, bool[] vis,
                                    int width, int height)
        {
            var c = MathF.Cos(pr);
            var s = MathF.Sin(pr);
            var x = px + (_lx * c - _ly * s) * ps;
            var y = py + (_lx * s + _ly * c) * ps;
            wx[i] = x; wy[i] = y;
            wr[i] = pr + _spin * time;
            ws[i] = ps * _scale;
            vis[i] = x > -40f && y > -40f && x < width + 40f && y < height + 40f;
        }
    }

    private sealed class PulseNode : SceneNode
    {
        private readonly float _lx, _ly, _spin, _scale, _phase;

        public PulseNode(float lx, float ly, float spin, float scale, float phase)
        { _lx = lx; _ly = ly; _spin = spin; _scale = scale; _phase = phase; }

        public override void Update(float time, int i, float px, float py, float pr, float ps,
                                    float[] wx, float[] wy, float[] wr, float[] ws, bool[] vis,
                                    int width, int height)
        {
            var c = MathF.Cos(pr);
            var s = MathF.Sin(pr);
            var x = px + (_lx * c - _ly * s) * ps;
            var y = py + (_lx * s + _ly * c) * ps;
            wx[i] = x; wy[i] = y;
            wr[i] = pr + _spin * time;
            ws[i] = ps * _scale * (0.75f + 0.25f * MathF.Sin(time * 2f + _phase));
            vis[i] = x > -40f && y > -40f && x < width + 40f && y < height + 40f;
        }
    }

    private sealed class OrbitNode : SceneNode
    {
        private readonly float _radius, _speed, _spin, _scale, _phase;

        public OrbitNode(float radius, float speed, float spin, float scale, float phase)
        { _radius = radius; _speed = speed; _spin = spin; _scale = scale; _phase = phase; }

        public override void Update(float time, int i, float px, float py, float pr, float ps,
                                    float[] wx, float[] wy, float[] wr, float[] ws, bool[] vis,
                                    int width, int height)
        {
            var a = pr + time * _speed + _phase;
            var x = px + MathF.Cos(a) * _radius * ps;
            var y = py + MathF.Sin(a) * _radius * ps;
            wx[i] = x; wy[i] = y;
            wr[i] = a + _spin * time;
            ws[i] = ps * _scale;
            vis[i] = x > -40f && y > -40f && x < width + 40f && y < height + 40f;
        }
    }

    // ----------------------------------------------------------- structs

    /// <summary>
    /// Same maths, same output, no indirect dispatch: one loop over flat arrays
    /// with a switch the JIT can turn into a jump table.
    /// </summary>
    private void UpdateStructs(float time, int width, int height)
    {
        for (var i = 0; i < _count; i++)
        {
            var p = _parent[i];
            float px = 0f, py = 0f, pr = 0f, ps = 1f;
            if (p >= 0) { px = _worldX[p]; py = _worldY[p]; pr = _worldR[p]; ps = _worldS[p]; }

            float x, y, r, s;
            var kind = _kind[i];

            if (kind == KindOrbit)
            {
                var a = pr + time * _spin[i] + _phase[i];
                x = px + MathF.Cos(a) * _localX[i] * ps;
                y = py + MathF.Sin(a) * _localX[i] * ps;
                r = a + _spin[i] * time;
                s = ps * _scale[i];
            }
            else
            {
                var c = MathF.Cos(pr);
                var sn = MathF.Sin(pr);
                x = px + (_localX[i] * c - _localY[i] * sn) * ps;
                y = py + (_localX[i] * sn + _localY[i] * c) * ps;
                r = pr + _spin[i] * time;
                s = kind == KindPulse
                    ? ps * _scale[i] * (0.75f + 0.25f * MathF.Sin(time * 2f + _phase[i]))
                    : ps * _scale[i];
                if (kind == KindGroup) s = ps;
            }

            _worldX[i] = x; _worldY[i] = y; _worldR[i] = r; _worldS[i] = s;
            _visible[i] = kind != KindGroup
                          && x > -40f && y > -40f && x < width + 40f && y < height + 40f;
        }
    }

    /// <summary>Same slots, filled by a flat loop with no indirect call.</summary>
    private int EmitStructs()
    {
        var n = 0;
        for (var i = 0; i < _count; i++)
        {
            if (_kind[i] == KindGroup) continue;
            _xforms[n] = SKRotationScaleMatrix.CreateDegrees(
                _worldS[i], _worldR[i] * RadToDeg, _worldX[i], _worldY[i], HalfSprite, HalfSprite);
            _texs[n] = _variantRects[_variant[i]];
            n++;
        }
        return n;
    }

    private const byte KindGroup = 0;
    private const byte KindSpin = 1;
    private const byte KindPulse = 2;
    private const byte KindOrbit = 3;

    // ------------------------------------------------------------- shared

    /// <summary>Sampling is fixed and shared so the GPU cost per sprite is the
    /// same in both modes.</summary>
    private static readonly SKSamplingOptions Sampling = new(SKFilterMode.Linear);

    /// <summary>
    /// One line per node back to its parent, so the tree is visible as a tree.
    /// Skips edges whose child is culled, which keeps the cost proportional to
    /// what is actually on screen.
    /// </summary>
    private void DrawEdges(SKCanvas canvas)
    {
        var paint = _edgePaint;
        if (paint is null) return;

        for (var i = 0; i < _count; i++)
        {
            if (!_visible[i]) continue;

            var p = _parent[i];
            if (p < 0) continue;

            canvas.DrawLine(_worldX[p], _worldY[p], _worldX[i], _worldY[i], paint);
        }
    }

    /// <summary>
    /// The atlas is built on a raster surface. Handing that straight to
    /// DrawAtlas on a GL canvas stalls the render thread on this device, so the
    /// GPU path draws an uploaded copy instead. If the upload is unavailable the
    /// raster image is reused — slower, but it keeps rendering.
    /// </summary>
    private SKImage AtlasFor(bool gpu, GRContext context)
    {
        if (!gpu || context is null || _textureUploadFailed) return _atlas;

        if (_atlasTexture is null)
        {
            try
            {
                _atlasTexture = _atlas.ToTextureImage(context);
            }
            catch
            {
                _textureUploadFailed = true;
            }
        }

        return _atlasTexture ?? _atlas;
    }

    /// <summary>
    /// One texture holding the eight sprite variants side by side. DrawAtlas
    /// picks a variant per instance through its source rect, so a single call
    /// covers every sprite on screen.
    /// </summary>
    private void EnsureAtlas()
    {
        if (_atlas is not null) return;

        _spritePaint = new SKPaint { IsAntialias = true };
        _edgePaint = new SKPaint
        {
            IsAntialias = true,
            IsStroke = true,
            StrokeWidth = 2.2f,
            // Brighter and wider than a hairline: at this pixel density a 1px
            // stroke at low alpha reads as noise. Antialiasing stays on, so the
            // extra weight costs sharpness nothing.
            Color = new SKColor(0xB8, 0xC6, 0xDE, 0xC8),
        };

        const int cell = (int)SpriteSize;
        var info = new SKImageInfo(cell * SpriteVariants, cell, SKColorType.Rgba8888, SKAlphaType.Premul);
        using var surface = SKSurface.Create(info);
        var c = surface.Canvas;
        c.Clear(SKColors.Transparent);

        for (var v = 0; v < SpriteVariants; v++)
        {
            var ox = v * cell;

            using var fill = new SKPaint { IsAntialias = true, Color = Palette[v], IsStroke = false };
            c.DrawRoundRect(new SKRect(ox + 4, 4, ox + 28, 28), 7, 7, fill);

            using var ring = new SKPaint
            {
                IsAntialias = true,
                Color = Palette[v].WithAlpha(0x66),
                IsStroke = true,
                StrokeWidth = 2,
            };
            c.DrawRoundRect(new SKRect(ox + 2, 2, ox + 30, 30), 9, 9, ring);

            _variantRects[v] = new SKRect(ox, 0, ox + cell, cell);
        }

        _atlas = surface.Snapshot();
    }

    /// <summary>
    /// Builds both representations from one deterministic description, so the
    /// toggle changes only the code shape and never the workload.
    /// </summary>
    private void BuildScene(int width, int height)
    {
        var rng = new Random(Seed);
        var nodes = new SceneNode[_count];

        for (var i = 0; i < _count; i++)
        {
            // Three levels per block of Fanout: an invisible anchor, a ring of
            // branches hanging off it, and leaves hanging off those. Every
            // parent index lands before its child, which is what lets the update
            // resolve world transforms in a single forward pass.
            var slot = i % Fanout;
            var blockStart = i - slot;
            var isGroup = slot == 0;

            _parent[i] = isGroup
                ? -1
                : slot <= 5
                    ? blockStart
                    : blockStart + 1 + slot % 5;

            var spin = (float)(rng.NextDouble() * 1.6 - 0.8);
            var scale = (float)(0.5 + rng.NextDouble() * 0.9);
            var phase = (float)(rng.NextDouble() * MathF.PI * 2);

            if (isGroup)
            {
                var gx = (float)(rng.NextDouble() * width);
                var gy = (float)(rng.NextDouble() * height);
                _kind[i] = KindGroup;
                _localX[i] = gx; _localY[i] = gy;
                _spin[i] = spin * 0.25f; _scale[i] = 1f; _phase[i] = phase;
                nodes[i] = new GroupNode(gx, gy, spin * 0.25f);
            }
            else
            {
                var pick = i % 3;
                var lx = (float)(rng.NextDouble() * 120 - 60);
                var ly = (float)(rng.NextDouble() * 120 - 60);
                _spin[i] = spin; _scale[i] = scale; _phase[i] = phase;
                _variant[i] = (byte)(i % SpriteVariants);

                if (pick == 0)
                {
                    _kind[i] = KindSpin; _localX[i] = lx; _localY[i] = ly;
                    nodes[i] = new SpinNode(lx, ly, spin, scale);
                }
                else if (pick == 1)
                {
                    _kind[i] = KindPulse; _localX[i] = lx; _localY[i] = ly;
                    nodes[i] = new PulseNode(lx, ly, spin, scale, phase);
                }
                else
                {
                    var radius = 20f + (float)rng.NextDouble() * 60f;
                    _kind[i] = KindOrbit; _localX[i] = radius; _localY[i] = 0f;
                    nodes[i] = new OrbitNode(radius, spin, spin * 2f, scale, phase);
                }
            }
        }

        var spriteCount = 0;
        for (var i = 0; i < _count; i++)
            if (_kind[i] != KindGroup) spriteCount++;

        if (_xforms.Length != spriteCount)
        {
            _xforms = new SKRotationScaleMatrix[spriteCount];
            _texs = new SKRect[spriteCount];
        }

        _nodes = nodes;
        _sceneBuilt = true;
        ResetStats();
    }

    // ------------------------------------------------------------ lifetime

    public void Start()
    {
        IsRunning = true;
        ToggleButtonText = "Stop";
        ResetStats();
    }

    public void Stop()
    {
        IsRunning = false;
        ToggleButtonText = "Start";
    }

    public override void Disappearing()
    {
        Stop();
        base.Disappearing();
    }
}
