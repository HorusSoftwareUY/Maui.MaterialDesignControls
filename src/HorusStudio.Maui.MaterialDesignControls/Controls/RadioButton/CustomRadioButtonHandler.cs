using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class CustomRadioButtonHandler : RadioButtonHandler
{
    public CustomRadioButtonHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomRadioButton.StrokeColor), MapStrokeColor);

#if IOS || MACCATALYST
        Mapper.Add(nameof(CustomRadioButton.IsChecked), MapStrokeColor);
#endif
    }
}
