using HorusStudio.Maui.MaterialDesignControls.Extensions;
#if IOS
using UIKit;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

class FloatingButtonImplementation : IDisposable
{
    private bool isDisposed;

#if ANDROID
    private Google.Android.Material.Snackbar.Snackbar layout;
#endif

#if IOS
    private FloatingButtonBuilder_MaciOS layout;
#endif

    public FloatingButtonImplementation()
    {
    }

    ~FloatingButtonImplementation() => Dispose(false);

    public IDisposable Show(FloatingButtonConfig config)
    {
#if ANDROID
        var activity = Platform.CurrentActivity;
        activity.SafeRunOnUi(() =>
        {
            Dismiss();
            layout = new FloatingButtonBuilder_Android(activity, config).Build();
            layout.Show();
        });
#endif
#if IOS
        var app = UIApplication.SharedApplication;
        app.SafeInvokeOnMainThread(() =>
        {
            Dismiss();
            layout = new FloatingButtonBuilder_MaciOS(config);
            layout.Show();
        });
#endif
        return this;
    }

    public void Dismiss()
    {
#if ANDROID
        layout?.Dismiss();
#endif
#if IOS
        layout?.Dismiss();
#endif
    } 
    
	public void Dispose()
    {
        Dismiss();
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
#if ANDROID
                layout?.Dispose();
#endif
#if IOS
                layout?.Dispose();
#endif
            }
            isDisposed = true;
        }
    }
}