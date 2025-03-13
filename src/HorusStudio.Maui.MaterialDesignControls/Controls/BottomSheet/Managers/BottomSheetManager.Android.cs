namespace HorusStudio.Maui.MaterialDesignControls;

partial class BottomSheetManager
{ 
    static partial void PlatformShow(Window window, MaterialBottomSheet sheet, bool animated)
    {
        if (window.Handler?.MauiContext is not IMauiContext mauiContext) return;
        
        var controller = new BottomSheetController(mauiContext, sheet);
        sheet.Controller = controller;
        controller.Show(animated);
    }
}
