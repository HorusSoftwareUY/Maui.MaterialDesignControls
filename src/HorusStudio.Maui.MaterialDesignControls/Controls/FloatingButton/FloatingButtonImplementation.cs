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
    private FloatingButtonConfig lastConfig;
    #endif
    
    public virtual IDisposable ShowFloatingButton(FloatingButtonConfig config)
    {
#if IOS
        /*Snackbar bar = null;
        var app = UIApplication.SharedApplication;

        app.SafeInvokeOnMainThread(() =>
        {
            bar = new Snackbar(config);
            bar.Show();
        });

        return new DisposableAction(() => app.SafeInvokeOnMainThread(() =>
        {
            bar.Dismiss();
        }));*/
#endif
#if ANDROID
        
        var activity = Platform.CurrentActivity;
        activity.SafeRunOnUi(() =>
        {
            if (layout == null || lastConfig != config)
            {
                layout?.Dispose();
                layout = new FloatingButtonBuilder(activity, config).Build();
                lastConfig = config;
            }
                
            if (!layout.IsShown) layout.Show();
        });
        return new DisposableAction(() =>
        {
            if (layout.IsShown)
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
    }

}