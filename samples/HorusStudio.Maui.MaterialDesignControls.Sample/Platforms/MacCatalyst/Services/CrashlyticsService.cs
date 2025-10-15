using HorusStudio.Maui.MaterialDesignControls.Sample.Services;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.MacCatalyst.Services;

public class CrashlyticsService : ICrashlyticsService
{
    public void InitCrashDetection()
    {
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            if (e.ExceptionObject is System.Exception ex)
            {
                LogException(ex, new Dictionary<string, string> { {"CrashDetectionMethod", "AppDomain.CurrentDomain.UnhandledException"} });
            }
        };

        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            LogException(e.Exception, new Dictionary<string, string> { {"CrashDetectionMethod", "TaskScheduler.UnobservedTaskException"} });
            e.SetObserved();
        };
    }
    
    public void LogException(Exception exception, Dictionary<string, string>? properties = null)
    {
        // TODO: Implement LogException on MacCatalyst
    }
}