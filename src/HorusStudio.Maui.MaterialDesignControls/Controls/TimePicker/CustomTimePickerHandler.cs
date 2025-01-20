using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomTimePickerHandler : TimePickerHandler
{
    public CustomTimePickerHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomTimePicker), MapBorder);
        Mapper.Add(nameof(CustomTimePicker.HorizontalTextAlignment), MapHorizontalTextAlignment);
        Mapper.Add(nameof(CustomTimePicker.IsFocused), MapIsFocused);
    }
}
