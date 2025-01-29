#if IOS || MACCATALYST
using UIKit;
#endif
#if ANDROID
using Android.App;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

class FloatingButtonImplementation : IDisposable
{
    private bool _isDisposed;

#if ANDROID
    private Google.Android.Material.Snackbar.Snackbar? _layout;
#endif

#if IOS || MACCATALYST
    private FloatingButtonBuilder_MaciOS _layout;
#endif
    
    ~FloatingButtonImplementation() => Dispose(false);

    public IDisposable Show(FloatingButtonConfig config)
    {
#if ANDROID
        var activity = Platform.CurrentActivity;
        activity?.SafeRunOnUi(() =>
        {
            Dismiss();
            _layout = new FloatingButtonBuilder().Build(activity, config);
            _layout.Show();
        });
#endif
#if IOS
        var app = UIApplication.SharedApplication;
        app.SafeInvokeOnMainThread(() =>
        {
            Dismiss();
            _layout = new FloatingButtonBuilder_MaciOS(config);
            _layout.Show();
        });
#endif
        return this;
    }

    public void Dismiss()
    {
#if ANDROID
        _layout?.Dismiss();
#endif
#if IOS
        _layout?.Dismiss();
#endif
    } 
    
	public void Dispose()
    {
        Dismiss();
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        if (disposing)
        {
#if ANDROID
            _layout?.Dispose();
#endif
#if IOS
                _layout?.Dispose();
#endif
        }
        _isDisposed = true;
    }
}