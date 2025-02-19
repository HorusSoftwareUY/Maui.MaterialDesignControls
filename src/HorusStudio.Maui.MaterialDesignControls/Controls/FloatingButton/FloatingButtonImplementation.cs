#if IOS || MACCATALYST
using UIKit;
#elif ANDROID
using Android.App;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

class FloatingButtonImplementation : IDisposable
{
    private bool _isDisposed;

#if ANDROID
    private FloatingButtonBuilder? _layout;
#elif IOS || MACCATALYST
    private FloatingButtonBuilder? _layout;
#endif

    public FloatingButtonImplementation(MaterialFloatingButton fab)
    {
#if ANDROID
        _layout = new FloatingButtonBuilder(fab, Platform.CurrentActivity);
#elif IOS || MACCATALYST
        _layout = new FloatingButtonBuilder(fab);
#endif
    }
    
    
    ~FloatingButtonImplementation() => Dispose(false);

    public IDisposable Show()
    {
#if ANDROID
        var activity = Platform.CurrentActivity;
        activity?.SafeRunOnUiThread(() =>
        {
            Dismiss();
            _layout?.Show();
        });
#elif IOS || MACCATALYST
        var app = UIApplication.SharedApplication;
        app.SafeInvokeOnMainThread(() =>
        {
            _layout?.Show();
        });
#endif
        return this;
    }
    
    public void Dismiss()
    {
#if ANDROID
        _layout?.Dismiss();
#elif IOS || MACCATALYST
        _layout?.Dismiss();
#endif
    } 
    
	public void Dispose()
    {
        Dismiss();
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        if (disposing)
        {
#if ANDROID
            _layout?.Dispose();
#elif IOS || MACCATALYST
            _layout?.Dispose();
#endif
        }
        _isDisposed = true;
    }
}