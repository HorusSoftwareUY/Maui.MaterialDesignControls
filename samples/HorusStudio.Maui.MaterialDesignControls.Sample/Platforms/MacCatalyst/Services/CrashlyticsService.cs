using HorusStudio.Maui.MaterialDesignControls.Sample.Services;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.MacCatalyst.Services;

public class CrashlyticsService : ICrashlyticsService
{
    public void InitCrashDetection()
    {
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