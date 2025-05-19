using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialTextFieldHandler
{
    public static void MapBorder(IEntryHandler handler, IEntry entry)
    {
        handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
    }

    public static void MapCursorColor(IEntryHandler handler, IEntry entry) { }
}