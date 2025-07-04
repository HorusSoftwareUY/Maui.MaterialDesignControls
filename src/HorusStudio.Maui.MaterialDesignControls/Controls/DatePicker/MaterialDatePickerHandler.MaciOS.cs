using Microsoft.Maui.Handlers;
#if IOS
using Microsoft.Maui.Platform;
#endif
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialDatePickerHandler
{
#if IOS
    protected override void ConnectHandler(MauiDatePicker platformView)
    {
        base.ConnectHandler(platformView);
        platformView.EditingDidBegin += OnEditingDidBegin;
        platformView.EditingDidEnd += OnEditingDidEnd;
    }

    protected override void DisconnectHandler(MauiDatePicker platformView)
    {
        base.DisconnectHandler(platformView);
        platformView.EditingDidBegin -= OnEditingDidBegin;
        platformView.EditingDidEnd -= OnEditingDidEnd;
    }
#endif

    public static void MapBorder(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView.BackgroundColor = UIColor.Clear;
        handler.PlatformView.Layer.BorderWidth = 0;
#if IOS
        handler.PlatformView.BorderStyle = UITextBorderStyle.None;
#endif

        var checkUseWheelsPickerStyle = CheckUseWheelsPickerStyle(datePicker, handler);
        if (checkUseWheelsPickerStyle && handler is UITextField control)
        {
            try
            {
                UIDatePicker pickers = (UIDatePicker)control.InputView;
                pickers.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
            }
            catch (Exception)
            { }
        }
    }

    private static bool CheckUseWheelsPickerStyle(IDatePicker datePicker, IDatePickerHandler handler)
    {
        return datePicker is CustomDatePicker && handler is UITextField && UIDevice.CurrentDevice.CheckSystemVersion(13, 2);
    }

    public static void MapHorizontalTextAlignment(IDatePickerHandler handler, IDatePicker datePicker)
    {
#if IOS
        if (datePicker is CustomDatePicker customPicker
            && handler != null
            && handler.PlatformView is UITextField control)
        {
            control.TextAlignment = customPicker.HorizontalTextAlignment.ToUIKit();
        }
#endif
    }

    public static void MapIsFocused(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (handler.PlatformView.Focused == datePicker.IsFocused) return;

        if (datePicker.IsFocused)
        {
            handler.PlatformView.BecomeFirstResponder();
        }
        else
        {
            handler.PlatformView.ResignFirstResponder();
        }
    }

    private void OnEditingDidBegin(object sender, EventArgs e)
    {
        this.VirtualView.IsFocused = true;
    }

    public void OnEditingDidEnd(object sender, EventArgs e)
    {
        this.VirtualView.IsFocused = false;
    }
}
