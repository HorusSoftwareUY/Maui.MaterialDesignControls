namespace System;

using Android.Content;
using Android.Util;

static class DimensionExtensions
{
    public static float DpToPixels(this float valueInDp) => DpToPixels(valueInDp, Platform.CurrentActivity?.ApplicationContext);
    
    public static float DpToPixels(this float valueInDp, Context? context)
    {
        var metrics = context?.Resources?.DisplayMetrics;
        return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
    }
    
    public static int DpToPixels(this int valueInDp) => DpToPixels(valueInDp, Platform.CurrentActivity?.ApplicationContext);

    public static int DpToPixels(this int valueInDp, Context? context) => Convert.ToInt32(((float)valueInDp).DpToPixels(context));
}