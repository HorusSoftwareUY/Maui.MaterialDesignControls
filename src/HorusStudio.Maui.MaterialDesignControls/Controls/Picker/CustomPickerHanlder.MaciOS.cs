using HorusStudio.Maui.MaterialDesignControls.Utils;
using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomPickerHandler
{
    public static void MapBorder(IPickerHandler handler, IPicker picker)
    {
        handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
        handler.PlatformView.Layer.BorderWidth = 0;
        handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
    }

    public new static void MapHorizontalTextAlignment(IPickerHandler handler, IPicker picker)
    {
        if (picker is CustomPicker customPicker)
        {
            handler.PlatformView.TextAlignment = TextAlignmentHelper.Convert(customPicker.HorizontalTextAlignment);
        }
    }
}
