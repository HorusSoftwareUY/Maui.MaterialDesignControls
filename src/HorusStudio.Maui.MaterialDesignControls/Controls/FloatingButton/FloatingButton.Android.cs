using Android.App;
using Android.Views;
using Google.Android.Material.Snackbar;
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace HorusStudio.Maui.MaterialDesignControls;

class FloatingButton : Snackbar.Callback
{
    #region Attributes
    
    private readonly Snackbar? _snackbar;
    private readonly MaterialFloatingButton _fab;
    
    #endregion
    
    #region Constructors
    
    public FloatingButton(MaterialFloatingButton fab, Activity? activity)
    {
        _fab = fab;
        _snackbar = Build(fab, activity);
        _snackbar?.AddCallback(this);
    }

    #endregion

    public void Show()
    {
        try
        {
            Logger.Debug("Showing FAB");
            _snackbar?.Show();
            Logger.Debug("FAB showed");
        }
        catch (Exception ex)
        {
            Logger.LogException("ERROR showing FAB", ex, _fab);
            throw;
        }
    }

    public void Dismiss() 
    {
        try
        {
            Logger.Debug("Dismissing FAB");
            _snackbar?.Dismiss();
            Logger.Debug("FAB dismissed");
        }
        catch (Exception ex)
        {
            Logger.LogException("ERROR dismissing FAB", ex, _fab);
            throw;
        }
    }
    
    #region Snackbar.Callback

    public override void OnShown(Snackbar? control)
    {
        base.OnShown(control);
        try
        {
            control?.View?.Animate()?.Alpha(1f).SetDuration(300).Start();
        }
        catch (Exception ex)
        {
            Logger.LogException("ERROR showing FAB", ex, _fab);
            throw;
        }
    }

    public override void OnDismissed(Snackbar? control, int e)
    {
        base.OnDismissed(control, e);
        try
        {
            if (control == null) return;
            control.SetDuration(0);
            control.View.Animate()?.Alpha(1f).SetDuration(300).Start();    
        }
        catch (Exception ex)
        {
            Logger.LogException("ERROR dismissing FAB", ex, _fab);
            throw;
        }
    }

    #endregion
    
    private static Snackbar? Build(MaterialFloatingButton fab, Activity? activity)
    {
        ArgumentNullException.ThrowIfNull(fab);
        ArgumentNullException.ThrowIfNull(activity);

        Logger.Debug("Creating FAB");
        try
        {
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
                    .SetSize(fab.IconSize + fab.Padding.VerticalThickness,
                        fab.IconSize + fab.Padding.HorizontalThickness)
                    .SetGravity(fab.Position);

                var iconView = snackbarContent.AddIcon(activity, fab.Icon, Convert.ToInt32(fab.IconSize), fab.IconColor,
                    fab.Padding,
                    () =>
                    {
                        if (fab.IsEnabled && (fab.Command?.CanExecute(fab.CommandParameter) ?? false))
                        {
                            fab.Command?.Execute(fab.CommandParameter);
                        }
                    }, 0);

                if (iconView?.LayoutParameters != null)
                    iconView.LayoutParameters.Width = ViewGroup.LayoutParams.MatchParent;
                
                if (!string.IsNullOrEmpty(fab.AutomationId))
                    iconView.ContentDescription = fab.AutomationId;
            }

            Logger.Debug("FAB created");
            return snackbar;
        }
        catch (Exception ex)
        {
            Logger.LogException("ERROR creating FAB", ex, fab);
            return null;
        }
    }
}