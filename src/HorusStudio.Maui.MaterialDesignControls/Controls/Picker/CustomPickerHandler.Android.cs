using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomPickerHandler
{
    public static void MapBorder(IPickerHandler handler, IPicker picker)
    {
        handler.PlatformView.Background = null;
        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
        handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

        handler.PlatformView.SetPadding(0, 0, 0, 0);
    }

    public new static void MapHorizontalTextAlignment(IPickerHandler handler, IPicker picker)
    {
        if (picker is CustomPicker customPicker)
        {
            handler.PlatformView.Gravity = TextAlignmentHelper.ConvertToGravityFlags(customPicker.HorizontalTextAlignment);
        }
    }
}
