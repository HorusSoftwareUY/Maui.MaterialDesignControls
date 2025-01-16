using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;
partial class CustomPickerHandler : PickerHandler
{
    public CustomPickerHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomPicker), MapBorder);
        Mapper.Add(nameof(CustomPicker.HorizontalTextAlignment), MapHorizontalTextAlignment);
    }
}
