using Microsoft.Maui.Handlers;
#if IOS
using Microsoft.Maui.Platform;
#endif
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class CustomTimePickerHandler
{
#if IOS
    protected override void ConnectHandler(MauiTimePicker platformView)
    {
        base.ConnectHandler(platformView);
        platformView.EditingDidBegin += OnEditingDidBegin;
        platformView.EditingDidEnd += OnEditingDidEnd;
    }

    protected override void DisconnectHandler(MauiTimePicker platformView)
    {
        base.DisconnectHandler(platformView);
        platformView.EditingDidBegin -= OnEditingDidBegin;
        platformView.EditingDidEnd -= OnEditingDidEnd;
    }
#endif

    public static void MapBorder(ITimePickerHandler handler, ITimePicker timePicker)
    {
        handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
        handler.PlatformView.Layer.BorderWidth = 0;
#if IOS
        handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif

        var checkUseWheelsPickerStyle = CheckUseWheelsPickerStyle(timePicker, handler);
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

    private static bool CheckUseWheelsPickerStyle(ITimePicker timePicker, ITimePickerHandler handler)
    {
        return timePicker is CustomTimePicker && handler is UITextField && UIDevice.CurrentDevice.CheckSystemVersion(13, 2);
    }

    public static void MapHorizontalTextAlignment(ITimePickerHandler handler, ITimePicker timePicker)
    {
        if (timePicker is CustomTimePicker customPicker && handler is UITextField control)
        {
            control.TextAlignment = customPicker.HorizontalTextAlignment.ToUIKit();
        }
    }

    public static void MapIsFocused(ITimePickerHandler handler, ITimePicker timePicker)
    {
        if (handler.PlatformView.Focused == timePicker.IsFocused) return;

        if (timePicker.IsFocused)
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
