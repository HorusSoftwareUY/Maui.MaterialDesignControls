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
    
    public static Task SafeInvokeOnMainThreadAsync(this UIApplication app, Func<Task> func)
    {
        TaskCompletionSource result = new();
        
        app.InvokeOnMainThread(async () =>
        {
            try
            {
                await func();
                result.SetResult();
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.ToString());
            }
        });
        
        return result.Task;
    }     
}