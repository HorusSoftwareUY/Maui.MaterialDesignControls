using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class CustomButtonHandler: ButtonHandler
{
	public CustomButtonHandler(): base(Mapper, CommandMapper)
	{
		Mapper.Add(nameof(CustomButton.TextDecorations), MapTextDecorations);
	}
}