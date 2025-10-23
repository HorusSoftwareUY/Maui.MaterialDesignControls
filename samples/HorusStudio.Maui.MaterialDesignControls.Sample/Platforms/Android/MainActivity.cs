using Android.App;
using Android.Content.PM;
using Android.Runtime;

namespace HorusStudio.Maui.MaterialDesignControls.Sample
{
    [Activity(Theme = "@style/MyAppTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [Register("com.horusstudio.maui.materialdesigncontrols.sample.MainActivity")]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}