using System.ComponentModel;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Microsoft.Maui.Handlers;

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

    protected override void ConnectHandler(Google.Android.Material.Button.MaterialButton platformView)
    {
        base.ConnectHandler(platformView);

        platformView.LayoutChange += PlatformView_LayoutChange;

        if (VirtualView is Button button)
        {
            button.PropertyChanged += OnElementPropertyChanged;
        }
    }

    protected override void DisconnectHandler(Google.Android.Material.Button.MaterialButton platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.LayoutChange -= PlatformView_LayoutChange;

        if (VirtualView is Button button)
        {
            button.PropertyChanged -= OnElementPropertyChanged;
        }
    }

    private void PlatformView_LayoutChange(object? sender, Android.Views.View.LayoutChangeEventArgs e)
    {
        if (VirtualView is CustomButton customButton)
        {
            SetIconSize(PlatformView, customButton);
        }
    }

    private void OnElementPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (VirtualView is CustomButton customButton
            && (e.PropertyName == Image.SourceProperty.PropertyName
            || e.PropertyName == CustomButton.IconSizeProperty.PropertyName
            || e.PropertyName == CustomButton.ApplyIconTintColorProperty.PropertyName))
        {
            SetIconSize(PlatformView, customButton);
        }
    }

    private void SetIconSize(Google.Android.Material.Button.MaterialButton platformView, CustomButton customButton)
    {
        var activity = Platform.CurrentActivity;
        activity?.SafeRunOnUiThreadAsync(async () =>
        {
            if (platformView.Icon != null
                && platformView.Icon is LayerDrawable layerDrawable
                && layerDrawable.GetDrawable(0) is BitmapDrawable bitmapDrawable
                && bitmapDrawable.Bitmap != null
                && platformView.Context != null
                && customButton.IconSize != Size.Zero)
            {
                var targetWidth = ((int)customButton.IconSize.Width).DpToPixels();
                var targetHeight = ((int)customButton.IconSize.Height).DpToPixels();
                var scaledBitmap = Bitmap.CreateScaledBitmap(bitmapDrawable.Bitmap, targetWidth, targetHeight, true);
                var scaledDrawable = new BitmapDrawable(platformView.Context.Resources, scaledBitmap);
                platformView.Icon = scaledDrawable;
                platformView.Invalidate();
            }
        });
    }
}