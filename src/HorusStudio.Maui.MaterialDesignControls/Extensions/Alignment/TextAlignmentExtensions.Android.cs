using AndroidViews = Android.Views;

namespace Microsoft.Maui;

static class TextAlignmentExtensions
{
    public static AndroidViews.GravityFlags ToGravityFlags(this TextAlignment textAlignment)
    {
        return textAlignment switch
        {
            TextAlignment.Start =>AndroidViews.GravityFlags.Left,
            TextAlignment.Center => AndroidViews.GravityFlags.Center,
            _ => AndroidViews.GravityFlags.Right
        };
    }

    public static AndroidViews.TextAlignment ToAndroid(this TextAlignment textAlignment)
    {
        return textAlignment switch
        {
            TextAlignment.Start => AndroidViews.TextAlignment.TextStart,
            TextAlignment.Center => AndroidViews.TextAlignment.Center,
            _ => AndroidViews.TextAlignment.TextEnd
        };
    }
}
