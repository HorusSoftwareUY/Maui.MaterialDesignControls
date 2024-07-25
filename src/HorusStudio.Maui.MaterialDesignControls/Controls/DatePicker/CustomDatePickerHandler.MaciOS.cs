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
        if (picker is CustomDatePicker customPicker /*&& handler.PlatformView is UITextField control*/)
        {
            //control.TextAlignment = TextAlignmentHelper.Convert(customPicker.HorizontalTextAlignment);
        }
    }

    public static void MapPlaceholder(IDatePickerHandler handler, IDatePicker picker)
    {
        //if (picker is CustomDatePicker customDatePicker && handler.PlatformView is EditText control)
        //{
        //    if (!customDatePicker.CustomDate.HasValue && !string.IsNullOrEmpty(customDatePicker.Placeholder))
        //    {
        //        control.Text = null;
        //        control.Hint = customDatePicker.Placeholder;
        //        control.SetHintTextColor(customDatePicker.PlaceholderColor.ToPlatform());
        //    }
        //}
    }
}
