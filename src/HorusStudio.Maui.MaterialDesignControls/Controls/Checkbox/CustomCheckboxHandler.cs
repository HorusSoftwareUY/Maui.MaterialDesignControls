using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;
partial class CustomCheckboxHandler : CheckBoxHandler
{
    public CustomCheckboxHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CheckBox.Color), MapForeground);
        Mapper.Add(nameof(CustomCheckBox.CheckColor), MapForeground);
    }
}
