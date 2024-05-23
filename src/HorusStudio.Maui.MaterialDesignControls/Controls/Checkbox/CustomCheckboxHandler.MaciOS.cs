using CoreGraphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;
partial class CustomCheckboxHandler
{
    public new static void MapForeground(ICheckBoxHandler handler, ICheckBox check)
    {
        if (check is CustomCheckBox customCheckBox && handler.PlatformView != null && handler.PlatformView is CustomCheckboxView iosCheckboxView)
        {
            iosCheckboxView.Color = customCheckBox.Color;
            iosCheckboxView.CheckColor = customCheckBox.CheckColor;
            iosCheckboxView.Checkbox = customCheckBox;
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
    public Color CheckColor { get; set; }
    public CheckBox Checkbox { get; set; }


    public override void Draw(CGRect rect)
    {
        ClearSublayers();

        using var context = UIGraphics.GetCurrentContext();
        if (context != null)
        {
            var rect2 = this.Bounds;
            UIColor.FromCGColor(Color.FromArgb("#00000000").ToCGColor()).SetFill();
            context.FillRect(rect2);

            context.SetLineWidth(2.0f);
            UIColor.FromCGColor(Color.ToCGColor()).SetStroke();
            context.StrokeRect(rect);

            if (this.IsChecked)
            {
                UIColor.FromCGColor(Color.ToCGColor()).SetFill();
                context.FillRect(rect2);

                UIColor.FromCGColor(CheckColor.ToCGColor()).SetStroke();
                var path = new CGPath();
                var inset = rect.Inset((nfloat)(rect.Width * 0.2), (nfloat)(rect.Height * 0.2));
                path.MoveToPoint(inset.X, inset.Y + inset.Height * 0.8f);
                path.AddLineToPoint(inset.X + inset.Width * 0.4f, inset.Y + inset.Height);
                path.AddLineToPoint(inset.X + inset.Width, inset.Y);
                context.AddPath(path);
                context.SetLineWidth(2);
                context.StrokePath();
            }
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

