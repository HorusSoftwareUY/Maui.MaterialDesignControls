using Foundation;
using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomDatePickerHandler
{
    public static void MapBorder(IDatePickerHandler handler, IDatePicker picker)
    {
        handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
        handler.PlatformView.Layer.BorderWidth = 0;
    }

    public static void MapHorizontalTextAlignment(IDatePickerHandler handler, IDatePicker picker)
    {
        if (picker is CustomDatePicker customPicker && handler is UITextField control)
        {
            control.TextAlignment = TextAlignmentHelper.Convert(customPicker.HorizontalTextAlignment);
        }
    }

    public static void MapPlaceholder(IDatePickerHandler handler, IDatePicker picker)
    {
        if (picker is CustomDatePicker customDatePicker && handler is UITextField control)
        {
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
    }

    //protected override void ConnectHandler(UIDatePicker platformView)
    //{
    //    base.DisconnectHandler(platformView);
    //}

    //protected override void ConnectHandler(UIDatePicker platformView)
    //{
    //    base.ConnectHandler(platformView);
    //    platformView.EditingDidBegin += OnEditingDidBegin;
    //    platformView.EditingDidEnd += OnEditingDidEnd;
    //}

    protected override void DisconnectHandler(MauiDatePicker platformView)
    {
        base.DisconnectHandler(platformView);
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
