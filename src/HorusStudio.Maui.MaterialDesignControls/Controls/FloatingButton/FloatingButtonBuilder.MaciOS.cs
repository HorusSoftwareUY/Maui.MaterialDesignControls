using CoreGraphics;
using Foundation;
using HorusStudio.Maui.MaterialDesignControls.Extensions;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

public class FloatingButtonBuilder_MaciOS : UIView
{
    public static bool DefaultUseBlur { get; set; } = true;
    public static bool DefaultUseAnimation { get; set; } = true;
    public static TimeSpan DefaultAnimationDuration { get; set; } = TimeSpan.FromMilliseconds(250);
    public static float DefaultIconSpacing { get; set; } = 16f;
    public static UIBlurEffectStyle DefaultBlurEffectStyle { get; set; } = UIBlurEffectStyle.Dark;

    protected FloatingButtonConfig Config { get; }
    
    public FloatingButtonBuilder_MaciOS(FloatingButtonConfig config)
    {
        Config = config;
        TranslatesAutoresizingMaskIntoConstraints = false;
        BackgroundColor = config.BackgroundColor.ToPlatform();
        Layer.CornerRadius = (float)config.CornerRadius.BottomRight;
    }
    
    public virtual void Dismiss()
    {
        try
        {
            RemoveFromSuperview();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Not dismiss floating button ex: {ex.Message}");
        }
    }

    public virtual void Show()
    {
        this.ClearSubviews();

        var window = ExtensionsConverters.GetDefaultWindow();
        window.AddSubview(this);
            
        List<NSLayoutConstraint> constraints = new List<NSLayoutConstraint>();

        var popup = SetupFlatingButton();
        this.AddSubview(popup);

        if (Config.Type == MaterialFloatingButtonType.FAB)
        {
            NSLayoutConstraint.ActivateConstraints(
            [
                popup.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor, 38/2),
                popup.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor, -38/2),
                popup.TopAnchor.ConstraintEqualTo(this.TopAnchor, 38/2),
                popup.BottomAnchor.ConstraintEqualTo(this.BottomAnchor, -38/2),
            ]);
        }
        else if (Config.Type == MaterialFloatingButtonType.Small)
        {
            NSLayoutConstraint.ActivateConstraints(
            [
                popup.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor, 19/2),
                popup.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor, -19/2),
                popup.TopAnchor.ConstraintEqualTo(this.TopAnchor, 19/2),
                popup.BottomAnchor.ConstraintEqualTo(this.BottomAnchor, -19/2),
            ]);
        }
        else if (Config.Type == MaterialFloatingButtonType.Large)
        {
            NSLayoutConstraint.ActivateConstraints(
            [
                popup.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor, 56/2),
                popup.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor, -56/2),
                popup.TopAnchor.ConstraintEqualTo(this.TopAnchor, 56/2),
                popup.BottomAnchor.ConstraintEqualTo(this.BottomAnchor, -56/2),
            ]);
        }
        
        if (Config.Position == MaterialFloatingButtonPosition.BottomRight)
        {
            constraints.Add(this.BottomAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.BottomAnchor, -16f));
            constraints.Add(this.TrailingAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TrailingAnchor, -16f));
        }
        else if (Config.Position == MaterialFloatingButtonPosition.TopRight)
        {
            constraints.Add(this.TopAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TopAnchor, 56f));
            constraints.Add(this.TrailingAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TrailingAnchor, -16f));
        }
        else if (Config.Position == MaterialFloatingButtonPosition.TopLeft)
        {
            constraints.Add(this.TopAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TopAnchor, 56f));
            constraints.Add(this.LeadingAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.LeadingAnchor, 16f));
        }
        else if (Config.Position == MaterialFloatingButtonPosition.BottomLeft)
        {
            constraints.Add(this.BottomAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.BottomAnchor, -16f));
            constraints.Add(this.LeadingAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.LeadingAnchor, 16f));
        }
        
        NSLayoutConstraint.ActivateConstraints([.. constraints]);

        if (DefaultUseAnimation)
        {
            this.Alpha = 0;

            UIView.Animate(DefaultAnimationDuration.TotalSeconds, () =>
            {
                this.Alpha = 1f;
            });
        }
    }

    protected virtual void SetupBlur()
    {
        var blurEffect = UIBlurEffect.FromStyle(DefaultBlurEffectStyle);
        var effectsView = new UIVisualEffectView
        {
            Effect = blurEffect,
            TranslatesAutoresizingMaskIntoConstraints = false,
            ClipsToBounds = true
        };
        effectsView.Layer.CornerRadius = (float)Config.CornerRadius.BottomRight;
        

        this.AddSubview(effectsView);

        NSLayoutConstraint.ActivateConstraints(
        [
            effectsView.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor),
            effectsView.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor),
            effectsView.TopAnchor.ConstraintEqualTo(this.TopAnchor),
            effectsView.BottomAnchor.ConstraintEqualTo(this.BottomAnchor),
        ]);
    }

    protected virtual UIView SetupFlatingButton()
    {
        var container = new UIStackView
        { 
            Alignment = UIStackViewAlignment.Center,
            Axis = UILayoutConstraintAxis.Horizontal,
            TranslatesAutoresizingMaskIntoConstraints = false,
        };
        
        if (Config.Icon is not null)
        {
            container.AddArrangedSubview(GetButtonImage(Config.Icon, Config.IconColor, Config.Action));
        }

        return container;
    }

    protected virtual UIButton GetButtonImage(string icon, Color color, Action? action)
    {
        var button = new UIButton
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
        };
        
        button.TouchUpInside += (s, a) =>
        {
            action?.Invoke();
        };
        
        if (OperatingSystem.IsMacCatalystVersionAtLeast(15) || OperatingSystem.IsIOSVersionAtLeast(15))
        {
            var configuration = UIButtonConfiguration.PlainButtonConfiguration;
            configuration.ContentInsets = new NSDirectionalEdgeInsets(0,10,0,8);
            button.Configuration = configuration;
        }
        else
        {
            button.ImageEdgeInsets = new UIEdgeInsets(0,10,0,8);
        }

        var widthConstraint = NSLayoutConstraint.Create(button, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1f, button.IntrinsicContentSize.Width);
        widthConstraint.Priority = (int)UILayoutPriority.Required;
        button.AddConstraint(widthConstraint);
        var imageView = GetIcon(icon, color);
        button.BackgroundColor = Colors.Transparent.ToPlatform();
        if (imageView.Image != null)
        {
            button.TintColor = color.ToPlatform();
            button.ImageView.TintColor = color.ToPlatform();
            button.SetImage(imageView.Image, UIControlState.Normal);
        }

        return button;
    }

    protected virtual UIImageView GetIcon(string Icon, Color color)
    {
        var image = new UIImageView(new CGRect(0, 0, Config.IconSize, Config.IconSize))
        {
            Image = new UIImage(Icon).ScaleTo(Config.IconSize),
            ContentMode = UIViewContentMode.Center,
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        
        image.Image = image.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        image.TintColor = color.ToPlatform();
        return image;
    }
}