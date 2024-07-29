using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;
partial class CustomDatePickerHandler : DatePickerHandler
{
    public CustomDatePickerHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomDatePicker), MapBorder);
        Mapper.Add(nameof(CustomDatePicker.HorizontalTextAlignment), MapHorizontalTextAlignment);
        Mapper.Add(nameof(CustomDatePicker.Placeholder), MapPlaceholder);
        Mapper.Add(nameof(CustomDatePicker.PlaceholderColor), MapPlaceholder);
        Mapper.Add(nameof(CustomDatePicker.IsFocused), MapIsFocused);
    }
}
