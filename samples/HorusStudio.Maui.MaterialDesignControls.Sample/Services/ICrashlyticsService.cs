namespace HorusStudio.Maui.MaterialDesignControls.Sample.Services;

public interface ICrashlyticsService
{
    void InitCrashDetection();
        
    void LogException(Exception exception, Dictionary<string, string> properties = null);
}