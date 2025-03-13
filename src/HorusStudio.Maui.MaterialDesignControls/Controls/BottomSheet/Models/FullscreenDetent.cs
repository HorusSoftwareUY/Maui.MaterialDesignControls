namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
public partial class FullscreenDetent : Detent
{
    public override double GetHeight(MaterialBottomSheet page, double maxSheetHeight)
    {
        return maxSheetHeight;
    }
}
