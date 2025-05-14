using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialDatePickerHandler : DatePickerHandler
{
    public MaterialDatePickerHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomDatePicker), MapBorder);
        Mapper.Add(nameof(CustomDatePicker.HorizontalTextAlignment), MapHorizontalTextAlignment);
        Mapper.Add(nameof(CustomDatePicker.IsFocused), MapIsFocused);
    }
}
