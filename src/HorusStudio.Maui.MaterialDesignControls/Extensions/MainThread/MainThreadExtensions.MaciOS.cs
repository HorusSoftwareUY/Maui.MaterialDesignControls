using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace UIKit;

static class MainThreadExtensions
{
    public static void SafeInvokeOnMainThread(this UIApplication app, Action action) => app.InvokeOnMainThread(() =>
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Logger.Debug(ex.ToString());
        }
    });
}