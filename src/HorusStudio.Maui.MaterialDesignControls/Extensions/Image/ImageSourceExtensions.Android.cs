using Android.Graphics.Drawables;
using Microsoft.Maui.Platform;

namespace Microsoft.Maui.Controls;

static partial class ImageSourceExtensions
{
    public static Drawable? ToDrawable(this ImageSource source, double? size = null, Color? tintColor = null)
    {
        if (source.Source() is not { } iconSource) return null;
        
        var currentApp = Microsoft.Maui.ApplicationModel.Platform.AppContext;
        var imgId = currentApp.GetDrawableId(iconSource);
        var bmp = Android.Graphics.BitmapFactory.DecodeResource(currentApp.Resources, imgId);
        if (bmp == null) return null;

        if (size != null)
        {
            var iconSizeInDp = Convert.ToInt32(size).DpToPixels();
            bmp = Android.Graphics.Bitmap.CreateScaledBitmap(bmp, iconSizeInDp, iconSizeInDp, true);    
        }
        
        var icon = new BitmapDrawable(currentApp.Resources, bmp);
        if (tintColor != null)
        {
            icon.SetColorFilter(tintColor.ToPlatform(), FilterMode.SrcIn);    
        }
        return icon;
    }
}
