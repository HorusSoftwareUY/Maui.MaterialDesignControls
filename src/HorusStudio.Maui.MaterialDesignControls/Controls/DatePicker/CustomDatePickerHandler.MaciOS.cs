using Foundation;
using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomDatePickerHandler
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
        handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
        handler.PlatformView.Layer.BorderWidth = 0;
#if IOS
        handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
        if (datePicker is CustomDatePicker customDatePicker && handler is UITextField control)
        {
            control.TextAlignment = TextAlignmentHelper.Convert(customDatePicker.HorizontalTextAlignment);
        }
#endif
    }

    public static void MapHorizontalTextAlignment(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (datePicker is CustomDatePicker customPicker && handler is UITextField control)
        {
            control.TextAlignment = TextAlignmentHelper.Convert(customPicker.HorizontalTextAlignment);
        }
    }

    public static void MapPlaceholder(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (datePicker is CustomDatePicker customDatePicker && handler is UITextField control)
        {
            control.TextAlignment = TextAlignmentHelper.Convert(customDatePicker.HorizontalTextAlignment);

            if (!customDatePicker.CustomDate.HasValue && !string.IsNullOrEmpty(customDatePicker.Placeholder))
            {
                control.Text = null;
                control.AttributedPlaceholder = new NSAttributedString(customDatePicker.Placeholder, foregroundColor: customDatePicker.PlaceholderColor.ToPlatform());
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 2))
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

        if (datePicker is CustomDatePicker customDatePicker && handler is UITextField control)
        {
            control.TextAlignment = TextAlignmentHelper.Convert(customDatePicker.HorizontalTextAlignment);
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
