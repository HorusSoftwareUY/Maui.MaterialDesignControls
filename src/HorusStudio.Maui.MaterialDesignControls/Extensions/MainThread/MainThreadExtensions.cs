using System.Runtime.CompilerServices;
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace HorusStudio.Maui.MaterialDesignControls;

static class MainThreadExtensions
{
    public static bool IsMainThread => Microsoft.Maui.ApplicationModel.MainThread.IsMainThread;

    public static void SafeRunOnUiThread(Action action, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null)
    {
        Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
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

    public static Task SafeRunOnUiThreadAsync(Func<Task> func, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null)
    {
        TaskCompletionSource result = new();

        Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(async () =>
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