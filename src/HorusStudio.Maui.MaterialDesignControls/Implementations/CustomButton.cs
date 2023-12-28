using System;

#if IOS || MACCATALYST
using Foundation;
#endif

namespace HorusStudio.Maui.MaterialDesignControls.Implementations;

/// <summary>
/// An extended <see cref="Button" /> that adds new features to native one.
/// </summary>
class CustomButton : Button
{
    #region Attributes

#if ANDROID
    private Google.Android.Material.Button.MaterialButton _nativeView;
#elif IOS || MACCATALYST
    private UIKit.UIButton _nativeView;
#else
    private object _nativeView;
#endif

    #endregion

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="TextDecorations" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextDecorationsProperty = BindableProperty.Create(nameof(TextDecorations), typeof(TextDecorations), typeof(CustomButton), defaultValue: TextDecorations.None, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is CustomButton self)
        {
            self.UpdateTextDecorations();
        }
    });

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets <see cref="TextDecorations" /> for the text of the button.
    /// This is a bindable property.
    /// </summary>
    public TextDecorations TextDecorations
    {
        get => (TextDecorations)GetValue(TextDecorationsProperty);
        set => SetValue(TextDecorationsProperty, value);
    }

    #endregion

    public static void RegisterHandler()
    {
        Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping(nameof(CustomButton), (handler, view) =>
        {
            if (view is CustomButton customButton)
            {
                customButton._nativeView = handler.PlatformView;
            }
        });
    }

    private void UpdateTextDecorations()
    {
#if ANDROID

        _nativeView.PaintFlags &= ~Android.Graphics.PaintFlags.UnderlineText & ~Android.Graphics.PaintFlags.StrikeThruText;

        if (TextDecorations == TextDecorations.Underline)
        {
            _nativeView.PaintFlags |= Android.Graphics.PaintFlags.UnderlineText;
        }
        else if (TextDecorations == TextDecorations.Strikethrough)
        {
            _nativeView.PaintFlags |= Android.Graphics.PaintFlags.StrikeThruText;
        }

#elif IOS || MACCATALYST

        var text = _nativeView.Title(UIKit.UIControlState.Normal);
        var range = new NSRange(0, text.Length);

        var titleString = new NSMutableAttributedString(text);
        if (TextDecorations == TextDecorations.Underline)
        {
            titleString.AddAttribute(UIKit.UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), range);
        }
        else if (TextDecorations == TextDecorations.Strikethrough)
        {
            titleString.AddAttribute(UIKit.UIStringAttributeKey.StrikethroughStyle, NSNumber.FromInt32(2), range);
        }

        _nativeView.SetAttributedTitle(titleString, UIKit.UIControlState.Normal);

#elif WINDOWS
#endif

    }
}