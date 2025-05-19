using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialPickerHandler : PickerHandler
{
    public MaterialPickerHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomPicker), MapBorder);
        Mapper.Add(nameof(CustomPicker.HorizontalTextAlignment), MapHorizontalTextAlignment);
    }
}
