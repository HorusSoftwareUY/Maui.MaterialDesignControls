using Android.OS;
using Firebase.Analytics;
using HorusStudio.Maui.MaterialDesignControls.Sample.Services;
using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Android.Services;

public class AnalyticsService : IAnalyticsService
{
    private FirebaseAnalytics _analyticsInstance;
    
    private FirebaseAnalytics GetAnalytics()
    {
        if (_analyticsInstance != null)
            return _analyticsInstance;
    
        var activity = Platform.CurrentActivity;
    
        _analyticsInstance = FirebaseAnalytics.GetInstance(activity);
    
        return _analyticsInstance;
    }

    public void LogEvent(string name, Dictionary<string, string>? parameters = null)
    {
        try
        {
            var bundle = new Bundle();
        
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                    bundle.PutString(parameter.Key, parameter.Value);
            }
        
            GetAnalytics()?.LogEvent(name, bundle);
        }
        catch (Exception ex)
        {
            Logger.LogInfo($"Error logging event on firebase: {ex.Message}");
        }
    }
}