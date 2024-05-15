using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;
partial class CustomRadioButtonHandler : RadioButtonHandler
{
    public CustomRadioButtonHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomRadioButton.StrokeColor), MapStrokeColor);
    }
}
