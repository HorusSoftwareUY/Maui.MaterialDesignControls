using CoreGraphics;
using Foundation;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using UIKit;
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace HorusStudio.Maui.MaterialDesignControls;

public class MaterialSnackbarBuilder : UIView
{
    #region Constants
    
    private const bool UseBlur = true;
    private const bool UseAnimation = true;
    private const UIBlurEffectStyle BlurEffectStyle = UIBlurEffectStyle.Dark;
    private static TimeSpan AnimationDuration = TimeSpan.FromMilliseconds(300);
    
    #endregion
    
    #region Attributes
    
    private System.Timers.Timer _timer;
    private Action? _onDismissed;
    private TaskCompletionSource? _showCompletionSource;
    
    #endregion
    
    public MaterialSnackbarBuilder(SnackbarConfig config)
    {
        Build(config);
    }
    
    public void Show()
    {
        _timer.Start();
        if (UseAnimation)
        {
            Alpha = 0f;
            Animate(AnimationDuration.TotalSeconds, () =>
            {
                Alpha = 1f;
            });    
        }
        else
        {
            Alpha = 1f;
        }
    }
    
    public Task ShowAsync()
    {
        _showCompletionSource = new TaskCompletionSource();
        Show();
        return _showCompletionSource.Task;
    }
    
    public void Dismiss()
    {
        UIApplication.SharedApplication.SafeInvokeOnMainThread(() =>
        {
            if (UseAnimation)
            {
                Animate(AnimationDuration.TotalSeconds, () =>
                {
                    Alpha = 0f;
                });
            }
            else
            {
                Alpha = 0f;
            }

            RemoveFromSuperview();
            
            if (_showCompletionSource != null)
            {
                _showCompletionSource.SetResult();
                _showCompletionSource = null;
                return;
            };
            _onDismissed?.Invoke();
        });
    }

    private void Build(SnackbarConfig snackbarConfig)
    {
        _timer = new System.Timers.Timer(snackbarConfig.Duration) { AutoReset = false };
        _timer.Elapsed += (s, a) =>
        {
            Dismiss();
        };
        _onDismissed = snackbarConfig.OnDismissed;

        this.ClearSubviews();
        if (UseBlur) ConfigureBlur(snackbarConfig);

        var window = UIKit.WindowExtensions.GetDefaultWindow();
        if (window == null) return;
        window.AddSubview(this);

        this.SetRoundedBackground(snackbarConfig.BackgroundColor, snackbarConfig.CornerRadius);
        this.SetMargin(window, snackbarConfig.Margin, snackbarConfig.Position);
       
        var popup = CreateLayout(snackbarConfig);
        this.AddSubview(popup);
        
        NSLayoutConstraint.ActivateConstraints(
        [
            popup.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor, (float)snackbarConfig.Padding.Left),
            popup.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor, (float)-snackbarConfig.Padding.Right),
            popup.TopAnchor.ConstraintEqualTo(this.TopAnchor, (float)snackbarConfig.Padding.Top),
            popup.BottomAnchor.ConstraintEqualTo(this.BottomAnchor, (float)-snackbarConfig.Padding.Bottom),
        ]);
    }

    private UIView CreateLayout(SnackbarConfig snackbarConfig)
    {
        var container = new UIStackView
        {
            Spacing = snackbarConfig.Spacing,
            Alignment = UIStackViewAlignment.Center,
            Axis = UILayoutConstraintAxis.Horizontal,
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        
        if (snackbarConfig.LeadingIcon?.Source is not null)
        {
            container.AddArrangedSubview(
                ConfigureIconButton(
                    snackbarConfig.LeadingIcon.Source.Source(), 
                    snackbarConfig.LeadingIcon.Size, 
                    snackbarConfig.LeadingIcon.Color, 
                    snackbarConfig.LeadingIcon.Action));
        }

        container.AddArrangedSubview(ConfigureText(snackbarConfig));

        if (snackbarConfig.Action?.Action is not null)
        {
            container.AddArrangedSubview(ConfigureAction(snackbarConfig, Dismiss));    
        }
        
        if (snackbarConfig.TrailingIcon?.Source is not null)
        {
            container.AddArrangedSubview(
                ConfigureIconButton(
                    snackbarConfig.TrailingIcon.Source.Source(), 
                    snackbarConfig.TrailingIcon.Size, 
                    snackbarConfig.TrailingIcon.Color, 
                    snackbarConfig.TrailingIcon.Action));
        }

        return container;
    }
    
    private void ConfigureBlur(SnackbarConfig snackbarConfig)
    {
        var blurEffect = UIBlurEffect.FromStyle(BlurEffectStyle);
        var effectsView = new UIVisualEffectView
        {
            Effect = blurEffect,
            TranslatesAutoresizingMaskIntoConstraints = false,
            ClipsToBounds = true
        };
        effectsView.Layer.CornerRadius = snackbarConfig.CornerRadius;

        this.AddSubview(effectsView);

        NSLayoutConstraint.ActivateConstraints(
        [
            effectsView.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor),
            effectsView.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor),
            effectsView.TopAnchor.ConstraintEqualTo(this.TopAnchor),
            effectsView.BottomAnchor.ConstraintEqualTo(this.BottomAnchor),
        ]);
    }
    
    private static UIButton ConfigureIconButton(string? iconSource, double iconSize, Color tintColor, Action? action)
    {
        var button = new UIButton
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
            BackgroundColor = UIColor.Clear,
            TintColor = tintColor.ToPlatform()
        };
        
        button.TouchUpInside += (s, a) =>
        {
            action?.Invoke();
        };
        
        var imageView = GetIcon(iconSource, iconSize);
        if (imageView?.Image is {} image)
        {
            button.SetImage(image, UIControlState.Normal);
        }

        button.SetSize(iconSize, iconSize);
        return button;
    }

    private static UIImageView? GetIcon(string? iconSource, double iconSize)
    {
        if (string.IsNullOrEmpty(iconSource)) return null;
        
        var icon = new UIImage(iconSource)
            .ScaleTo(iconSize)
            .ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        
        var image = new UIImageView
        {
            Image = icon,
            ContentMode = UIViewContentMode.Center,
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        
        return image;
    }

    private static UIView ConfigureAction(SnackbarConfig snackbarConfig, Action dismiss)
    {
        var actionConfig = snackbarConfig.Action;
        var button = new UIButton
        {
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        button.TouchUpInside += (s, a) =>
        {
            actionConfig.Action.Invoke();
            dismiss();
        };

        UIFont font = null;
        font ??= UIFont.SystemFontOfSize((nfloat)actionConfig.FontSize, UIFontWeight.Semibold);
        button.SetAttributedTitle(new NSMutableAttributedString(actionConfig.Text, font, actionConfig.Color.ToPlatform()), UIControlState.Normal);

        if (OperatingSystem.IsMacCatalystVersionAtLeast(15) || OperatingSystem.IsIOSVersionAtLeast(15))
        {
            var configuration = UIButtonConfiguration.PlainButtonConfiguration;
            configuration.ImagePadding = snackbarConfig.Spacing;
            configuration.ContentInsets = new NSDirectionalEdgeInsets(0, 0, 0, 0);
            button.Configuration = configuration;
        }
        else
        {
            button.ImageEdgeInsets = new UIEdgeInsets(0, snackbarConfig.Spacing, 0, snackbarConfig.Spacing);
        }

        var widthConstraint = NSLayoutConstraint.Create(button, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1f, button.IntrinsicContentSize.Width);
        widthConstraint.Priority = (int)UILayoutPriority.Required;
        button.AddConstraint(widthConstraint);

        return button;
    }

    private static UIView ConfigureText(SnackbarConfig snackbarConfig)
    {
        UIFont font = null;
        font ??= UIFont.SystemFontOfSize((nfloat)snackbarConfig.FontSize);

        var label = new UILabel
        {
            Text = snackbarConfig.Message,
            TextColor = snackbarConfig.TextColor.ToPlatform(),
            Font = font,
            LineBreakMode = UILineBreakMode.WordWrap,
            Lines = 0,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        return label;
    }
}