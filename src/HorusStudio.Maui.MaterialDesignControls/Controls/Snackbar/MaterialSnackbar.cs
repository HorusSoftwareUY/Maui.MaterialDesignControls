#if ANDROID
using Platform = Microsoft.Maui.ApplicationModel.Platform;
using Android.App;
#elif IOS || MACCATALYST
using UIKit;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

class MaterialSnackbar : IMaterialSnackbar, IDisposable
{
    private const string NoAction = "Action should not be set as async will not use it";
    private bool _isDisposed;
    
#if ANDROID
    private MaterialSnackbarBuilder? _snackbar;
#elif IOS || MACCATALYST
    private MaterialSnackbarBuilder? _snackbar;
#endif
    
    ~MaterialSnackbar() => Dispose(false);
    
    public IDisposable Show(MaterialSnackbarConfig config)
    {
#if ANDROID
        var activity = Platform.CurrentActivity!;
        activity.SafeRunOnUiThread(() =>
        {
            _snackbar = new MaterialSnackbarBuilder(activity, config);
            _snackbar.Show();
        });
        
#elif IOS || MACCATALYST
        var app = UIApplication.SharedApplication;
        app.SafeInvokeOnMainThread(() =>
        {
            _snackbar = new MaterialSnackbarBuilder(config);
            _snackbar.Show();
        });
#endif
        return this;
    }

    public IDisposable Show(string message, 
        TimeSpan? duration = null, 
        MaterialSnackbarConfig.ActionConfig? action = null, 
        MaterialSnackbarConfig.IconConfig? leadingIcon = null, 
        MaterialSnackbarConfig.IconConfig? trailingIcon = null)
        => Show(new MaterialSnackbarConfig(message)
        {
            LeadingIcon = leadingIcon,
            TrailingIcon = trailingIcon,
            Duration = duration ?? MaterialSnackbarConfig.DefaultDuration,
            Action = action
        });

    public Task ShowAsync(string message, 
        TimeSpan? duration = null, 
        MaterialSnackbarConfig.ActionConfig? action = null, 
        MaterialSnackbarConfig.IconConfig? leadingIcon = null, 
        MaterialSnackbarConfig.IconConfig? trailingIcon = null, 
        CancellationToken cancellationToken = default)
        => ShowAsync(new MaterialSnackbarConfig(message)
        {
            LeadingIcon = leadingIcon,
            TrailingIcon = trailingIcon,
            Duration = duration ?? MaterialSnackbarConfig.DefaultDuration,
            Action = action
        }, cancellationToken);

    public async Task ShowAsync(MaterialSnackbarConfig config, CancellationToken cancellationToken = default)
    {
        await using (cancellationToken.Register(() => _snackbar?.Dismiss()))
        {
#if ANDROID
            var activity = Platform.CurrentActivity!;
            await activity.SafeRunOnUiThreadAsync(() =>
            {
                _snackbar = new MaterialSnackbarBuilder(activity, config);
                return _snackbar.ShowAsync();
            });
#elif IOS || MACCATALYST
            var app = UIApplication.SharedApplication;
            await app.SafeInvokeOnMainThreadAsync(() =>
            {
                _snackbar = new MaterialSnackbarBuilder(config);
                return _snackbar.ShowAsync();
            });
#endif
        }
    }
    
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        if (disposing)
        {
#if ANDROID
            _snackbar?.Dispose();
#elif IOS || MACCATALYST
            _snackbar?.Dispose();
#endif
        }
        _isDisposed = true;
    }
}