using SkiaSharp;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Models;

public class Particle
{
    private const int TrailLength = 20;

    public SKPoint Position { get; set; }
    public SKPoint Velocity { get; set; }
    public SKColor Color { get; }
    public float Radius { get; }
    public Queue<SKPoint> Trail { get; } = new(TrailLength + 1);

    public Particle(float x, float y, float vx, float vy, SKColor color, float radius)
    {
        Position = new SKPoint(x, y);
        Velocity = new SKPoint(vx, vy);
        Color    = color;
        Radius   = radius;
    }

    public void Update(float width, float height)
    {
        Trail.Enqueue(Position);
        if (Trail.Count > TrailLength)
            Trail.Dequeue();

        var nx = Position.X + Velocity.X;
        var ny = Position.Y + Velocity.Y;

        var vx = Velocity.X;
        var vy = Velocity.Y;

        if (nx - Radius < 0 || nx + Radius > width)  vx = -vx;
        if (ny - Radius < 0 || ny + Radius > height) vy = -vy;

        Velocity = new SKPoint(vx, vy);
        Position = new SKPoint(
            Math.Clamp(nx, Radius, width  - Radius),
            Math.Clamp(ny, Radius, height - Radius));
    }
}
