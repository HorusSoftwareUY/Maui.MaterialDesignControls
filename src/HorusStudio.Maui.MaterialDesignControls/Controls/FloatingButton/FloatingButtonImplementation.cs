using HorusStudio.Maui.MaterialDesignControls.Extensions;
#if IOS
using UIKit;
using Microsoft.Maui.Platform;
#endif
#if ANDROID
using Platform = Microsoft.Maui.ApplicationModel.Platform;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class FloatingButtonImplementation
{
    #if ANDROID
    private Google.Android.Material.Snackbar.Snackbar layout;
    #endif

    #if IOS
    private FloatingButtonBuilder_MaciOS layout;
    #endif
    
    public virtual IDisposable ShowFloatingButton(FloatingButtonConfig config)
    {
#if IOS
        var app = UIApplication.SharedApplication;
        app.SafeInvokeOnMainThread(() =>
        {
            layout?.Dismiss();
            layout = new FloatingButtonBuilder_MaciOS(config);
            layout.Show();
        });
        return new DisposableAction(() => app.SafeInvokeOnMainThread(() =>
        {
            layout.Dismiss();
        }));
#endif
#if ANDROID
        
        var activity = Platform.CurrentActivity;
        activity.SafeRunOnUi(() =>
        {
            layout?.Dismiss();
            layout?.Dispose();
            layout = new FloatingButtonBuilder_Android(activity, config).Build();
            layout.Show();
        });
        return new DisposableAction(() =>
        {
            if (layout != null && layout.IsShown)
                activity.SafeRunOnUi(() =>
                {
                    layout.Dismiss();
                });
        });
#endif
        return null;
    }


    public virtual void DismissFloatingButton()
    {
        #if ANDROID
        layout?.Dismiss();
        layout?.Dispose();
        #endif
        
        #if IOS
        layout?.Dismiss();
        layout?.Dispose();
        #endif
    }

}