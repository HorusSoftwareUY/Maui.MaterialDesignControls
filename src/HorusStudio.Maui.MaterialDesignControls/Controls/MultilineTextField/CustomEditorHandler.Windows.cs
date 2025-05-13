using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class CustomEditorHandler
{
    public static void MapActiveIndicator(IEditorHandler handler, IEditor editor)
    {
        handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
    }

    public static void MapCursorColor(IEditorHandler handler, IEditor editor){ }
}
