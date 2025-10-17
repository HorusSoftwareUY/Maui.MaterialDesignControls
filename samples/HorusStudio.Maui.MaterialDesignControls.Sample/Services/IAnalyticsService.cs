namespace HorusStudio.Maui.MaterialDesignControls.Sample.Services;

public interface IAnalyticsService
{
    void LogEvent(string name, Dictionary<string, string> parameters = null);
}