using Android.Widget;
using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomDatePickerHandler
{
    public static void MapBorder(IDatePickerHandler handler, IDatePicker picker)
    {
        handler.PlatformView.Background = null;
        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
        handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

        handler.PlatformView.SetPadding(0, 0, 0, 0);
    }

    public static void MapHorizontalTextAlignment(IDatePickerHandler handler, IDatePicker picker)
    {
        if (picker is CustomDatePicker customPicker && handler.PlatformView is EditText)
        {
            handler.PlatformView.Gravity = TextAlignmentHelper.ConvertToGravityFlags(customPicker.HorizontalTextAlignment);
        }
    }

    public static void MapPlaceholder(IDatePickerHandler handler, IDatePicker picker)
    {
        if (picker is CustomDatePicker customDatePicker && handler.PlatformView is EditText control)
        {
            if (!customDatePicker.CustomDate.HasValue && !string.IsNullOrEmpty(customDatePicker.Placeholder))
            {
                control.Text = null;
                control.Hint = customDatePicker.Placeholder;
                control.SetHintTextColor(customDatePicker.PlaceholderColor.ToPlatform());
            }
        }
    }
}

