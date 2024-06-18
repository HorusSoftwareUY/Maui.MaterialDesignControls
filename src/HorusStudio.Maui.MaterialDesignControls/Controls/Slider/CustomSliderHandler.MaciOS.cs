using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using CoreGraphics;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomSliderHandler : ISliderHandler
{
    public static async void MapDesignProperties(ISliderHandler handler, ISlider slider)
    {
        if (slider is CustomSlider customSlider && handler.PlatformView is UISlider control)
        {
            control.MaximumTrackTintColor = customSlider.MaximumTrackColor.ToPlatform();
            control.MinimumTrackTintColor = customSlider.MinimumTrackColor.ToPlatform();

            control.UserInteractionEnabled = customSlider.UserInteractionEnabled;

            control.SetTrackHeight(customSlider.TrackHeight, customSlider.MinimumTrackColor.ToPlatform(), customSlider.MaximumTrackColor.ToPlatform(), customSlider.TrackCornerRadius);

            if (customSlider.ThumbImageSource == null)
            {
                var thumbWidth = 4; 
                var thumbHeight = 44;
                UIGraphics.BeginImageContext(new CGSize(thumbWidth, thumbHeight));
                var thumbContext = UIGraphics.GetCurrentContext();
                thumbContext.SetFillColor(customSlider.ThumbColor.ToPlatform().CGColor);
                thumbContext.FillRect(new CGRect(0, 0, thumbWidth, thumbHeight));
                var thumbImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();
                control.SetThumbImage(thumbImage, UIControlState.Normal);
                control.SetThumbImage(thumbImage, UIControlState.Highlighted);
            }
            else
            {
                var image = await customSlider.ThumbImageSource.ToUIImageAsync();
                if (image != null)
                {
                    control.SetThumbImage(image, UIControlState.Normal);
                    control.SetThumbImage(image, UIControlState.Highlighted);
                }
            }
        }
    }
}

public static class UISliderExtensions
{
    public static void SetTrackHeight(this UISlider slider, double height, UIColor minimumTrackColor, UIColor maximumTrackColor, int cornerRadius)
    {
        var radii = new CGSize(cornerRadius / 3, cornerRadius / 3);

        UIGraphics.BeginImageContextWithOptions(new CGSize(slider.Bounds.Width, height), false, 0);
        var context = UIGraphics.GetCurrentContext();
        var minPath = UIBezierPath.FromRoundedRect(new CGRect(0, 0, slider.Bounds.Width, height), UIRectCorner.AllCorners, radii);
        minimumTrackColor.SetFill();
        minPath.Fill();
        UIImage minTrackImage = UIGraphics.GetImageFromCurrentImageContext();
        UIGraphics.EndImageContext();

        UIGraphics.BeginImageContextWithOptions(new CGSize(slider.Bounds.Width, height), false, 0);
        context = UIGraphics.GetCurrentContext();
        var maxPath = UIBezierPath.FromRoundedRect(new CGRect(0, 0, slider.Bounds.Width, height), UIRectCorner.AllCorners, radii);
        maximumTrackColor.SetFill();
        maxPath.Fill();
        UIImage maxTrackImage = UIGraphics.GetImageFromCurrentImageContext();
        UIGraphics.EndImageContext();

        slider.SetMinTrackImage(minTrackImage, UIControlState.Normal);
        slider.SetMaxTrackImage(maxTrackImage, UIControlState.Normal);
    }
}


