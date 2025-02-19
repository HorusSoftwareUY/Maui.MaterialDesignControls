namespace UIKit;

using CoreGraphics;

static class ImageExtensions
{
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
}