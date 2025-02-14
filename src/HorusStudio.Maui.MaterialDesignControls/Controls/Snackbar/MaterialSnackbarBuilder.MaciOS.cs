using CoreGraphics;
using Foundation;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

public class MaterialSnackbarBuilder : UIView
{
    private System.Timers.Timer _timer;
    private DateTime _endOfAnimation;
    
    public static Thickness DefaultPadding { get; set; } = new Thickness(20);
    public static Thickness DefaultSnackbarMargin { get; set; } = new Thickness(20, 50, 20, 30);
    public static bool DefaultUseBlur { get; set; } = true;
    public static bool DefaultUseAnimation { get; set; } = true;
    public static TimeSpan DefaultAnimationDuration { get; set; } = TimeSpan.FromMilliseconds(250);
    public static float DefaultIconSpacing { get; set; } = 16f;
    public static UIBlurEffectStyle DefaultBlurEffectStyle { get; set; } = UIBlurEffectStyle.Dark;
    
    public Thickness Padding { get; set; } = DefaultPadding;
    public Thickness SnackbarMargin { get; set; } = DefaultSnackbarMargin;
    public bool UseBlur { get; set; } = DefaultUseBlur;
    public bool UseAnimation { get; set; } = DefaultUseAnimation;
    public float CornerRadius
    {
        get => (float)this.Layer.CornerRadius;
        set => this.Layer.CornerRadius = value;
    }
    public float IconSpacing { get; set; } = DefaultIconSpacing;
    public UIBlurEffectStyle BlurEffectStyle { get; set; } = DefaultBlurEffectStyle;
    public event EventHandler Timeout;
    public TimeSpan AnimationDuration { get; set; } = DefaultAnimationDuration;

    protected SnackbarConfig Config { get; }
    
    public MaterialSnackbarBuilder(SnackbarConfig config)
    {
        Config = config;
        TranslatesAutoresizingMaskIntoConstraints = false;
        BackgroundColor = config.BackgroundColor.ToPlatform();
        CornerRadius = config.CornerRadius;
        
    }
    
    public virtual void Dismiss()
    {
        try
        {
            _timer?.Stop();
        }
        catch { }
        try
        {
            this.RemoveFromSuperview();
        }
        catch { }
    }

    public virtual void Show()
    {
        this.ClearSubviews();

        if (UseBlur) SetupBlur();

        var window = UIKit.WindowExtensions.GetDefaultWindow();
        if (window == null) return;
        window.AddSubview(this);

        var constraints = new List<NSLayoutConstraint>
        {
            this.CenterXAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.CenterXAnchor)
        };

        _timer = new System.Timers.Timer
        {
            Interval = 500
        };

        var popup = SetupSnackBar();

        this.AddSubview(popup);

        NSLayoutConstraint.ActivateConstraints(
        [
            popup.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor, (float)Padding.Left),
            popup.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor, (float)-Padding.Right),
            popup.TopAnchor.ConstraintEqualTo(this.TopAnchor, (float)Padding.Top),
            popup.BottomAnchor.ConstraintEqualTo(this.BottomAnchor, (float)-Padding.Bottom),
        ]);

        constraints.Add(this.LeadingAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.LeadingAnchor, (float)SnackbarMargin.Left));
        constraints.Add(this.TrailingAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TrailingAnchor, (float)-SnackbarMargin.Right));

        constraints.Add(Config.Position is SnackbarPosition.Bottom
            ? this.BottomAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.BottomAnchor, (float)-SnackbarMargin.Bottom)
            : this.TopAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TopAnchor, (float)SnackbarMargin.Top));

        NSLayoutConstraint.ActivateConstraints([.. constraints]);

        if (UseAnimation)
        {
            this.Alpha = 0;

            UIView.Animate(AnimationDuration.TotalSeconds, () =>
            {
                this.Alpha = 1f;
            });
        }

        _endOfAnimation = DateTime.Now + Config.Duration;
        _timer.Elapsed += (s, a) =>
        {
            var rest = (_endOfAnimation - a.SignalTime).TotalSeconds;
            if (rest > 0) return;
            _timer.Stop();

            if (!UseAnimation) return;

            this.InvokeOnMainThread(() =>
            {
                UIView.Animate(AnimationDuration.TotalSeconds,
                    () =>
                    {
                        this.Alpha = 0f;
                    },
                    () =>
                    {
                        this.Dismiss();
                        this.Timeout?.Invoke(this, EventArgs.Empty);
                    });
            });
        };
        _timer.Start();
    }

    protected virtual void SetupBlur()
    {
        var blurEffect = UIBlurEffect.FromStyle(BlurEffectStyle);
        var effectsView = new UIVisualEffectView
        {
            Effect = blurEffect,
            TranslatesAutoresizingMaskIntoConstraints = false,
            ClipsToBounds = true
        };
        effectsView.Layer.CornerRadius = CornerRadius;

        this.AddSubview(effectsView);

        NSLayoutConstraint.ActivateConstraints(
        [
            effectsView.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor),
            effectsView.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor),
            effectsView.TopAnchor.ConstraintEqualTo(this.TopAnchor),
            effectsView.BottomAnchor.ConstraintEqualTo(this.BottomAnchor),
        ]);
    }

    protected virtual UIView SetupSnackBar()
    {
        var container = new UIStackView
        {
            Spacing = IconSpacing,
            Alignment = UIStackViewAlignment.Center,
            Axis = UILayoutConstraintAxis.Horizontal,
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        
        if (Config.LeadingIcon is not null)
        {
            container.AddArrangedSubview(GetButtonImage(Config.LeadingIcon.Source.Source(), Config.LeadingIcon.Color, Config.LeadingIcon.Size, Config.LeadingIcon.Action));
        }

        container.AddArrangedSubview(GetLabel());
        

        var action = GetAction();

        var widthConstraint = NSLayoutConstraint.Create(action, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1f, action.IntrinsicContentSize.Width);
        widthConstraint.Priority = (int)UILayoutPriority.Required;
        action.AddConstraint(widthConstraint);

        container.AddArrangedSubview(action);
        
        if (Config.TrailingIcon is not null)
        {
            container.AddArrangedSubview(GetButtonImage(Config.TrailingIcon.Source.Source(), Config.TrailingIcon.Color, Config.TrailingIcon.Size, Config.TrailingIcon.Action));
        }

        return container;
    }

    protected virtual UIButton GetButtonImage(string icon, Color color, int size, Action? action)
    {
        var button = new UIButton
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
        };
        
        button.TouchUpInside += (s, a) =>
        {
            this.Dismiss();
            action?.Invoke();
        };
        
        if (OperatingSystem.IsMacCatalystVersionAtLeast(15) || OperatingSystem.IsIOSVersionAtLeast(15))
        {
            var configuration = UIButtonConfiguration.PlainButtonConfiguration;
            configuration.ImagePadding = 10;
            configuration.ContentInsets = new NSDirectionalEdgeInsets(0, 8, 0, 8);
            button.Configuration = configuration;
        }
        else
        {
            button.ImageEdgeInsets = new UIEdgeInsets(0, 0, 0, 10f);
        }

        var widthConstraint = NSLayoutConstraint.Create(button, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1f, button.IntrinsicContentSize.Width);
        widthConstraint.Priority = (int)UILayoutPriority.Required;
        button.AddConstraint(widthConstraint);
        var imageView = GetIcon(icon, color, size);
        button.BackgroundColor = Colors.Transparent.ToPlatform();
        if (imageView.Image != null)
        {
            button.TintColor = color.ToPlatform();
            button.ImageView.TintColor = color.ToPlatform();
            button.SetImage(imageView.Image, UIControlState.Normal);
        }

        return button;
    }

    protected virtual UIView GetAction()
    {
        var container = new UIStackView
        {
            Spacing = IconSpacing,
            Alignment = UIStackViewAlignment.Center,
            Axis = UILayoutConstraintAxis.Horizontal,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        container.AddArrangedSubview(GetActionButton());

        return container;
    }

    protected virtual UIView GetActionButton()
    {
        var button = new UIButton
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
        };
        button.TouchUpInside += (s, a) =>
        {
            this.Dismiss();
            Config.Action?.Action?.Invoke();
        };

        UIFont font = null;
        font ??= UIFont.SystemFontOfSize((nfloat)Config.Action.FontSize);
        button.SetAttributedTitle(new NSMutableAttributedString(Config.Action.Text, font, Config.Action.Color.ToPlatform()), UIControlState.Normal);

        if (OperatingSystem.IsMacCatalystVersionAtLeast(15) || OperatingSystem.IsIOSVersionAtLeast(15))
        {
            var configuration = UIButtonConfiguration.PlainButtonConfiguration;
            configuration.ImagePadding = 10;
            configuration.ContentInsets = new NSDirectionalEdgeInsets(0, 0, 0, 0);
            button.Configuration = configuration;
        }
        else
        {
            button.ImageEdgeInsets = new UIEdgeInsets(0, 0, 0, 10f);
        }

        var widthConstraint = NSLayoutConstraint.Create(button, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1f, button.IntrinsicContentSize.Width);
        widthConstraint.Priority = (int)UILayoutPriority.Required;
        button.AddConstraint(widthConstraint);

        return button;
    }

    protected virtual UIImageView GetIcon(string Icon, Color color, int size)
    {
        var image = new UIImageView(new CGRect(0, 0, size, size))
        {
            Image = new UIImage(Icon).ScaleTo(size),
            ContentMode = UIViewContentMode.Center,
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        
        image.Image = image.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        image.TintColor = color.ToPlatform();
        return image;
    }

    protected virtual UIView GetLabel()
    {
        UIFont font = null;
        font ??= UIFont.SystemFontOfSize((nfloat)Config.FontSize);

        var label = new UILabel
        {
            Text = Config.Message,
            TextColor = Config.TextColor.ToPlatform(),
            Font = font,
            LineBreakMode = UILineBreakMode.WordWrap,
            Lines = 0,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        return label;
    }
}