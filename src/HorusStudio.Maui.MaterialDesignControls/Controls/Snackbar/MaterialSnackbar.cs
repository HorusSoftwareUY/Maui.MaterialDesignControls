#if IOS || MACCATALYST
using UIKit;
using Microsoft.Maui.Platform;
#endif

#if ANDROID
using Platform = Microsoft.Maui.ApplicationModel.Platform;
using Android.App;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

class MaterialSnackbar : IMaterialSnackbar
{
    private const string NoAction = "Action should not be set as async will not use it";

    public IDisposable ShowSnackbar(SnackbarConfig config)
    {
#if IOS || MACCATALYST
        MaterialSnackbarBuilder bar = null;
        var app = UIApplication.SharedApplication;

        app.SafeInvokeOnMainThread(() =>
        {
            bar = new MaterialSnackbarBuilder(config);
            bar.Show();
        });

        return new DisposableAction(() => app.SafeInvokeOnMainThread(() =>
        {
            bar.Dismiss();
            config.DimissAction?.Invoke();
        }));
#endif
#if ANDROID
        MaterialSnackbarBuilder snackBar = null;
        var activity = Platform.CurrentActivity;
        activity.SafeRunOnUi(() =>
        {
            snackBar = new MaterialSnackbarBuilder(activity, config);
            snackBar.Show();
        });
        return snackBar;
#endif
        return null;
    }

    public IDisposable ShowSnackbar(string message, string leadingIcon, string trailingIcon,
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

    public Task ShowSnackbarAsync(string message, string leadingIcon, string trailingIcon,
        TimeSpan? dismissTimer, string actionText, CancellationToken? cancelToken)
        => ShowSnackbarAsync(new SnackbarConfig(message)
        {
            LeadingIcon = new SnackbarConfig.IconConfig(leadingIcon),
            TrailingIcon = new SnackbarConfig.IconConfig (trailingIcon),
            Duration = dismissTimer ?? SnackbarConfig.DefaultDuration,
            Action = new SnackbarConfig.ActionConfig(actionText)
        }, cancelToken);

    public async Task ShowSnackbarAsync(SnackbarConfig config, CancellationToken? cancelToken)
    {
        if (config.Action is not null || 
            config.LeadingIcon?.Action is not null || 
            config.TrailingIcon?.Action is not null)
            throw new ArgumentException(NoAction);
    }
}