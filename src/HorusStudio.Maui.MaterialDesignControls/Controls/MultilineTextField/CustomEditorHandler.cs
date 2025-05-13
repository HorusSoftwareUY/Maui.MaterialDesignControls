using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class CustomEditorHandler : EditorHandler
{
    public CustomEditorHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomEditor), MapActiveIndicator);
        Mapper.Add(nameof(CustomEditor.CursorColor), MapCursorColor);
    }
}
