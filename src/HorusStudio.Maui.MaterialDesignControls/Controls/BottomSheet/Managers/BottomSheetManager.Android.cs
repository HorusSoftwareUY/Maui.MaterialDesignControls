namespace HorusStudio.Maui.MaterialDesignControls;

internal partial class BottomSheetManager
{ 
    static partial void PlatformShow(Window window, MaterialBottomSheet sheet, bool animated)
    {
        if (window.Handler?.MauiContext is not { } mauiContext) return;
        
        var controller = new BottomSheetController(mauiContext, sheet);
        sheet.Controller = controller;
        controller.Show(animated);
    }
}
