using CoreGraphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class CustomCheckboxHandler
{
    public new static void MapForeground(ICheckBoxHandler handler, ICheckBox check)
    {
        if (check is CustomCheckBox customCheckBox && handler.PlatformView != null && handler.PlatformView is CustomCheckboxView iosCheckboxView)
        {
            iosCheckboxView.Color = customCheckBox.Color;
            iosCheckboxView.TickColor = customCheckBox.TickColor;
            iosCheckboxView.SetNeedsDisplay();
        }
    }

    protected override Microsoft.Maui.Platform.MauiCheckBox CreatePlatformView()
    {
        return new CustomCheckboxView();
    }
}

internal class CustomCheckboxView : Microsoft.Maui.Platform.MauiCheckBox
{
    public Color Color { get; set; }
    public Color TickColor { get; set; }

    public override void Draw(CGRect rect)
    {
        ClearSublayers();

        using var context = UIGraphics.GetCurrentContext();
        var rect2 = this.Bounds;
        var cornerRadius = 4.0f; 

        var roundedRectPath = UIBezierPath.FromRoundedRect(rect2, cornerRadius);
        UIColor.FromCGColor(Colors.Transparent.ToCGColor()).SetFill();
        roundedRectPath.Fill();

        if (this.IsEnabled || !this.IsChecked)
        {
            context.SetLineWidth(2.0f);
            UIColor.FromCGColor(Color.ToCGColor()).SetStroke();

            var borderRect = new CGRect(
                rect2.X + 2,
                rect2.Y + 1,
                rect2.Width - 4,
                rect2.Height - 4
            );

            var borderPath = UIBezierPath.FromRoundedRect(borderRect, 2f);
            context.AddPath(borderPath.CGPath);
            context.StrokePath();
        }

        if (this.IsChecked)
        {
            UIColor.FromCGColor(Color.ToCGColor()).SetFill();
            roundedRectPath.Fill();

            UIColor.FromCGColor(TickColor.ToCGColor()).SetStroke();
            var path = new CGPath();
            var inset = rect2.Inset((nfloat)(rect2.Width * 0.2), (nfloat)(rect2.Height * 0.2));
            path.MoveToPoint(inset.X, inset.Y + inset.Height * 0.5f);
            path.AddLineToPoint(inset.X + inset.Width * 0.3f, inset.Y + inset.Height * 0.8f);
            path.AddLineToPoint(inset.X + inset.Width * 0.9f, inset.Y);

            context.AddPath(path);
            context.SetLineWidth(2);
            context.StrokePath();
        }
    }

    private void ClearSublayers()
    {
        if (Layer?.Sublayers != null)
        {
            foreach (var sublayer in Layer.Sublayers)
            {
                sublayer.Opacity = 0;
            }
        }
    }
}

