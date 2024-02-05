using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class BorderlessEntryHandler : EntryHandler
{
	public BorderlessEntryHandler(): base(Mapper, CommandMapper)
	{
		Mapper.Add(nameof(BorderlessEntry), MapBorder);
	}
}