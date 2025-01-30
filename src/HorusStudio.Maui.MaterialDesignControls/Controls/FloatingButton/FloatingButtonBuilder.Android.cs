using Android.App;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Snackbar;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;

class FloatingButtonBuilder : Snackbar.Callback
{
    #region Attributes
    
    private readonly Snackbar? _snackbar;
    
    #endregion
    
    #region Constructors
    
    public FloatingButtonBuilder(MaterialFloatingButton fab, Activity? activity)
    {
        _snackbar = Build(fab, activity);
        _snackbar.AddCallback(this);
    }

    #endregion

    public void Show() => _snackbar?.Show();

    public void Dismiss() => _snackbar?.Dismiss();
    
    #region Snackbar.Callback

    public override void OnShown(Snackbar? control)
    {
        base.OnShown(control);
        control?.View?.Animate()?.Alpha(1f).SetDuration(300).Start();
    }

    public override void OnDismissed(Snackbar? control, int e)
    {
        base.OnDismissed(control, e);
        control?.SetDuration(0);
        control?.View?.Animate()?.Alpha(1f).SetDuration(300).Start();
    }

    #endregion
    
    private static Snackbar Build(MaterialFloatingButton fab, Activity? activity)
    {
        ArgumentNullException.ThrowIfNull(activity);
        ArgumentNullException.ThrowIfNull(fab);

        var rootView = activity!.Window!.DecorView.RootView;
        var snackbar = Snackbar.Make(
            activity,
            rootView!,
            string.Empty,
            -2
        );

        if (snackbar.View is Snackbar.SnackbarLayout snackbarView)
        {
            var insets = new Thickness();
            var androidVersion = Android.OS.Build.VERSION.SdkInt;
            if (androidVersion >= BuildVersionCodes.Q && rootView!.RootWindowInsets?.StableInsets is { } stableInsets)
            {
                insets = new Thickness(stableInsets.Left, stableInsets.Top, stableInsets.Right, stableInsets.Bottom);
            }
            
            snackbarView.Background = CreateRoundedBackground(fab.BackgroundColor, (float)fab.CornerRadius);
            snackbarView.Alpha = 0f;
            snackbarView
                .SetMargin(fab.Margin, insets)
                .SetSize(fab.IconSize + fab.Padding.VerticalThickness, fab.IconSize + fab.Padding.HorizontalThickness)
                .SetGravity(fab.Position);

            var iconSize = fab.IconSize;
            var icon = fab.Icon.ToDrawable(iconSize, fab.IconColor);
            if (icon != null)
            {
                var button = new Android.Widget.ImageButton(activity);
                button.Background = new ColorDrawable(Colors.Transparent.ToPlatform());
                button.SetImageDrawable(icon);
                button.SetPadding(fab.Padding);
            
                button.Click += (sender, args) =>
                {
                    if (fab.IsEnabled && (fab.Command?.CanExecute(fab.CommandParameter) ?? false))
                    {
                        fab.Command?.Execute(fab.CommandParameter);
                    }
                };
            
                if (snackbarView.GetChildAt(0) is SnackbarContentLayout snackbarContent)
                {
                    snackbarContent.AddView(button,0);
                }
            
                if (button.LayoutParameters != null) button.LayoutParameters.Width = ViewGroup.LayoutParams.MatchParent;
            }
        }
        
        return snackbar;
    }
    
    private static Drawable CreateRoundedBackground(Microsoft.Maui.Graphics.Color backgroundColor, float cornerRadius)
    {
        var backgroundDrawable = new GradientDrawable();
        backgroundDrawable.SetColor(backgroundColor.ToInt());
        backgroundDrawable.SetCornerRadius(cornerRadius);
        return backgroundDrawable;
    }
}

static class ViewExtensions
{
    public static Android.Views.View SetMargin(this Android.Views.View view, Thickness margin, Thickness? insets = null)
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

    public static Android.Views.View SetPadding(this Android.Views.View view, Thickness padding)
    {
        view.SetPadding(
            Convert.ToInt32(padding.Left).DpToPixels(), 
            Convert.ToInt32(padding.Top).DpToPixels(), 
            Convert.ToInt32(padding.Right).DpToPixels(), 
            Convert.ToInt32(padding.Bottom).DpToPixels());
        
        return view;
    }
    
    public static Android.Views.View SetSize(this Android.Views.View view, double height, double width)
    {
        if (view.LayoutParameters == null) return view;
        view.LayoutParameters.Height = Convert.ToInt32(height).DpToPixels();
        view.LayoutParameters.Width = Convert.ToInt32(width).DpToPixels();
        return view;
    }
    
    public static Android.Views.View SetGravity(this Android.Views.View view, MaterialFloatingButtonPosition position)
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
}