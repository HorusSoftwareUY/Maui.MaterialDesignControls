using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using UIKit;

namespace Microsoft.Maui.Controls;
static partial class ImageSourceExtensions
{
    public static async Task<UIImage> ToUIImageAsync(this ImageSource source)
    {
        var handler = source?.GetHandler();
        if (handler is null) return null;

        return await handler.LoadImageAsync(source);
    }

    public static IImageSourceHandler GetHandler(this ImageSource source)
    {
        IImageSourceHandler returnValue = null;

        if (source is UriImageSource)
        {
            returnValue = new ImageLoaderSourceHandler();
        }
        else if (source is FileImageSource)
        {
            returnValue = new FileImageSourceHandler();
        }
        else if (source is StreamImageSource)
        {
            returnValue = new StreamImagesourceHandler();
        }

        return returnValue;
    }
}
