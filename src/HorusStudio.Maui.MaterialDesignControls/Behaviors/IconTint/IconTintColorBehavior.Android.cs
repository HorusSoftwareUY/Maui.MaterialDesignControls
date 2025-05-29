#nullable enable

using System.ComponentModel;
using Android.Content.Res;
using Android.Graphics;
using Android.Widget;
using Microsoft.Maui.Platform;
using AButton = Android.Widget.Button;
using AView = Android.Views.View;
using Color = Microsoft.Maui.Graphics.Color;
using ImageButton = Microsoft.Maui.Controls.ImageButton;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors;

public partial class IconTintColorBehavior
{
    /// <inheritdoc/>
    protected override void OnAttachedTo(View bindable, AView platformView)
    {
        if (IsEnabled)
            ApplyTintColor(bindable, platformView);

        this.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == TintColorProperty.PropertyName && IsEnabled)
            {
                ApplyTintColor(bindable, platformView);
            }
            else
            {
                ClearTintColor(bindable, platformView);
            }
        };
    }

    /// <inheritdoc/>
    protected override void OnDetachedFrom(View bindable, AView platformView) =>
        ClearTintColor(bindable, platformView);

    void ApplyTintColor(View element, AView control)
    {
        var color = TintColor;

        if (color is null)
        {
            ClearTintColor(element, control);
            return;
        }

        element.PropertyChanged -= OnElementPropertyChanged;
        element.PropertyChanged += OnElementPropertyChanged;

        switch (control)
        {
            case ImageView image:
                SetImageViewTintColor(image, color);
                break;
            case AButton button:
                SetButtonTintColor(button, color);
                break;
            default:
                throw new NotSupportedException($"{nameof(IconTintColorBehavior)} only currently supports Android.Widget.Button and {nameof(ImageView)}.");
        }

        static void SetImageViewTintColor(ImageView image, Color? color)
        {
            if (color is null)
            {
                image.ClearColorFilter();
                color = Colors.Transparent;
            }

            try
            {
                image.SetColorFilter(new PorterDuffColorFilter(color.ToPlatform(), PorterDuff.Mode.SrcIn ?? throw new InvalidOperationException("PorterDuff.Mode.SrcIn should not be null at runtime.")));
            }
            catch (ObjectDisposedException)
            {
                // Catching the exception to prevent crashes if the image object has already been disposed.
            }
        }

        static void SetButtonTintColor(AButton button, Color? color)
        {
            if (button is MauiMaterialButton nativeButton)
            {
                if (color is null)
                {
                    nativeButton?.Icon?.ClearColorFilter();
                }
                else
                {
                    nativeButton.IconTint = ColorStateList.ValueOf(color.ToPlatform());
                    nativeButton.IconTintMode = PorterDuff.Mode.SrcIn;
                }
                return;
            }

            var drawables = button.GetCompoundDrawables().Where(d => d is not null);
            foreach (var img in drawables)
            {
                if (color is null)
                {
                    img?.ClearColorFilter();
                }
                else
                {
                    img?.SetTint(color.ToPlatform());
                }
            }

            color ??= Colors.Transparent;
        }
    }

    void OnElementPropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName is not string propertyName
            || sender is not View bindable
            || bindable.Handler?.PlatformView is not AView platformView)
        {
            return;
        }

        if (!propertyName.Equals(Image.SourceProperty.PropertyName, StringComparison.Ordinal)
            && !propertyName.Equals(ImageButton.SourceProperty.PropertyName, StringComparison.Ordinal))
        {
            return;
        }

        if (IsEnabled)
            ApplyTintColor(bindable, platformView);
    }

    void ClearTintColor(View element, AView control)
    {
        element.PropertyChanged -= OnElementPropertyChanged;
        switch (control)
        {
            case ImageView image:
                try
                {
                    image.ClearColorFilter();
                }
                catch (ObjectDisposedException)
                {
                    // Catching the exception to prevent crashes if the image object has already been disposed.
                }
                break;
            case AButton button:
                foreach (var drawable in button.GetCompoundDrawables())
                {
                    drawable?.ClearColorFilter();
                }
                break;
        }
    }
}