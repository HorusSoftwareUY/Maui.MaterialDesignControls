using Android.Runtime;
using HorusStudio.Maui.MaterialDesignControls.Sample.Services;
using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Android.Services;

public class CrashlyticsService : ICrashlyticsService
{
    public void InitCrashDetection()
    {
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            LogException(e.Exception, new Dictionary<string, string> { {"CrashDetectionMethod", "TaskScheduler.UnobservedTaskException"} });
            e.SetObserved();
        };
            
        AndroidEnvironment.UnhandledExceptionRaiser += (sender, e) =>
        {
            LogException(e.Exception, new Dictionary<string, string> { {"CrashDetectionMethod", "AndroidEnvironment.UnhandledExceptionRaiser"} });
            e.Handled = true;
        };
    }
        
    public void LogException(Exception exception, Dictionary<string, string>? properties = null)
    {
        try
        {
            Firebase.Crashlytics.FirebaseCrashlytics.Instance.Log($"ExceptionSource: {exception.Source}");
            
            if (properties != null && properties.Any())
            {
                foreach (var property in properties)
                {
                    try
                    {
                        Firebase.Crashlytics.FirebaseCrashlytics.Instance.Log($"ExceptionProperty {property.Key}: {property.Value}");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogInfo($"Error logging exception log on firebase: {ex.Message}");
                    }
                }
            }

            var error = Java.Lang.Throwable.FromException(exception);
            error.Source = exception.Source;
            Firebase.Crashlytics.FirebaseCrashlytics.Instance.RecordException(error);
            Firebase.Crashlytics.FirebaseCrashlytics.Instance.SendUnsentReports();
        }
        catch (Exception ex)
        {
            Logger.LogInfo($"Error logging event on firebase: {ex.Message}");
        }
    }
}