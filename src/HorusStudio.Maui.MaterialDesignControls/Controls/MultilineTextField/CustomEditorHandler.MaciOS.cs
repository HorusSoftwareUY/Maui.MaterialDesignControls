using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;
partial class CustomEditorHandler
{
    public static void MapActiveIndicator(IEditorHandler handler, IEditor editor)
    {
        handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
        handler.PlatformView.Layer.BorderWidth = 0;
    }

    public static void MapCursorColor(IEditorHandler handler, IEditor editor)
    {
        if (editor is CustomEditor customEditor && customEditor.CursorColor != null)
        {
            handler.PlatformView.TintColor = customEditor.CursorColor.ToPlatform();
        }
    }
}
