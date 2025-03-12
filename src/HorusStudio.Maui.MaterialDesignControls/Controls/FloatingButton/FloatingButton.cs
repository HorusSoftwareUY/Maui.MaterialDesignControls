#if IOS || MACCATALYST
using UIKit;
#elif ANDROID
using Android.App;
#endif
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace HorusStudio.Maui.MaterialDesignControls;

class FloatingButtonImplementation : IDisposable
{
    private bool _isDisposed;
    private bool _isShowing;
    private readonly MaterialFloatingButton _fab;
    
#if ANDROID
    private readonly FloatingButton? _layout;
#elif IOS || MACCATALYST
    private readonly FloatingButton? _layout;
#endif

    public FloatingButtonImplementation(MaterialFloatingButton fab)
    {
        _fab = fab;
#if ANDROID
        _layout = new FloatingButton(fab, Platform.CurrentActivity);
#elif IOS || MACCATALYST
        _layout = new FloatingButton(fab);
#endif
    }
    
    ~FloatingButtonImplementation() => Dispose(false);

    public IDisposable Show()
    {
        if (_isShowing) return this;
#if ANDROID
        var activity = Platform.CurrentActivity;
        activity?.SafeRunOnUiThread(() =>
        {
            _layout?.Show();
            _isShowing = true;
        });
#elif IOS || MACCATALYST
        var app = UIApplication.SharedApplication;
        app.SafeInvokeOnMainThread(() =>
        {
            _layout?.Show();
            _isShowing = true;
        });
#endif
        return this;
    }
    
    public void Dismiss()
    {
        try
        {
#if ANDROID
            _layout?.Dismiss();
#elif IOS || MACCATALYST
            _layout?.Dismiss();
#endif
            _isShowing = false;
        }
        catch (Exception ex)
        {
            Logger.LogException("ERROR dismissing FAB", ex, _fab);
        }
    } 
    
	public void Dispose()
    {
        try
        {
            Dismiss();
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        catch (Exception ex)
        {
            Logger.LogException("ERROR disposing FAB", ex, _fab);
        }
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