using Android.Text;
using Android.Text.Style;
using Android.Widget;
using Button = Android.Widget.Button;

namespace HorusStudio.Maui.MaterialDesignControls.Extensions.Label;

public static partial class LabelExtensions
{
    public static void SetText(this TextView textView, string text, TextDecorations textDecorations, double characterSpacing)
    {
        var spannableString = GetSpannableString(text, textDecorations, characterSpacing);
        textView.LetterSpacing = (float)characterSpacing;
        textView.SetText(spannableString, TextView.BufferType.Spannable);
    }
    
    public static void SetText(this Button button, string text, TextDecorations textDecorations)
    {
        var spannableString = GetSpannableString(text, textDecorations, 0);
        button.SetText(spannableString, TextView.BufferType.Spannable);
    }

    private static SpannableString GetSpannableString(string text, TextDecorations textDecorations, double characterSpacing)
    {
        var spannableString = new SpannableString(text);
        CharacterStyle textStyle = textDecorations switch
        {
            TextDecorations.Underline => new UnderlineSpan(),
            TextDecorations.Strikethrough => new StrikethroughSpan(),
            _ => new LetterSpacingSpan((float)characterSpacing)
        };
        spannableString.SetSpan(textStyle, 0, text.Length, SpanTypes.ExclusiveExclusive);
        return spannableString;
    }
    
    public static void SetLineBreakMode(this TextView textView, LineBreakMode lineBreakMode, int textMaxLines)
    {
        switch (lineBreakMode)
        {
            case LineBreakMode.NoWrap:
            case LineBreakMode.TailTruncation:
                textView.SetSingleLine(true);
                textView.Ellipsize = Android.Text.TextUtils.TruncateAt.End;
                break;
            case LineBreakMode.HeadTruncation:
                textView.SetSingleLine(true);
                textView.Ellipsize = Android.Text.TextUtils.TruncateAt.Start;
                break;
            case LineBreakMode.MiddleTruncation:
                textView.SetSingleLine(true);
                textView.Ellipsize = Android.Text.TextUtils.TruncateAt.Middle;
                break;
            case LineBreakMode.WordWrap:
            case LineBreakMode.CharacterWrap:
                textView.Ellipsize = null;
                textView.SetMaxLines(textMaxLines);
                break;
        }
    }
    
    public static void SetFontAttributes(this TextView textView, FontAttributes fontAttributes)
    {
        switch (fontAttributes)
        {
            case FontAttributes.Bold:
                textView.SetTypeface(textView.Typeface, Android.Graphics.TypefaceStyle.Bold);
                break;
            case FontAttributes.Italic:
                textView.SetTypeface(textView.Typeface, Android.Graphics.TypefaceStyle.Italic);
                break;
            case FontAttributes.Bold | FontAttributes.Italic:
                textView.SetTypeface(textView.Typeface, Android.Graphics.TypefaceStyle.BoldItalic);
                break;
            default:
                textView.SetTypeface(textView.Typeface, Android.Graphics.TypefaceStyle.Normal);
                break;
        }
    }
}
