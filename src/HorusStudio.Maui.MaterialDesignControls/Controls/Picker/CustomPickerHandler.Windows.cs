using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class CustomPickerHandler
{
    public static void MapBorder(IPickerHandler handler, IPicker picker)
    {
        handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
    }

    public new static void MapHorizontalTextAlignment(IPickerHandler handler, IPicker picker)
    {
    }
}
