using Foundation;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Extensions.Label;

public static partial class LabelExtensions
{
    public static void SetTextCharacterSpacing(this UILabel uiLabel, string text, double characterSpacing)
    {
        var attributedString = new NSMutableAttributedString(text);
        attributedString.AddAttribute(
            UIStringAttributeKey.KerningAdjustment,
            NSNumber.FromDouble(characterSpacing),
            new NSRange(0, text.Length)
        );
        
        uiLabel.AttributedText = attributedString;
    }
    
    public static void SetLineBreakMode(this UILabel uiLabel, LineBreakMode lineBreakMode, int textMaxLines)
    {
        switch (lineBreakMode)
        {
            case LineBreakMode.NoWrap:
            case LineBreakMode.TailTruncation:
                uiLabel.LineBreakMode = UILineBreakMode.TailTruncation;
                uiLabel.Lines = 1;
                break;
            case LineBreakMode.HeadTruncation:
                uiLabel.LineBreakMode = UILineBreakMode.HeadTruncation;
                uiLabel.Lines = 1;
                break;
            case LineBreakMode.MiddleTruncation:
                uiLabel.LineBreakMode = UILineBreakMode.MiddleTruncation;
                uiLabel.Lines = 1;
                break;
            case LineBreakMode.WordWrap:
                uiLabel.LineBreakMode = UILineBreakMode.WordWrap;
                uiLabel.Lines = textMaxLines;
                break;
            case LineBreakMode.CharacterWrap:
                uiLabel.LineBreakMode = UILineBreakMode.CharacterWrap;
                uiLabel.Lines = textMaxLines;
                break;
        }
    }
}
