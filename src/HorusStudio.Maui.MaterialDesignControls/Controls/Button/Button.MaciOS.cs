using System;
using Foundation;
using NativeButton = UIKit.UIButton;

namespace HorusStudio.Maui.MaterialDesignControls;

static partial class ButtonExtensions
{
	public static void SetTextDecorations(this NativeButton button, TextDecorations textDecorations)
	{
        var text = button.Title(UIKit.UIControlState.Normal);
        var range = new NSRange(0, text.Length);

        var titleString = new NSMutableAttributedString(text);
        if (textDecorations == TextDecorations.Underline)
        {
            titleString.AddAttribute(UIKit.UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), range);
        }
        else if (textDecorations == TextDecorations.Strikethrough)
        {
            titleString.AddAttribute(UIKit.UIStringAttributeKey.StrikethroughStyle, NSNumber.FromInt32(2), range);
        }

        button.SetAttributedTitle(titleString, UIKit.UIControlState.Normal);
    }
}