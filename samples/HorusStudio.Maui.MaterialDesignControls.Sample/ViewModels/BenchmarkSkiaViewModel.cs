using System.Diagnostics;
using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SkiaSharp;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

public partial class BenchmarkSkiaViewModel : BaseViewModel
{
    private static readonly int[] ParticleCounts = { 50, 100, 200, 500 };
    private static readonly SKColor[] Palette =
    {
        new(0x4C, 0xAF, 0x50), new(0x21, 0x96, 0xF3), new(0xFF, 0x98, 0x00),
        new(0xE9, 0x1E, 0x63), new(0x9C, 0x27, 0xB0), new(0x00, 0xBC, 0xD4),
        new(0xFF, 0x57, 0x22), new(0x8B, 0xC3, 0x4A),
    };

    private List<Particle> _particles = new();
    private readonly Random _rng = new(42);
    private readonly Stopwatch _frameSw = new();

    // Rolling stats
    private readonly Queue<double> _recentFps = new(120);
    private double _minFps = double.MaxValue;
    private long _frameCount;

    public override string Title => Models.Pages.BenchmarkSkia;

    [ObservableProperty] private bool _isRunning;
    [ObservableProperty] private string _toggleButtonText = "Start";
    [ObservableProperty] private string _fpsText     = "FPS: —";
    [ObservableProperty] private string _avgFpsText  = "avg: —";
    [ObservableProperty] private string _minFpsText  = "min: —";
    [ObservableProperty] private string _physicsText = "physics: —";
    [ObservableProperty] private string _drawText    = "draw: —";
    [ObservableProperty] private string _countText   = "0 particles";
    [ObservableProperty] private int    _activeCount = 100;

    // Read-only snapshot for the canvas thread
    public IReadOnlyList<Particle> Particles => _particles;

    public override void Initialize()
    {
        SpawnParticles(ActiveCount, 1, 1);
    }

    // Called by SKCanvasView.PaintSurface — returns true if we should invalidate again
    public bool OnPaintSurface(SKCanvas canvas, int width, int height)
    {
        if (!IsRunning) return false;

        // Physics
        var physSw = Stopwatch.StartNew();
        foreach (var p in _particles)
            p.Update(width, height);
        physSw.Stop();

        // Draw
        var drawSw = Stopwatch.StartNew();
        canvas.Clear(SKColors.Black);

        using var paint = new SKPaint { IsAntialias = true };

        foreach (var p in _particles)
        {
            var trail = p.Trail.ToArray();
            for (var i = 0; i < trail.Length; i++)
            {
                var alpha = (byte)(255 * (i + 1) / (float)(trail.Length + 1));
                paint.Color = p.Color.WithAlpha(alpha);
                var r = p.Radius * (i + 1) / (float)(trail.Length + 1);
                canvas.DrawCircle(trail[i], r, paint);
            }
            paint.Color = p.Color;
            canvas.DrawCircle(p.Position, p.Radius, paint);
        }
        drawSw.Stop();

        // Stats
        _frameCount++;
        double fps = 0;
        if (_frameSw.IsRunning)
        {
            var elapsed = _frameSw.Elapsed.TotalSeconds;
            if (elapsed > 0) fps = 1.0 / elapsed;
        }
        _frameSw.Restart();

        if (fps > 0)
        {
            _recentFps.Enqueue(fps);
            if (_recentFps.Count > 120) _recentFps.Dequeue();
            if (fps < _minFps) _minFps = fps;

            // Update UI labels every 10 frames to avoid flooding
            if (_frameCount % 10 == 0)
            {
                var avg = _recentFps.Average();
                FpsText     = $"FPS: {fps:F0}";
                AvgFpsText  = $"avg: {avg:F0}";
                MinFpsText  = $"min: {(_minFps < double.MaxValue ? _minFps.ToString("F0") : "—")}";
                PhysicsText = $"physics: {physSw.Elapsed.TotalMilliseconds:F2} ms";
                DrawText    = $"draw: {drawSw.Elapsed.TotalMilliseconds:F2} ms";
            }
        }

        return true;
    }

    [ICommand]
    private void ToggleRunning()
    {
        if (IsRunning)
        {
            IsRunning = false;
            ToggleButtonText = "Start";
            _frameSw.Stop();
        }
        else
        {
            ResetStats();
            ToggleButtonText = "Stop";
            IsRunning = true;
        }
    }

    [ICommand]
    private void SetCount(int count)
    {
        ActiveCount = count;
        SpawnParticles(count, 1, 1);
        ResetStats();
        CountText = $"{count} particles";
    }

    private void SpawnParticles(int count, float canvasW, float canvasH)
    {
        // Use a reasonable virtual space so particles spread out even before first paint
        var w = canvasW > 1 ? canvasW : 400f;
        var h = canvasH > 1 ? canvasH : 800f;

        var list = new List<Particle>(count);
        for (var i = 0; i < count; i++)
        {
            var radius = _rng.NextSingle() * 6 + 4;  // 4–10 px
            var x  = (float)(_rng.NextDouble() * (w - radius * 2) + radius);
            var y  = (float)(_rng.NextDouble() * (h - radius * 2) + radius);
            var vx = (float)((_rng.NextDouble() - 0.5) * 6);
            var vy = (float)((_rng.NextDouble() - 0.5) * 6);
            var color = Palette[i % Palette.Length];
            list.Add(new Particle(x, y, vx, vy, color, radius));
        }
        _particles = list;
        CountText = $"{count} particles";
    }

    private void ResetStats()
    {
        _recentFps.Clear();
        _minFps    = double.MaxValue;
        _frameCount = 0;
        FpsText     = "FPS: —";
        AvgFpsText  = "avg: —";
        MinFpsText  = "min: —";
        PhysicsText = "physics: —";
        DrawText    = "draw: —";
    }
}
