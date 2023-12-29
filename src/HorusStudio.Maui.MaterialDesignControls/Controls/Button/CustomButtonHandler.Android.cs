using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomButtonHandler
{
    public static void MapTextDecorations(IButtonHandler handler, IButton button)
    {
        if (button is CustomButton customButton)
        {
            handler.PlatformView?.SetTextDecorations(customButton.TextDecorations);
        }
    }
}