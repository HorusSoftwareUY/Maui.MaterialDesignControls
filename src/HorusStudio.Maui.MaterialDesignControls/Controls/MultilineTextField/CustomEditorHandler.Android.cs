using Android.Graphics.Drawables;
using Android.OS;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class CustomEditorHandler
{
    public static void MapActiveIndicator(IEditorHandler handler, IEditor editor)
    {
        handler.PlatformView.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
    }

    public static void MapCursorColor(IEditorHandler handler, IEditor editor)
    {
        if (editor is CustomEditor customEditor && customEditor.CursorColor != null && handler.PlatformView is Android.Widget.EditText editText)
        {
            BuildVersionCodes androidVersion = Build.VERSION.SdkInt;
            if (androidVersion >= BuildVersionCodes.Q)
            {
                editText.TextCursorDrawable.SetTint(customEditor.CursorColor.ToPlatform());
            }
        }
    }
}
