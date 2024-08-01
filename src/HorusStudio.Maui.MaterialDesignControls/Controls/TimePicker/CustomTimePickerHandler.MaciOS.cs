using HorusStudio.Maui.MaterialDesignControls.Helpers;
using Microsoft.Maui.Handlers;
#if IOS
using Microsoft.Maui.Platform;
#endif
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomTimePickerHandler
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
        if (timePicker is CustomTimePicker && handler is UITextField control && UIDevice.CurrentDevice.CheckSystemVersion(13, 2))
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

    public static void MapHorizontalTextAlignment(ITimePickerHandler handler, ITimePicker timePicker)
    {
        if (timePicker is CustomTimePicker customPicker && handler is UITextField control)
        {
            control.TextAlignment = TextAlignmentHelper.Convert(customPicker.HorizontalTextAlignment);
        }
    }

    //public static void MapPlaceholder(ITimePickerHandler handler, ITimePicker picker)
    //{
    //    if (datePicker is CustomDatePicker customDatePicker && handler is UITextField control)
    //    {
    //        if (!customDatePicker.CustomDate.HasValue && !string.IsNullOrEmpty(customDatePicker.Placeholder))
    //        {
    //            control.Text = null;
    //            control.AttributedPlaceholder = new NSAttributedString(customDatePicker.Placeholder, foregroundColor: customDatePicker.PlaceholderColor.ToPlatform());
    //        }

    //        if (UIDevice.CurrentDevice.CheckSystemVersion(13, 2))
    //        {
    //            try
    //            {
    //                UIDatePicker pickers = (UIDatePicker)control.InputView;
    //                pickers.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
    //            }
    //            catch (Exception)
    //            { }
    //        }
    //    }
    //}

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
