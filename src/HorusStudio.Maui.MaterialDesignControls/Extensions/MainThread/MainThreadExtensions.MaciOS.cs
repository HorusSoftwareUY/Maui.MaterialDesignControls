using System.Runtime.CompilerServices;
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace UIKit;

static class MainThreadExtensions
{
    public static void SafeInvokeOnMainThread(this UIApplication app, Action action, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null)
    {
        app.InvokeOnMainThread(() =>
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
    
    public static Task SafeInvokeOnMainThreadAsync(this UIApplication app, Func<Task> func, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null)
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
                Logger.LogException(ex, callerFilePath: callerFilePath, callerMemberName: callerMemberName);
            }
        });
        
        return result.Task;
    }     
}