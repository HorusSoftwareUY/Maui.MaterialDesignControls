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

            control.SetTrackDesign(customSlider.TrackHeight, customSlider.MinimumTrackColor.ToPlatform(), customSlider.MaximumTrackColor.ToPlatform(), customSlider.TrackCornerRadius);

            if (customSlider.ThumbImageSource is null)
            {
                nfloat padding = 5;
                nfloat thumbWidthWithPadding = customSlider.ThumbWidth + 2 * padding;
                nfloat thumbHeightWithPadding = customSlider.ThumbHeight + 2 * padding;

                UIGraphics.BeginImageContextWithOptions(new CGSize(thumbWidthWithPadding, thumbHeightWithPadding), false, 0.0f);
                var thumbContext = UIGraphics.GetCurrentContext();

                UIColor backgroundColor = customSlider.ThumbBackgroundColor.ToPlatform();
                thumbContext.SetFillColor(backgroundColor.CGColor);
                thumbContext.FillRect(new CGRect(0, 0, thumbWidthWithPadding, thumbHeightWithPadding));

                thumbContext.SetFillColor(customSlider.ThumbColor.ToPlatform().CGColor);
                UIBezierPath thumbPath = UIBezierPath.FromRoundedRect(new CGRect(padding, padding, customSlider.ThumbWidth, customSlider.ThumbHeight), customSlider.TrackCornerRadius);
                thumbContext.AddPath(thumbPath.CGPath);
                thumbContext.FillPath();

                var thumbImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();

                control.SetThumbImage(thumbImage, UIControlState.Normal);
                control.SetThumbImage(thumbImage, UIControlState.Highlighted);
            }
            else
            {
                var image = await customSlider.ThumbImageSource.ToUIImageAsync();

                if(image is not null)
                {
                    CGSize thumbSize = new CGSize(customSlider.ThumbWidth, customSlider.ThumbHeight);

                    UIColor backgroundColor = customSlider.ThumbBackgroundColor.ToPlatform();

                    UIGraphics.BeginImageContextWithOptions(thumbSize, false, 0.0f);

                    CGContext context = UIGraphics.GetCurrentContext();
                    context.SetFillColor(backgroundColor.CGColor);
                    context.FillRect(new CGRect(0, 0, thumbSize.Width, thumbSize.Height));

                    image.Draw(new CGRect(0, 0, thumbSize.Width, thumbSize.Height));
                    UIImage resizedImage = UIGraphics.GetImageFromCurrentImageContext();
                    UIGraphics.EndImageContext();

                    control.SetThumbImage(resizedImage, UIControlState.Normal);
                    control.SetThumbImage(resizedImage, UIControlState.Highlighted);
                }
            }
        }
    }
}

public static class UISliderExtensions
{
    public static void SetTrackDesign(this UISlider slider, double height, UIColor minimumTrackColor, UIColor maximumTrackColor, int cornerRadius)
    {
        UIGraphics.BeginImageContextWithOptions(new CGSize(slider.Bounds.Width, height), false, 0);
        var minPath = UIBezierPath.FromRoundedRect(new CGRect(0, 0, slider.Bounds.Width, height), cornerRadius / 3);
        minimumTrackColor.SetFill();
        minPath.Fill();
        UIImage minTrackImage = UIGraphics.GetImageFromCurrentImageContext();
        UIGraphics.EndImageContext();

        UIGraphics.BeginImageContextWithOptions(new CGSize(slider.Bounds.Width, height), false, 0);
        var maxPath = UIBezierPath.FromRoundedRect(new CGRect(0, 0, slider.Bounds.Width, height), cornerRadius / 3);
        maximumTrackColor.SetFill();
        maxPath.Fill();
        UIImage maxTrackImage = UIGraphics.GetImageFromCurrentImageContext();
        UIGraphics.EndImageContext();

        slider.SetMinTrackImage(minTrackImage, UIControlState.Normal);
        slider.SetMaxTrackImage(maxTrackImage, UIControlState.Normal);
    }
}


