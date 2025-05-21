#nullable enable

using System.ComponentModel;
using CoreAnimation;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors;

public partial class IconTintColorBehavior
{
    /// <inheritdoc/>
    protected override void OnAttachedTo(View bindable, UIView platformView)
    {
        if (IsEnabled)
            ApplyTintColor(platformView, bindable, TintColor);

        bindable.PropertyChanged += OnElementPropertyChanged;
        this.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == TintColorProperty.PropertyName && IsEnabled)
            {
                ApplyTintColor(platformView, bindable, TintColor);
            }
            else
            {
                ClearTintColor(platformView, bindable);
            }
        };
    }

    void OnElementPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if ((e.PropertyName != ImageButton.IsLoadingProperty.PropertyName
            && e.PropertyName != Image.SourceProperty.PropertyName
            && e.PropertyName != ImageButton.SourceProperty.PropertyName)
            || sender is not IImageElement element
            || (sender as VisualElement)?.Handler?.PlatformView is not UIView platformView)
        {
            return;
        }

        if (!element.IsLoading)
        {
            ApplyTintColor(platformView, (View)element, TintColor);
        }
    }

    /// <inheritdoc/>
    protected override void OnDetachedFrom(View bindable, UIView platformView) =>
        ClearTintColor(platformView, bindable);

    void ClearTintColor(UIView platformView, View element)
    {
        element.PropertyChanged -= OnElementPropertyChanged;
        switch (platformView)
        {
            case UIImageView imageView:
                if (imageView.Image is not null)
                {
                    imageView.Image = imageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                }

                break;
            case UIButton button:
                if (button.ImageView?.Image is not null)
                {
                    var originalImage = button.CurrentImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                    button.SetImage(originalImage, UIControlState.Normal);
                }

                break;

            default:
                throw new NotSupportedException($"{nameof(IconTintColorBehavior)} only currently supports {nameof(UIButton)} and {nameof(UIImageView)}.");
        }
    }

    void ApplyTintColor(UIView platformView, View element, Color? color)
    {
        if (color is null)
        {
            ClearTintColor(platformView, element);
            return;
        }

        switch (platformView)
        {
            case UIImageView imageView:
                SetUIImageViewTintColor(imageView, element, color);
                break;
            case UIButton button:
                SetUIButtonTintColor(button, element, color);
                break;
            default:
                throw new NotSupportedException($"{nameof(IconTintColorBehavior)} only currently supports {nameof(UIButton)} and {nameof(UIImageView)}.");
        }
    }

    static void SetUIButtonTintColor(UIButton button, View element, Color color)
    {
        // Runs after the current layout and render cycle
        CATransaction.Begin();
        CATransaction.CompletionBlock = () =>
        {
            if (button.ImageView.Image is null || button.CurrentImage is null)
            {
                return;
            }

            var templatedImage = button.CurrentImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            button.SetImage(templatedImage, UIControlState.Normal);

            var platformColor = color.ToPlatform();
            button.TintColor = platformColor;
            button.ImageView.TintColor = platformColor;

            button.SetNeedsLayout();
            button.LayoutIfNeeded();
        };
        CATransaction.Commit();
    }

    static void SetUIImageViewTintColor(UIImageView imageView, View element, Color color)
    {
        if (imageView.Image is null)
        {
            return;
        }

        imageView.Image = imageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        imageView.TintColor = color.ToPlatform();
    }
}