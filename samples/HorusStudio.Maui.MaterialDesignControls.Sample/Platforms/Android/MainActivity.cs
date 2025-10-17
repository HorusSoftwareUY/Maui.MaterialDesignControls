using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls.Sample
{
    [Activity(Theme = "@style/MyAppTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var uiModeManager = (UiModeManager)GetSystemService(UiModeService);
            var isDarkMode = uiModeManager.NightMode == UiNightMode.Yes;
            if (isDarkMode)
            {
                Window.SetStatusBarColor(MaterialDarkTheme.Surface.ToPlatform());
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)0;
            }
            else
            {
                Window.SetStatusBarColor(MaterialLightTheme.Surface.ToPlatform());
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            }
        }
    }
}