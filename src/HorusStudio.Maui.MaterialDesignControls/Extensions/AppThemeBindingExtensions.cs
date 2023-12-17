using System;

namespace Microsoft.Maui.Controls.Xaml;

internal static class AppThemeBindingExtensions
{
    public static object GetValueForCurrentTheme(this AppThemeBindingExtension instance)
    {
        AppTheme currentTheme = AppTheme.Unspecified;
        if (MainThread.IsMainThread)
        {
            currentTheme = Application.Current.RequestedTheme;
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(() => currentTheme = Application.Current.RequestedTheme);
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

