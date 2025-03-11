using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace Android.App;

static class MainThreadExtensions
{
    public static void SafeRunOnUiThread(this Activity activity, Action action) => activity.RunOnUiThread(() =>
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
    
    public static Task SafeRunOnUiThreadAsync(this Activity activity, Func<Task> func)
    {
        TaskCompletionSource result = new();
        
        activity.RunOnUiThread(async () =>
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