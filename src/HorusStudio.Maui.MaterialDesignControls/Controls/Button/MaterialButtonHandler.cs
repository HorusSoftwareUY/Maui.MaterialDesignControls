using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialButtonHandler : ButtonHandler
{
	public MaterialButtonHandler (): base(Mapper, CommandMapper)
	{
		Mapper.Add(nameof(CustomButton.TextDecorations), MapTextDecorations);
	}
}