#if IOS
using UIKit;
using Microsoft.Maui.Platform;
#endif
#if ANDROID
using Platform = Microsoft.Maui.ApplicationModel.Platform;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class SnackbarImplemetation
{
    public virtual partial IDisposable ShowSnackbar(SnackbarConfig config)
    {
#if IOS
        Snackbar bar = null;
        var app = UIApplication.SharedApplication;

        app.SafeInvokeOnMainThread(() =>
        {
            bar = new Snackbar()
            {
                Message = config.Message,
                IconLeading = config.LeadingIcon,
                IconTrailing = config.TrailingIcon,
                MessageFontSize = (float)config.MessageFontSize,
                CornerRadius = config.CornerRadius,
                DismissDuration = config.Duration,
                FontFamily = config.MessageFontFamily,
                CancelButtonFontFamily = config.NegativeButtonFontFamily,
                Position = config.Position.ToNative(),
                ActionText = config.ActionText,
                Action = () =>
                {
                    config.Action?.Invoke(SnackbarActionType.UserInteraction);
                }
            };
            bar.BackgroundColor = config.BackgroundColor?.ToPlatform() ?? bar.BackgroundColor;
            bar.MessageColor = config.MessageColor?.ToPlatform() ?? bar.MessageColor;
            bar.ActionColor = config.NegativeButtonTextColor?.ToPlatform() ?? bar.ActionColor;
            bar.Show();
            bar.Timeout += (s, a) =>
            {
                config.Action?.Invoke(SnackbarActionType.Timeout);
            };
        });

        return new DisposableAction(() => app.SafeInvokeOnMainThread(() =>
        {
            bar.Dismiss();
            config.Action?.Invoke(SnackbarActionType.Cancelled);
        }));
#endif
#if ANDROID
        Google.Android.Material.Snackbar.Snackbar snackBar = null;
        var activity = Platform.CurrentActivity;
        activity.SafeRunOnUi(() =>
        {
            snackBar = new SnackbarBuilder(activity, config).Build();

            snackBar.Show();
        });
        return new DisposableAction(() =>
        {
            if (snackBar.IsShown)
                activity.SafeRunOnUi(() =>
                {
                    snackBar.Dismiss();
                    config.Action?.Invoke(SnackbarActionType.Cancelled);
                });
        });
#endif

        return null;
    }
}

public partial class SnackbarImplemetation : ISnackbarUser
{
    const string _noAction = "Action should not be set as async will not use it";
    public virtual partial IDisposable ShowSnackbar(SnackbarConfig config);

    public virtual IDisposable ShowSnackbar(string message, string leadingIcon, string trailingIcon,
        TimeSpan? dismissTimer, string actionText, Action<SnackbarActionType> action)
        => ShowSnackbar(new SnackbarConfig()
        {
            Message = message,
            LeadingIcon = leadingIcon,
            TrailingIcon = trailingIcon,
            Duration = dismissTimer ?? SnackbarConfig.DefaultDuration,
            Action = action,
            ActionText = actionText
        });

    public virtual Task<SnackbarActionType> ShowSnackbarAsync(string message, string leadingIcon, string trailingIcon,
        TimeSpan? dismissTimer, string actionText, CancellationToken? cancelToken)
        => ShowSnackbarAsync(new SnackbarConfig()
        {
            Message = message,
            LeadingIcon = leadingIcon,
            TrailingIcon = trailingIcon,
            Duration = dismissTimer ?? SnackbarConfig.DefaultDuration,
            ActionText = actionText
        }, cancelToken);

    public virtual async Task<SnackbarActionType> ShowSnackbarAsync(SnackbarConfig config, CancellationToken? cancelToken)
    {
        if (config.Action is not null)
            throw new ArgumentException(_noAction);

        var tcs = new TaskCompletionSource<SnackbarActionType>();
        config.SetAction(x => tcs.TrySetResult(x));

        var disp = this.ShowSnackbar(config);
        using (cancelToken?.Register(() => Cancel(disp, tcs)))
        {
            return await tcs.Task;
        }
    }
    
    static void Cancel<TResult>(IDisposable disp, TaskCompletionSource<TResult> tcs)
    {
        disp.Dispose();
        tcs.TrySetCanceled();
    }
}

public interface ISnackbarUser
{
    IDisposable ShowSnackbar(string message, string iconLeading = null, string iconTrailing = null, TimeSpan? dismissTimer = null, string actionText = null, Action<SnackbarActionType> action = null);
    IDisposable ShowSnackbar(SnackbarConfig config);
    
    Task<SnackbarActionType> ShowSnackbarAsync(string message, string iconLeading = null, string iconTrailing = null, TimeSpan? dismissTimer = null, string actionText = null, CancellationToken? cancelToken = null);
    Task<SnackbarActionType> ShowSnackbarAsync(SnackbarConfig config, CancellationToken? cancelToken = null);
}