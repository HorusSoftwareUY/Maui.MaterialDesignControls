using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

class FloatingButtonBuilder : UIView
{
    #region Attributes & Properties
    
    public static bool DefaultUseAnimation { get; set; } = true;
    public static TimeSpan DefaultAnimationDuration { get; set; } = TimeSpan.FromMilliseconds(250);
    
    #endregion Attributes & Properties
    
    public FloatingButtonBuilder(MaterialFloatingButton config)
    {
        Build(config);
    }

    public void Show()
    {
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
    }
    
    public void Dismiss()
    {
        try
        {
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
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Not dismiss floating button ex: {ex.Message}");
        }
    }

    private void SetRoundedBackground(Color backgroundColor, double cornerRadius)
    {
        BackgroundColor = backgroundColor.ToPlatform();
        Layer.CornerRadius = (float)cornerRadius/2;
        TranslatesAutoresizingMaskIntoConstraints = false;
        Alpha = 0f;
    }
    
    private void SetMargin(UIWindow window, Thickness margin, MaterialFloatingButtonPosition position)
    {
        var constraints = new List<NSLayoutConstraint>();
        
        switch (position)
        {
            case MaterialFloatingButtonPosition.BottomRight:
                constraints.Add(BottomAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.BottomAnchor, -1*(float)margin.Bottom));
                constraints.Add(RightAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.RightAnchor, -1*(float)margin.Right));
                break;
            case MaterialFloatingButtonPosition.TopRight:
                constraints.Add(TopAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TopAnchor, (float)margin.Top));
                constraints.Add(RightAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.RightAnchor, -1*(float)margin.Right));
                break;
            case MaterialFloatingButtonPosition.TopLeft:
                constraints.Add(TopAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TopAnchor, (float)margin.Top));
                constraints.Add(LeftAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.LeftAnchor, (float)margin.Left));
                break;
            case MaterialFloatingButtonPosition.BottomLeft:
                constraints.Add(BottomAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.BottomAnchor, -1*(float)margin.Bottom));
                constraints.Add(LeftAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.LeftAnchor, (float)margin.Left));
                break;
        }
        
        NSLayoutConstraint.ActivateConstraints([.. constraints]);
    }
    
    private void SetSize(UIView view, double height, double width)
    {
        NSLayoutConstraint.ActivateConstraints(
        [
            view.LeftAnchor.ConstraintEqualTo(this.LeftAnchor, (float)width/4),
            view.RightAnchor.ConstraintEqualTo(this.RightAnchor, -(float)width/4),
            view.TopAnchor.ConstraintEqualTo(this.TopAnchor, (float)height/4),
            view.BottomAnchor.ConstraintEqualTo(this.BottomAnchor, -(float)height/4),
        ]);
    }
    
    private void Build(MaterialFloatingButton fab)
    {
        this.ClearSubviews();
        
        var window = UIKit.WindowExtensions.GetDefaultWindow();
        if (window == null) return;
        window.AddSubview(this);
        
        SetRoundedBackground(fab.BackgroundColor, fab.CornerRadius);
        SetMargin(window, fab.Margin, fab.Position);
        
        var content = CreateLayout(fab);
        AddSubview(content);
        SetSize(content, fab.IconSize + fab.Padding.VerticalThickness, fab.IconSize + fab.Padding.VerticalThickness);
    }
    
    private UIView CreateLayout(MaterialFloatingButton fab)
    {
        var container = new UIStackView
        { 
            Alignment = UIStackViewAlignment.Center,
            Axis = UILayoutConstraintAxis.Horizontal,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        var button = GetButtonImage(fab.Icon.Source(), fab.IconSize, fab.IconColor, () =>
        {
            if (fab.IsEnabled && (fab.Command?.CanExecute(fab.CommandParameter) ?? false))
            {
                fab.Command?.Execute(fab.CommandParameter);
            }
        });
        container.AddArrangedSubview(button);

        return container;
    }

    private UIButton GetButtonImage(string? iconSource, double iconSize, Color tintColor, Action? action)
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
}