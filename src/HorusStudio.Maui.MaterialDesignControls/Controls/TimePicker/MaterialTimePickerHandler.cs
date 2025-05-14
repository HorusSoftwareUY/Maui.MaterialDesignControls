using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialTimePickerHandler : TimePickerHandler
{
    public MaterialTimePickerHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomTimePicker), MapBorder);
        Mapper.Add(nameof(CustomTimePicker.HorizontalTextAlignment), MapHorizontalTextAlignment);
        Mapper.Add(nameof(CustomTimePicker.IsFocused), MapIsFocused);
    }
}
