using System.ComponentModel;
using CoreGraphics;
using Microsoft.Maui.Handlers;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialButtonHandler 
{
    public static void MapTextDecorations(IButtonHandler handler, IButton button)
    {
        if (button is CustomButton customButton)
        {
            handler.PlatformView?.SetTextDecorations(customButton.TextDecorations);
        }
    }

    protected override void ConnectHandler(UIButton platformView)
    {
        base.ConnectHandler(platformView);

        if (VirtualView is Button button)
        {
            button.PropertyChanged += OnElementPropertyChanged;
        }
    }

    protected override void DisconnectHandler(UIButton platformView)
    {
        base.DisconnectHandler(platformView);

        if (VirtualView is Button button)
        {
            button.PropertyChanged -= OnElementPropertyChanged;
        }
    }

    private void OnElementPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (VirtualView is CustomButton customButton
            && (e.PropertyName != Image.SourceProperty.PropertyName
            || e.PropertyName != CustomButton.IconSizeProperty.PropertyName
            || e.PropertyName != CustomButton.ApplyIconTintColorProperty.PropertyName))
        {
            SetIconSize(PlatformView, customButton);
        }
    }

    private void SetIconSize(UIButton button, CustomButton customButton)
    {
        if (button.ImageView.Image is null || button.CurrentImage is null || customButton.IconSize == Size.Zero)
        {
            return;
        }

        var resizedImage = ResizeImage(button.CurrentImage, new CGSize(customButton.IconSize.Width, customButton.IconSize.Height), customButton.ApplyIconTintColor);
        button.SetImage(resizedImage, UIControlState.Normal);
    }

    private UIImage ResizeImage(UIImage icon, CGSize newSize, bool applyIconTintColor)
    {
        var renderer = new UIGraphicsImageRenderer(newSize);
        var resizedImage = renderer.CreateImage(context =>
        {
            icon.Draw(new CGRect(0, 0, newSize.Width, newSize.Height));
        });

        if (applyIconTintColor)
        {
            return resizedImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        }
        else
        {
            return resizedImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
        }
    }
}