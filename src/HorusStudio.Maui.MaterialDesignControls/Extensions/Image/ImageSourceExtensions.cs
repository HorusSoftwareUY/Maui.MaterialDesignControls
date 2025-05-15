using System;
namespace Microsoft.Maui.Controls;

static partial class ImageSourceExtensions
{
    public static string? Source(this ImageSource imageSource)
    {
        if (imageSource is FileImageSource fileImageSource)
        {
            // For file-based images
            return fileImageSource.File;
        }
        else if (imageSource is UriImageSource uriImageSource)
        {
            // For URI-based images
            return uriImageSource.Uri.ToString();
        }
        else if (imageSource is StreamImageSource)
        {
            // StreamImageSource does not have a string representation
            return "Stream-based image source (no name)";
        }
        else
        {
            return null; // Unknown ImageSource type
        }
    }
}

