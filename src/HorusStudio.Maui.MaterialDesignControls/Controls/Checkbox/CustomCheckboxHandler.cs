using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class CustomCheckboxHandler : CheckBoxHandler
{
    public CustomCheckboxHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CheckBox.Color), MapForeground);

#if IOS || MACCATALYST
        Mapper.Add(nameof(CustomCheckBox.TickColor), MapForeground);
#endif
    }
}
