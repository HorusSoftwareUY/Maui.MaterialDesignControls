using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

class FloatingButton : UIView
{
    #region Attributes & Properties
    
    private readonly MaterialFloatingButton _fab;
    public static bool DefaultUseAnimation { get; set; } = true;
    public static TimeSpan DefaultAnimationDuration { get; set; } = TimeSpan.FromMilliseconds(250);
    
    #endregion Attributes & Properties
    
    public FloatingButton(MaterialFloatingButton config)
    {
        _fab = config;
        Build(config);
    }

    public void Show()
    {
        try
        {
            Utils.Logger.Debug("Showing FAB");
            if (DefaultUseAnimation)
            {
                Alpha = 0f;
                Animate(DefaultAnimationDuration.TotalSeconds, () =>
                {
                    Alpha = 1f;
                });    
            }
            else
            {
                Alpha = 1f;
            }
            Utils.Logger.Debug("FAB showed");
        }
        catch(Exception ex)
        {
            Utils.Logger.LogException("ERROR showing FAB", ex, _fab);
            throw;
        }
    }
    
    public void Dismiss()
    {
        try
        {
            Utils.Logger.Debug("Dismissing FAB");
            //TODO: Check when we should remove view from superview
            //RemoveFromSuperview();
            
            if (DefaultUseAnimation)
            {
                Animate(DefaultAnimationDuration.TotalSeconds, () =>
                {
                    Alpha = 0f;
                });    
            }
            else
            {
                Alpha = 0f;
            }
            Utils.Logger.Debug("FAB dismissed");
        }
        catch(Exception ex)
        {
            Utils.Logger.LogException("ERROR dismissing FAB", ex, _fab);
            throw;
        }
    }
    
    private void Build(MaterialFloatingButton fab)
    {
        Utils.Logger.Debug("Creating FAB");
        try
        {
            Alpha = 0f;
            this.ClearSubviews();

            var window = UIKit.WindowExtensions.GetDefaultWindow();
            if (window == null) return;
            window.AddSubview(this);

            this.SetRoundedBackground(fab.BackgroundColor, fab.CornerRadius);
            this.SetMargin(window, fab.Margin, fab.Position);

            var tapGesture = new UITapGestureRecognizer(() =>
            {
                if (fab.IsEnabled && (fab.Command?.CanExecute(fab.CommandParameter) ?? false))
                {
                    fab.Command?.Execute(fab.CommandParameter);
                }
            });
            this.UserInteractionEnabled = true;
            this.AddGestureRecognizer(tapGesture);

            var content = CreateLayout(fab);
            AddSubview(content);
            SetSize(content, fab.IconSize + fab.Padding.VerticalThickness,
                fab.IconSize + fab.Padding.VerticalThickness);
            
            Utils.Logger.Debug("FAB created");
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR creating FAB", ex, _fab);
        }
    }
    
    private UIView CreateLayout(MaterialFloatingButton fab)
    {
        var container = new UIStackView
        { 
            Alignment = UIStackViewAlignment.Center,
            Axis = UILayoutConstraintAxis.Horizontal,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        var button = GetButtonImage(fab.Icon.Source(), fab.IconSize, fab.IconColor, fab.AutomationId);
        container.AddArrangedSubview(button);

        return container;
    }
    
    private UIButton GetButtonImage(string? iconSource, double iconSize, Color tintColor, string automationId)
    {
        var button = new UIButton
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
            BackgroundColor = UIColor.Clear,
            TintColor = tintColor.ToPlatform(),
            UserInteractionEnabled = false,
            AccessibilityIdentifier = automationId
        };
        
        var imageView = GetIcon(iconSource, iconSize);
        if (imageView?.Image is {} image)
        {
            button.SetImage(image, UIControlState.Normal);
        }

        return button;
    }

    private UIImageView? GetIcon(string? iconSource, double iconSize)
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
    
    private void SetSize(UIView view, double height, double width)
    {
        NSLayoutConstraint.ActivateConstraints(
        [
            view.LeftAnchor.ConstraintEqualTo(this.LeftAnchor, (float)width/4),
            view.RightAnchor.ConstraintEqualTo(this.RightAnchor, -(float)width/4),
            view.TopAnchor.ConstraintEqualTo(this.TopAnchor, (float)height/4),
            view.BottomAnchor.ConstraintEqualTo(this.BottomAnchor, -(float)height/4)
        ]);
    }
}