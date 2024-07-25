using Android.Views;

namespace HorusStudio.Maui.MaterialDesignControls.Utils;

public static class TextAlignmentHelper
{
    public static GravityFlags ConvertToGravityFlags(Microsoft.Maui.TextAlignment textAlignment)
    {
        switch (textAlignment)
        {
            case Microsoft.Maui.TextAlignment.Start:
                return GravityFlags.Left;
            case Microsoft.Maui.TextAlignment.Center:
                return GravityFlags.Center;
            default:
                return GravityFlags.Right;
        }
    }

    public static global::Android.Views.TextAlignment ConvertToAndroid(Microsoft.Maui.TextAlignment textAlignment)
    {
        switch (textAlignment)
        {
            case Microsoft.Maui.TextAlignment.Start:
                return global::Android.Views.TextAlignment.TextStart;
            case Microsoft.Maui.TextAlignment.Center:
                return global::Android.Views.TextAlignment.Center;
            default:
                return global::Android.Views.TextAlignment.TextEnd;
        }
    }
}
