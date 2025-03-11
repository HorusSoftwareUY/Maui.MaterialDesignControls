using Android.App;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
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
        ArgumentNullException.ThrowIfNull(fab);
        ArgumentNullException.ThrowIfNull(activity);

        var rootView = activity!.Window!.DecorView.RootView;
        var snackbar = Snackbar.Make(
            activity,
            rootView!,
            string.Empty,
            -2
        );

        if (snackbar.View is Snackbar.SnackbarLayout snackbarView &&
            snackbarView.GetChildAt(0) is SnackbarContentLayout snackbarContent)
        {
            var insets = rootView!.GetInsets();
            
            snackbarView.Alpha = 0f;
            snackbarView
                .SetRoundedBackground(fab.BackgroundColor, (float)fab.CornerRadius)
                .SetMargin(fab.Margin, insets)
                .SetSize(fab.IconSize + fab.Padding.VerticalThickness, fab.IconSize + fab.Padding.HorizontalThickness)
                .SetGravity(fab.Position);

            var iconView = snackbarContent.AddIcon(activity, fab.Icon, Convert.ToInt32(fab.IconSize), fab.IconColor, fab.Padding,
                () =>
                {
                    if (fab.IsEnabled && (fab.Command?.CanExecute(fab.CommandParameter) ?? false))
                    {
                        fab.Command?.Execute(fab.CommandParameter);
                    }
                }, 0);
            
            if (iconView?.LayoutParameters != null) iconView.LayoutParameters.Width = ViewGroup.LayoutParams.MatchParent;
        }
        
        return snackbar;
    }
}