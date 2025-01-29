namespace Android.Graphics.Drawables;

static class ImageExtensions
{
    public static void ScaleTo(this Drawable drawable, float newSize) => ScaleTo(drawable, Convert.ToInt32(newSize));
    
    public static void ScaleTo(this Drawable drawable, int newSize)
    {
        var width = drawable.IntrinsicWidth;
        var height = drawable.IntrinsicHeight;

        var ratio = width / height;
        if (width < height)
        {
            drawable.SetBounds(0, 0, DimensionExtensions.DpToPixels(newSize * ratio), DimensionExtensions.DpToPixels(newSize));
        }
        else drawable.SetBounds(0, 0, DimensionExtensions.DpToPixels(newSize), DimensionExtensions.DpToPixels(newSize / ratio));
    }
}