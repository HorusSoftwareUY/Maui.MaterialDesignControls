
#if IOS || MACCATALYST
using CoreGraphics;
using UIKit;
#endif

#if ANDROID
using Activity = Android.App.Activity;
using Android.Graphics.Drawables;
#endif

namespace HorusStudio.Maui.MaterialDesignControls.Extensions;

static class ExtensionsConverters
{
    #if ANDROID
    
    public static void ScaleTo(this Drawable drawable, double newSize)
    {
        double width = drawable.IntrinsicWidth;
        double height = drawable.IntrinsicHeight;

        var ratio = width / height;
        if (width < height)
        {
            drawable.SetBounds(0, 0, DpToPixels(newSize * ratio), DpToPixels(newSize));
        }
        else drawable.SetBounds(0, 0, DpToPixels(newSize), DpToPixels(newSize / ratio));
    }

    public static int DpToPixels(double number)
    {
        var density = Platform.CurrentActivity.Resources.DisplayMetrics.Density;

        return (int)(density * number);
    }
    
    public static void SafeRunOnUi(this Activity activity, Action action) => activity.RunOnUiThread(() =>
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    });
    
    #endif
    
    #if IOS || MACCATALYST
    
    public static void SafeInvokeOnMainThread(this UIApplication app, Action action) => app.InvokeOnMainThread(() =>
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    });

    public static UIUserInterfaceStyle ToNative(this UserInterfaceStyle style)
    {
        return style switch
        {
            UserInterfaceStyle.Unspecified => UIUserInterfaceStyle.Unspecified,
            UserInterfaceStyle.Light => UIUserInterfaceStyle.Light,
            UserInterfaceStyle.Dark => UIUserInterfaceStyle.Dark,
        };
    }

    public static SnackbarPosition ToNative(this SnackbarPosition position)
    {
        return position switch
        {
            SnackbarPosition.Bottom => SnackbarPosition.Bottom,
            SnackbarPosition.Top => SnackbarPosition.Top,
        };
    }

    public static UIImage ScaleTo(this UIImage image, double newSize)
    {
        double width = image.Size.Width;
        double height = image.Size.Height;

        CGSize size;
        var ratio = width / height;
        if (width < height)
        {
            size = new CGSize(newSize * ratio, newSize);
        }
        else size = new CGSize(newSize, newSize / ratio);

        var renderer = new UIGraphicsImageRenderer(size);
        var resizedImage = renderer.CreateImage((UIGraphicsImageRendererContext context) =>
        {
            image.Draw(new CGRect(CGPoint.Empty, size));
        }).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

        return resizedImage;
    }

    public static UIWindow GetDefaultWindow()
    {
        UIWindow window = null;

        if (OperatingSystem.IsMacCatalystVersionAtLeast(15) || OperatingSystem.IsIOSVersionAtLeast(15))
        {
            foreach (var scene in UIApplication.SharedApplication.ConnectedScenes)
            {
                if (scene is UIWindowScene windowScene)
                {
                    window = windowScene.KeyWindow;

                    window ??= windowScene?.Windows?.LastOrDefault();
                }
            }
        }
        else
        {
            window = UIApplication.SharedApplication.Windows?.LastOrDefault();
        }

        return window;
    }
    
    #endif
    
    
}