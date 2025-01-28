using Android.Views;

namespace Microsoft.Maui;

static class TextAlignmentExtensions
{
    public static GravityFlags ToGravityFlags(this TextAlignment textAlignment)
    {
        return textAlignment switch
        {
            TextAlignment.Start => GravityFlags.Left,
            TextAlignment.Center => GravityFlags.Center,
            _ => GravityFlags.Right
        };
    }

    public static Android.Views.TextAlignment ToAndroid(this TextAlignment textAlignment)
    {
        return textAlignment switch
        {
            TextAlignment.Start => Android.Views.TextAlignment.TextStart,
            TextAlignment.Center => Android.Views.TextAlignment.Center,
            _ => Android.Views.TextAlignment.TextEnd
        };
    }
}
