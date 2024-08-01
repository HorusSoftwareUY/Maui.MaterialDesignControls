using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomTimePickerHandler
{
    public static void MapBorder(ITimePickerHandler handler, ITimePicker timePicker)
    {
        handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
    }

    public static void MapHorizontalTextAlignment(ITimePickerHandler handler, ITimePicker timePicker) { }

    public static void MapPlaceholder(ITimePickerHandler handler, ITimePicker timePicker) { }

    public static void MapIsFocused(ITimePickerHandler handler, ITimePicker timePicker) { }
}
