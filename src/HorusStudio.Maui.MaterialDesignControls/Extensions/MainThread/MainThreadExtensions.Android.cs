using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace Android.App;

static class MainThreadExtensions
{
    public static void SafeRunOnUi(this Activity activity, Action action) => activity.RunOnUiThread(() =>
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Logger.Debug(ex.ToString());
        }
    });    
}