using HorusStudio.Maui.MaterialDesignControls;

namespace Microsoft.Maui.Controls.Xaml;

static class AppThemeBindingExtensions
{
    public static object GetValueForCurrentTheme(this AppThemeBindingExtension instance)
    {   
        AppTheme currentTheme = AppTheme.Unspecified;
        
        if (Application.Current is { } app)
        {
            if (MainThreadExtensions.IsMainThread)
            {
                currentTheme = app.RequestedTheme;
            }
            else
            {
                MainThreadExtensions.SafeRunOnUiThread(() => currentTheme = app.RequestedTheme);
            }    
        }

        return currentTheme switch
        {
            AppTheme.Light => instance.Light,
            AppTheme.Dark => instance.Dark,
            _ => instance.Default
        };
    }

    public static T GetValueForCurrentTheme<T>(this AppThemeBindingExtension instance) => (T)GetValueForCurrentTheme(instance);
}