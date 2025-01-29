namespace UIKit;

public static class WindowExtensions
{
    public static UIWindow? GetDefaultWindow()
    {
        UIWindow? window = null;

        if (OperatingSystem.IsMacCatalystVersionAtLeast(15) || OperatingSystem.IsIOSVersionAtLeast(15))
        {
            foreach (var scene in UIApplication.SharedApplication.ConnectedScenes)
            {
                if (scene is not UIWindowScene windowScene) continue;
                window = windowScene.KeyWindow;
                window ??= windowScene?.Windows?.LastOrDefault();
            }
        }
        else
        {
            window = UIApplication.SharedApplication.Windows?.LastOrDefault();
        }

        return window;
    }
}