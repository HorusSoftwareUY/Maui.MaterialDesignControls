namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
internal partial class BottomSheetManager
{
    internal static void Show(Window window, MaterialBottomSheet sheet, bool animated)
    {
        PlatformShow(window, sheet, animated);
        sheet.SizeChanged += OnSizeChanged;
    }

    private static void OnSizeChanged(object? sender, EventArgs e)
    {
        PlatformLayout(sender as MaterialBottomSheet);
    }

    static partial void PlatformShow(Window window, MaterialBottomSheet sheet, bool animated);
    static partial void PlatformLayout(MaterialBottomSheet? sheet);
}
