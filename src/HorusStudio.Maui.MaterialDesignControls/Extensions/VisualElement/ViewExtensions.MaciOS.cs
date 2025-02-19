using HorusStudio.Maui.MaterialDesignControls;
using Microsoft.Maui.Platform;

namespace UIKit;

public static class ViewExtensions
{
    public static void SetRoundedBackground(this UIView view, Color color, double radius)
    {
        view.BackgroundColor = color.ToPlatform();
        view.Layer.CornerRadius = (float)radius;
        view.TranslatesAutoresizingMaskIntoConstraints = false;
    }
    
    public static void SetMargin(this UIView view, UIWindow window, Thickness margin, MaterialFloatingButtonPosition position)
    {
        var constraints = new List<NSLayoutConstraint>();
        
        switch (position)
        {
            case MaterialFloatingButtonPosition.BottomRight:
                constraints.Add(view.BottomAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.BottomAnchor, -1*(float)margin.Bottom));
                constraints.Add(view.RightAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.RightAnchor, -1*(float)margin.Right));
                break;
            case MaterialFloatingButtonPosition.TopRight:
                constraints.Add(view.TopAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TopAnchor, (float)margin.Top));
                constraints.Add(view.RightAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.RightAnchor, -1*(float)margin.Right));
                break;
            case MaterialFloatingButtonPosition.TopLeft:
                constraints.Add(view.TopAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TopAnchor, (float)margin.Top));
                constraints.Add(view.LeftAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.LeftAnchor, (float)margin.Left));
                break;
            case MaterialFloatingButtonPosition.BottomLeft:
                constraints.Add(view.BottomAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.BottomAnchor, -1*(float)margin.Bottom));
                constraints.Add(view.LeftAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.LeftAnchor, (float)margin.Left));
                break;
        }
        
        NSLayoutConstraint.ActivateConstraints([.. constraints]);
    }
    
    public static void SetMargin(this UIView view, UIWindow window, Thickness margin, SnackbarPosition position)
    {
        var constraints = new List<NSLayoutConstraint>
        {
            view.LeadingAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.LeadingAnchor, (float)margin.Left),
            view.TrailingAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TrailingAnchor, -1 * (float)margin.Right)
        };

        switch (position)
        {
            case SnackbarPosition.Top:
                constraints.Add(view.TopAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.TopAnchor, (float)margin.Top));
                break;
            case SnackbarPosition.Bottom:
                constraints.Add(view.BottomAnchor.ConstraintEqualTo(window.SafeAreaLayoutGuide.BottomAnchor, -1*(float)margin.Bottom));
                break;
        }
        
        NSLayoutConstraint.ActivateConstraints([.. constraints]);
    }
    
    public static void SetSize(this UIView view, double height, double width)
    {
        NSLayoutConstraint.ActivateConstraints(
        [
            view.LeftAnchor.ConstraintEqualTo(view.LeftAnchor, (float)width/4),
            view.RightAnchor.ConstraintEqualTo(view.RightAnchor, -(float)width/4),
            view.TopAnchor.ConstraintEqualTo(view.TopAnchor, (float)height/4),
            view.BottomAnchor.ConstraintEqualTo(view.BottomAnchor, -(float)height/4),
        ]);
    }
}