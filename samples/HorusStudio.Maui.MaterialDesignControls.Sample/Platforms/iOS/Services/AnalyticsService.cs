using Firebase.Analytics;
using HorusStudio.Maui.MaterialDesignControls.Sample.Services;
using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.iOS.Services;

public class AnalyticsService : IAnalyticsService
{
    public void LogEvent(string name, Dictionary<string, string>? parameters = null)
    {
        try
        {
            var bundle = new Dictionary<object, object>();

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                    bundle.Add(parameter.Key, parameter.Value);
            }

            Analytics.LogEvent(name, bundle);
        }
        catch (Exception ex)
        {
            Logger.Log($"Error logging event on firebase: {ex.Message}");
        }
    }
}