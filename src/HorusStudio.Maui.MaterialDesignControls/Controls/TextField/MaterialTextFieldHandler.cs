using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialTextFieldHandler : EntryHandler
{
    public MaterialTextFieldHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomEntry), MapBorder);
        Mapper.Add(nameof(CustomEntry.CursorColor), MapCursorColor);
    }
}