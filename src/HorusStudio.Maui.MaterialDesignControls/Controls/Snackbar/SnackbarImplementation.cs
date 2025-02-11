#if IOS
using UIKit;
using Microsoft.Maui.Platform;
#endif

#if ANDROID
using Platform = Microsoft.Maui.ApplicationModel.Platform;
using Android.App;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class SnackbarImplementation
{
    public virtual partial IDisposable ShowSnackbar(SnackbarConfig config)
    {
#if IOS
        Snackbar bar = null;
        var app = UIApplication.SharedApplication;

        app.SafeInvokeOnMainThread(() =>
        {
            bar = new Snackbar(config);
            bar.Show();
        });

        return new DisposableAction(() => app.SafeInvokeOnMainThread(() =>
        {
            bar.Dismiss();
            config.DimissAction?.Invoke();
        }));
#endif
#if ANDROID
        SnackbarBuilder snackBar = null;
        var activity = Platform.CurrentActivity;
        activity.SafeRunOnUi(() =>
        {
            snackBar = new SnackbarBuilder(activity, config);
            snackBar.Show();
        });
        return snackBar;
#endif
        return null;
    }
}

public partial class SnackbarImplementation : ISnackbarUser
{
    const string _noAction = "Action should not be set as async will not use it";
    public virtual partial IDisposable ShowSnackbar(SnackbarConfig config);

    public virtual IDisposable ShowSnackbar(string message, string leadingIcon, string trailingIcon,
        TimeSpan? dismissTimer, string actionText, Action action, Action actionLeading, Action actionTrailing)
        => ShowSnackbar(new SnackbarConfig(message)
        {
            LeadingIcon = new SnackbarConfig.IconConfig(leadingIcon)
            {
              Action = actionLeading
            },
            TrailingIcon = new SnackbarConfig.IconConfig(trailingIcon)
            {
                Action = actionTrailing
            },
            Duration = dismissTimer ?? SnackbarConfig.DefaultDuration,
            Action = new SnackbarConfig.ActionConfig (actionText)
            {
                Action = action
            }
        });

    public virtual Task ShowSnackbarAsync(string message, string leadingIcon, string trailingIcon,
        TimeSpan? dismissTimer, string actionText, CancellationToken? cancelToken)
        => ShowSnackbarAsync(new SnackbarConfig(message)
        {
            LeadingIcon = new SnackbarConfig.IconConfig(leadingIcon),
            TrailingIcon = new SnackbarConfig.IconConfig (trailingIcon),
            Duration = dismissTimer ?? SnackbarConfig.DefaultDuration,
            Action = new SnackbarConfig.ActionConfig(actionText)
        }, cancelToken);

    public virtual async Task ShowSnackbarAsync(SnackbarConfig config, CancellationToken? cancelToken)
    {
        if (config.Action is not null || 
            config.LeadingIcon?.Action is not null || 
            config.TrailingIcon?.Action is not null)
            throw new ArgumentException(_noAction);
    }
}

public interface ISnackbarUser
{
    IDisposable ShowSnackbar(string message, string iconLeading = null, string iconTrailing = null, TimeSpan? dismissTimer = null, string actionText = null, Action action = null, Action actionLeading = null, Action actionTrailing = null);
    IDisposable ShowSnackbar(SnackbarConfig config);
    
    Task ShowSnackbarAsync(string message, string iconLeading = null, string iconTrailing = null, TimeSpan? dismissTimer = null, string actionText = null, CancellationToken? cancelToken = null);
    Task ShowSnackbarAsync(SnackbarConfig config, CancellationToken? cancelToken = null);
}