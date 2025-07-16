using Foundation;
using System.Runtime.InteropServices;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

internal partial class BottomSheetManager
{
    #region Fields & Properties
    
    //TODO: remove static for those observers !
    private static NSObject? _keyboardWillShowObserver;
    private static NSObject? _keyboardDidHideObserver;
    internal static NFloat KeyboardHeight { get; private set; }

    #endregion Fields & Properties

    private static void PlatformShow(MaterialBottomSheet sheet, bool animated)
    {
        var mauiContext = sheet.Handler!.MauiContext!;
        var window = (UIWindow)sheet.GetParentWindow()!.Handler!.PlatformView!;

        var controller = new BottomSheetController(mauiContext, sheet, window);
        sheet.Controller = controller; 
            
        _keyboardWillShowObserver ??= UIKeyboard.Notifications.ObserveWillShow(KeyboardWillShow);
        _keyboardDidHideObserver ??= UIKeyboard.Notifications.ObserveDidHide(KeyboardDidHide);
        
        if (OperatingSystem.IsIOSVersionAtLeast(15))
        {
            controller.UpdateDetents();
            controller.UpdateHasHandle(sheet.HasHandle);
            controller.UpdateHasBackdrop(sheet.HasScrim);
        }

        if (OperatingSystem.IsIOSVersionAtLeast(13))
        {
            controller.ModalInPresentation = !sheet.IsCancelable;
            //controller.PresentedViewController.View.BackgroundColor = UIColor.Green;
        }

        var parent = Platform.GetCurrentUIViewController();
        parent!.PresentViewController(controller, animated, sheet.NotifyShown);
    }

    private static void KeyboardDidHide(object? sender, UIKeyboardEventArgs e) 
        => KeyboardHeight = 0;

    private static void KeyboardWillShow(object? sender, UIKeyboardEventArgs e) 
        => KeyboardHeight = e.FrameEnd.Height;
}
