using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
public partial class AnchorDetent
{
    partial void UpdateHeight(MaterialBottomSheet page, double maxSheetHeight)
    {
        if (page?.Handler?.PlatformView is not UIView pageView ||
            Anchor?.Handler?.PlatformView is not UIView targetView) return;
        
        var targetOrigin = targetView.Superview.ConvertPointToView(targetView.Frame.Location, pageView);
        _height = targetOrigin.Y;
    }
}