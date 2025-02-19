using Android.Graphics.Drawables;
using Android.OS;
using Android.Widget;
using HorusStudio.Maui.MaterialDesignControls;

namespace Android.Views;

static partial class ViewExtensions
{
    public static View SetRoundedBackground(this View view, Color backgroundColor, float cornerRadius = 0)
    {
        var backgroundDrawable = new GradientDrawable();
        backgroundDrawable.SetColor(backgroundColor.ToInt());
        backgroundDrawable.SetCornerRadius(cornerRadius);
        view.Background = backgroundDrawable;
        
        return view;
    }
    
    public static View SetMargin(this View view, Thickness margin, Thickness? insets = null)
    {
        if (view.LayoutParameters is not ViewGroup.MarginLayoutParams layoutParams) return view;
        
        layoutParams.SetMargins(
            Convert.ToInt32(margin.Left).DpToPixels() + Convert.ToInt32(insets?.Left ?? 0),
            Convert.ToInt32(margin.Top).DpToPixels() + Convert.ToInt32(insets?.Top ?? 0),
            Convert.ToInt32(margin.Right).DpToPixels() + Convert.ToInt32(insets?.Right ?? 0),
            Convert.ToInt32(margin.Bottom).DpToPixels() + Convert.ToInt32(insets?.Bottom ?? 0));
            
        view.LayoutParameters = layoutParams;
        return view;
    }

    public static View SetPadding(this View view, Thickness padding)
    {
        view.SetPadding(
            Convert.ToInt32(padding.Left).DpToPixels(), 
            Convert.ToInt32(padding.Top).DpToPixels(), 
            Convert.ToInt32(padding.Right).DpToPixels(), 
            Convert.ToInt32(padding.Bottom).DpToPixels());
        
        return view;
    }
    
    public static View SetSize(this View view, double height, double width)
    {
        if (view.LayoutParameters == null) return view;
        view.LayoutParameters.Height = Convert.ToInt32(height).DpToPixels();
        view.LayoutParameters.Width = Convert.ToInt32(width).DpToPixels();
        return view;
    }
    
    public static View SetGravity(this View view, MaterialFloatingButtonPosition position)
    {
        var gravityFlags = position switch
        {
            MaterialFloatingButtonPosition.TopLeft => GravityFlags.Top | GravityFlags.Left,
            MaterialFloatingButtonPosition.TopRight => GravityFlags.Top | GravityFlags.Right,
            MaterialFloatingButtonPosition.BottomLeft => GravityFlags.Bottom | GravityFlags.Left,
            MaterialFloatingButtonPosition.BottomRight => GravityFlags.Bottom | GravityFlags.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, "FAB position value is not valid.")
        };
        
        switch (view.LayoutParameters)
        {
            case FrameLayout.LayoutParams frameLayoutParams:
                frameLayoutParams.Gravity = gravityFlags;
                view.LayoutParameters = frameLayoutParams;
                break;
            case LinearLayout.LayoutParams linearLayoutParams:
                linearLayoutParams.Gravity = gravityFlags;
                view.LayoutParameters = linearLayoutParams;
                break;
        }
        
        return view;
    }
    
    public static View SetGravity(this View view, MaterialSnackbarPosition position)
    {
        var gravityFlags = position switch
        {
            MaterialSnackbarPosition.Bottom => GravityFlags.CenterHorizontal | GravityFlags.Bottom,
            MaterialSnackbarPosition.Top => GravityFlags.CenterHorizontal | GravityFlags.Top,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, "Snackbar position value is not valid.")
        };
        
        switch (view.LayoutParameters)
        {
            case FrameLayout.LayoutParams frameLayoutParams:
                frameLayoutParams.Gravity = gravityFlags;
                view.LayoutParameters = frameLayoutParams;
                break;
            case LinearLayout.LayoutParams linearLayoutParams:
                linearLayoutParams.Gravity = gravityFlags;
                view.LayoutParameters = linearLayoutParams;
                break;
        }
        
        return view;
    }
    
    public static View SetGravity(this View view, LayoutOptions horizontalLayoutOptions, LayoutOptions verticalLayoutOptions)
    {
        var horizontalMappings = new Dictionary<LayoutOptions, GravityFlags>()
        {
            { LayoutOptions.Fill, GravityFlags.FillHorizontal },
            { LayoutOptions.Start, GravityFlags.Left },
            { LayoutOptions.End, GravityFlags.Right },
            { LayoutOptions.Center, GravityFlags.CenterHorizontal }
        };
        var verticalMappings = new Dictionary<LayoutOptions, GravityFlags>()
        {
            { LayoutOptions.Fill, GravityFlags.FillVertical },
            { LayoutOptions.Start, GravityFlags.Top },
            { LayoutOptions.End, GravityFlags.Bottom },
            { LayoutOptions.Center, GravityFlags.CenterVertical }
        };
        
        var gravityFlags = horizontalMappings[horizontalLayoutOptions] | verticalMappings[verticalLayoutOptions];
        switch (view.LayoutParameters)
        {
            case FrameLayout.LayoutParams frameLayoutParams:
                frameLayoutParams.Gravity = gravityFlags;
                view.LayoutParameters = frameLayoutParams;
                break;
            case LinearLayout.LayoutParams linearLayoutParams:
                linearLayoutParams.Gravity = gravityFlags;
                view.LayoutParameters = linearLayoutParams;
                break;
        }
        
        return view;
    }

    public static Thickness GetInsets(this View view)
    {
        var insets = new Thickness();
        
        var androidVersion = Build.VERSION.SdkInt;
        if (androidVersion >= BuildVersionCodes.Q && view?.RootWindowInsets?.StableInsets is { } stableInsets)
        {
            insets = new Thickness(stableInsets.Left, stableInsets.Top, stableInsets.Right, stableInsets.Bottom);
        }
        
        return insets;
    } 
    
    public static void SetVisibility(this View view, bool isVisible, long animationDuration = 0)
    {
        var visibility = isVisible ? 1f : 0f;
        if (animationDuration > 0)
        {
            view.Animate()?.Alpha(visibility).SetDuration(animationDuration).Start();    
        }
        else
        {
            view.Alpha = visibility;
        }
    }
}