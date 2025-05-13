using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialCheckBoxHandler : CheckBoxHandler
{
    public MaterialCheckBoxHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CheckBox.Color), MapForeground);

#if IOS || MACCATALYST
        Mapper.Add(nameof(CustomCheckBox.TickColor), MapForeground);
#endif
    }
}
