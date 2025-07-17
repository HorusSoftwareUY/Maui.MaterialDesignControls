namespace HorusStudio.Maui.MaterialDesignControls;

internal partial class BottomSheetManager
{
    internal static void Show(MaterialBottomSheet sheet, bool animated)
    {
        PlatformShow(sheet, animated);
    }
}
