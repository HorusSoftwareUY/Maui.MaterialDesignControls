using CoreGraphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class CustomRadioButtonHandler 
{
    public new static void MapStrokeColor(IRadioButtonHandler handler, IRadioButton radioButton)
    {
        if (radioButton is CustomRadioButton customRadioButton && customRadioButton.StrokeColor != null)
        {
            if (handler.PlatformView != null && handler.PlatformView is CustomRadioButtonView iosRadioButtonView)
            {
                iosRadioButtonView.StrokeColor = customRadioButton.StrokeColor;
                iosRadioButtonView.RadioButton = customRadioButton;
                iosRadioButtonView.SetNeedsDisplay();
            }
        }
    }

    protected override Microsoft.Maui.Platform.ContentView CreatePlatformView()
    {
        return new CustomRadioButtonView();
    }
}

internal class CustomRadioButtonView : Microsoft.Maui.Platform.ContentView
{
    public Color StrokeColor { get; set; }
    public CustomRadioButton RadioButton { get; set; }

    private bool _isSubLayerCleared = false;

    public override void Draw(CGRect rect)
    {
        base.Draw(rect);

        if (RadioButton is not null && RadioButton.IsControlTemplateByDefault)
        {
            if (!_isSubLayerCleared)
            {
                ClearSublayers();
                _isSubLayerCleared = true;
            }

            using var context = UIGraphics.GetCurrentContext();
            var lineWidth = 2f;
            context.SetStrokeColor(StrokeColor.ToCGColor());
            context.SetLineWidth(lineWidth);

            var radius = (float)Math.Min(rect.Width, rect.Height) / 4 - lineWidth / 4;
            var centerX = rect.GetMidX();
            var centerY = rect.GetMidY();

            var radiusCheck = radius / 2;

            context.AddArc(centerX, centerY, radius, 0, (nfloat)(2 * Math.PI), true);
            context.DrawPath(CGPathDrawingMode.Stroke);

            if (RadioButton.IsChecked)
            {
                context.SetFillColor(StrokeColor.ToCGColor());
                context.AddArc(centerX, centerY, radiusCheck, 0, (nfloat)(2 * Math.PI), true);
                context.DrawPath(CGPathDrawingMode.FillStroke);
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
