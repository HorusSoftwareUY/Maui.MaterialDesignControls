using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialTextFieldHandler
{
    public static void MapBorder(IEntryHandler handler, IEntry entry)
    {
        handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
        handler.PlatformView.Layer.BorderWidth = 0;
        handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
    }

    public static void MapCursorColor(IEntryHandler handler, IEntry entry)
    {
        if (entry is CustomEntry customEntry && customEntry.CursorColor != null)
        {
            handler.PlatformView.TintColor = customEntry.CursorColor.ToPlatform();
        }
    }
}