namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Init in 2 times:
/// 1) Create(): platform views are created
/// 2) the handler is initialized by BottomSheet, which automatically calls all mappers methods.
/// 3) PlatformShow(): the platform views are added to the visual tree
/// </summary>
internal partial class BottomSheetManager
{
    public static void Create(IMauiContext context, MaterialBottomSheet sheet, bool aboveEverything)
    {
        var controller = new BottomSheetController(context, sheet, !aboveEverything);
        sheet.Controller = controller;
        controller.CreateViews();
    }

    static void PlatformShow(MaterialBottomSheet sheet, bool animated)
    {
        var controller = sheet.Controller;
        var platformView = (Android.Views.View)sheet.Handler!.PlatformView;

        controller.Show(Platform.CurrentActivity?.Window, platformView, animated);
    }
}
