using System.Runtime.CompilerServices;
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace Android.App;

static class MainThreadExtensions
{
    public static void SafeRunOnUiThread(this Activity activity, Action action, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null)
    {
        activity.RunOnUiThread(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, callerFilePath: callerFilePath, callerMemberName: callerMemberName);
            }
        });
    }
    
    public static Task SafeRunOnUiThreadAsync(this Activity activity, Func<Task> func, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null)
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
                Logger.LogException(ex, callerFilePath: callerFilePath, callerMemberName: callerMemberName);
            }
        });
        
        return result.Task;
    }     
}