using System;
using Foundation;
using UIKit;
using NativeButton = UIKit.UIButton;

namespace HorusStudio.Maui.MaterialDesignControls;

static partial class ButtonExtensions
{
	public static void SetTextDecorations(this NativeButton button, TextDecorations textDecorations)
	{
        if (button.TitleLabel != null && button.CurrentTitle != null)
        {
            var text = button.CurrentTitle;
            var attributes = new UIStringAttributes
            {
                UnderlineStyle = textDecorations == TextDecorations.Underline ? NSUnderlineStyle.Single : NSUnderlineStyle.None,
                StrikethroughStyle = textDecorations == TextDecorations.Strikethrough ? NSUnderlineStyle.Single : NSUnderlineStyle.None
            };
            button.TitleLabel.AttributedText = new Foundation.NSAttributedString(text, attributes);
        }
    }
}