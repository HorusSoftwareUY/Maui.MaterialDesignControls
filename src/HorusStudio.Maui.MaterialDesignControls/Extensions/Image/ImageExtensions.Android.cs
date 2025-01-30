using Android.Widget;

namespace Android.Graphics.Drawables;

static class ImageExtensions
{
    public static void ScaleTo(this Drawable drawable, float newSize) => ScaleTo(drawable, Convert.ToInt32(newSize));
    
    public static void ScaleTo(this Drawable drawable, int newSize)
    {
        var newSizeInDp = newSize.DpToPixels();
        var currentWidth = drawable.IntrinsicWidth;
        //var scale = (float)newSize/width;
        var margin = Math.Abs(newSizeInDp - currentWidth) / 2;

        drawable.SetBounds(margin, margin, margin, margin);
    }
}