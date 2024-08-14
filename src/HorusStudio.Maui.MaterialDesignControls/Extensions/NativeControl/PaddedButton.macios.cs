using System.Runtime.InteropServices;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Extensions.NativeControl;

public class PaddedButton : UIButton
{
    /// <summary>
    /// Initialize <see cref="PaddedButton"/>
    /// </summary>
    public PaddedButton(nfloat leftPadding, NFloat topPadding, NFloat rightPadding, NFloat bottomPadding, UIImage? imageView)
    {
        if (imageView != null)
            SetImage(imageView, UIControlState.Normal);
        LeftPadding = leftPadding;
        TopPadding = topPadding;
        RightPadding = rightPadding;
        BottomPadding = bottomPadding;
        SetPadding(leftPadding, topPadding, rightPadding, bottomPadding);
    }

    /// <summary>
    /// Left Padding
    /// </summary>
    public NFloat LeftPadding { get; }

    /// <summary>
    /// Top Padding
    /// </summary>
    public NFloat TopPadding { get; }

    /// <summary>
    /// Right Padding
    /// </summary>
    public NFloat RightPadding { get; }

    /// <summary>
    /// Bottom Padding
    /// </summary>
    public NFloat BottomPadding { get; }

    void SetPadding(NFloat leftPadding, NFloat topPadding, NFloat rightPadding, NFloat bottomPadding)
    {
        if (OperatingSystem.IsIOSVersionAtLeast(15) && Configuration is not null)
        {
            Configuration.ContentInsets = new NSDirectionalEdgeInsets(topPadding, leftPadding, bottomPadding, rightPadding);
        }
        else
        {
            ContentEdgeInsets = new UIEdgeInsets(topPadding, leftPadding, bottomPadding, rightPadding);
        }
    }
}