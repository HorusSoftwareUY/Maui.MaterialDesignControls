using Android.Graphics;
using NativeButton = Google.Android.Material.Button.MaterialButton;

namespace HorusStudio.Maui.MaterialDesignControls;

static partial class ButtonExtensions
{
	public static void SetTextDecorations(this NativeButton button, TextDecorations textDecorations)
	{
        button.PaintFlags &= ~PaintFlags.UnderlineText & ~PaintFlags.StrikeThruText;

        if (textDecorations == TextDecorations.Underline)
        {
            button.PaintFlags |= PaintFlags.UnderlineText;
        }
        else if (textDecorations == TextDecorations.Strikethrough)
        {
            button.PaintFlags |= PaintFlags.StrikeThruText;
        }
    }
}