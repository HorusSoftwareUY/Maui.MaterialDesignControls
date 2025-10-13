using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Helpers
{
    internal class StatusBarHelper
    {
        internal static void SetStatusBarColor(Color color, bool darkStatusBarTint)
        {
#if ANDROID
            var window = Platform.CurrentActivity?.Window;

            if (window == null) return;

            window.SetStatusBarColor(color.ToPlatform());

            var flag = Android.Views.SystemUiFlags.LightStatusBar;

            if (darkStatusBarTint)
                window.DecorView.SystemUiVisibility |= (Android.Views.StatusBarVisibility)flag;
            else
                window.DecorView.SystemUiVisibility &= ~(Android.Views.StatusBarVisibility)flag;
#endif

#if IOS
            var uiColor = color.ToPlatform();

            var currentView = UIKit.UIApplication.SharedApplication.KeyWindow;
            if (currentView == null) return;

            currentView.BackgroundColor = uiColor;

            UIKit.UIApplication.SharedApplication.StatusBarStyle =
                darkStatusBarTint ? UIKit.UIStatusBarStyle.DarkContent : UIKit.UIStatusBarStyle.LightContent;
#endif
        }
    }
}
