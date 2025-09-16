using Foundation;
using HorusStudio.Maui.MaterialDesignControls.Extensions.Label;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

class MaterialSnackbarBuilder : UIView
{
    #region Constants
    
    private const int TextMaxLines = 20;
    
    private const bool UseBlur = false;
    private const bool UseAnimation = true;
    private const UIBlurEffectStyle BlurEffectStyle = UIBlurEffectStyle.Dark;
    private static TimeSpan AnimationDuration { get; set; } = TimeSpan.FromMilliseconds(300);

    #endregion
    
    #region Attributes
    
    private readonly System.Timers.Timer _timer;
    private readonly Action? _onDismissed;
    private TaskCompletionSource? _showCompletionSource;
    
    #endregion
    
    public MaterialSnackbarBuilder(MaterialSnackbarConfig config)
    {
        _timer = new System.Timers.Timer(config.Duration) { AutoReset = false };
        _timer.Elapsed += (s, a) =>
        {
            Dismiss();
        };
        _onDismissed = config.OnDismissed;
        
        config.CornerRadius /= 2;
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

    private void Build(MaterialSnackbarConfig materialSnackbarConfig)
    {
        this.ClearSubviews();
        if (UseBlur) ConfigureBlur(materialSnackbarConfig);

        var window = UIKit.WindowExtensions.GetDefaultWindow();
        if (window == null) return;
        window.AddSubview(this);

        this.SetRoundedBackground(materialSnackbarConfig.BackgroundColor, materialSnackbarConfig.CornerRadius);
        this.SetMargin(window, materialSnackbarConfig.Margin, materialSnackbarConfig.Position);
       
        var popup = CreateLayout(materialSnackbarConfig);
        this.AddSubview(popup);
        
        NSLayoutConstraint.ActivateConstraints(
        [
            popup.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor, (float)materialSnackbarConfig.Padding.Left),
            popup.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor, (float)-materialSnackbarConfig.Padding.Right),
            popup.TopAnchor.ConstraintEqualTo(this.TopAnchor, (float)materialSnackbarConfig.Padding.Top),
            popup.BottomAnchor.ConstraintEqualTo(this.BottomAnchor, (float)-materialSnackbarConfig.Padding.Bottom),
        ]);
    }

    private UIView CreateLayout(MaterialSnackbarConfig materialSnackbarConfig)
    {
        var container = new UIStackView
        {
            Spacing = materialSnackbarConfig.Spacing,
            Alignment = UIStackViewAlignment.Center,
            Axis = UILayoutConstraintAxis.Horizontal,
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        
        if (materialSnackbarConfig.LeadingIcon?.Source is not null)
        {
            container.AddArrangedSubview(
                ConfigureIconButton(
                    materialSnackbarConfig.LeadingIcon.Source.Source(), 
                    materialSnackbarConfig.LeadingIcon.Size, 
                    materialSnackbarConfig.LeadingIcon.Color, 
                    materialSnackbarConfig.LeadingIcon.Action));
        }

        container.AddArrangedSubview(ConfigureText(materialSnackbarConfig));

        if (materialSnackbarConfig.Action?.Action is not null)
        {
            container.AddArrangedSubview(ConfigureAction(materialSnackbarConfig, Dismiss));    
        }
        
        if (materialSnackbarConfig.TrailingIcon?.Source is not null)
        {
            container.AddArrangedSubview(
                ConfigureIconButton(
                    materialSnackbarConfig.TrailingIcon.Source.Source(), 
                    materialSnackbarConfig.TrailingIcon.Size, 
                    materialSnackbarConfig.TrailingIcon.Color, 
                    materialSnackbarConfig.TrailingIcon.Action));
        }

        return container;
    }
    
    private void ConfigureBlur(MaterialSnackbarConfig materialSnackbarConfig)
    {
        var blurEffect = UIBlurEffect.FromStyle(BlurEffectStyle);
        var effectsView = new UIVisualEffectView
        {
            Effect = blurEffect,
            TranslatesAutoresizingMaskIntoConstraints = false,
            ClipsToBounds = true
        };
        effectsView.Layer.CornerRadius = materialSnackbarConfig.CornerRadius;

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

        if (action is not null)
        {
            button.TouchUpInside += (s, a) => action.Invoke();
        }
        
        var imageView = GetIcon(iconSource, iconSize);
        if (imageView?.Image is {} image)
        {
            button.SetImage(image, UIControlState.Normal);
        }

        button.SetSize(iconSize, iconSize);

        var widthConstraint = NSLayoutConstraint.Create(button, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1f, button.IntrinsicContentSize.Width);
        widthConstraint.Priority = (int)UILayoutPriority.Required;
        button.AddConstraint(widthConstraint);

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

    private static UIView ConfigureAction(MaterialSnackbarConfig materialSnackbarConfig, Action dismiss)
    {
        var actionConfig = materialSnackbarConfig.Action!;
        var button = new UIButton
        {
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        button.TouchUpInside += (s, a) =>
        {
            actionConfig.Action!.Invoke();
            dismiss();
        };

        UIFont? font = null;
        font ??= UIFont.SystemFontOfSize((nfloat)actionConfig.FontSize, UIFontWeight.Semibold);
        
        var attributes = new UIStringAttributes
        {
            UnderlineStyle = actionConfig.TextDecorations == TextDecorations.Underline ? NSUnderlineStyle.Single : NSUnderlineStyle.None,
            StrikethroughStyle = actionConfig.TextDecorations == TextDecorations.Strikethrough ? NSUnderlineStyle.Single : NSUnderlineStyle.None,
            Font = font,
            ForegroundColor = actionConfig.Color.ToPlatform()
        };
        button.SetAttributedTitle(new NSMutableAttributedString(actionConfig.Text, attributes), UIControlState.Normal);

        if (OperatingSystem.IsMacCatalystVersionAtLeast(15) || OperatingSystem.IsIOSVersionAtLeast(15))
        {
            var configuration = UIButtonConfiguration.PlainButtonConfiguration;
            configuration.ImagePadding = materialSnackbarConfig.Spacing;
            configuration.ContentInsets = new NSDirectionalEdgeInsets(0, 0, 0, 0);
            button.Configuration = configuration;
        }
        else
        {
            button.ImageEdgeInsets = new UIEdgeInsets(0, materialSnackbarConfig.Spacing, 0, materialSnackbarConfig.Spacing);
        }

        var widthConstraint = NSLayoutConstraint.Create(button, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1f, button.IntrinsicContentSize.Width);
        widthConstraint.Priority = (int)UILayoutPriority.Required;
        button.AddConstraint(widthConstraint);

        return button;
    }

    private static UIView ConfigureText(MaterialSnackbarConfig materialSnackbarConfig)
    {
        UIFont font = null;
        font ??= UIFont.SystemFontOfSize((nfloat)materialSnackbarConfig.FontSize);

        var label = new UILabel
        {
            TextColor = materialSnackbarConfig.TextColor.ToPlatform(),
            Font = font,
            LineBreakMode = UILineBreakMode.WordWrap,
            Lines = 0,
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        
        label.SetTextCharacterSpacing(materialSnackbarConfig.Message, materialSnackbarConfig.CharacterSpacing);
        label.SetLineBreakMode(materialSnackbarConfig.LineBreakMode, TextMaxLines);
            
        return label;
    }
}