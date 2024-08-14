using System.Runtime.InteropServices;
using CoreGraphics;
using CoreText;
using Foundation;
using HorusStudio.Maui.MaterialDesignControls.Extensions.NativeControl;
using HorusStudio.Maui.MaterialDesignControls.Views;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

public class SnackbarView : Alert, IDisposable
{
    internal const float DefaultPadding = 10;
    
    readonly PaddedButton _leadingButton;
    readonly PaddedLabel _messageLabel;
    readonly PaddedButton _actionButton;
    readonly PaddedButton _trailingButton;
    
    bool isDisposed;

    public SnackbarView(
        string leadingIcon,
        string message,
        string trailingIcon,
        UIColor backgroundColor,
        CGRect cornerRadius,
        UIColor textColor,
        UIFont font,
        double characterSpacing,
        string actionButtonText,
        UIColor actionTextColor,
        UIFont actionButtonFont,
        nfloat padding)
    {
        padding += DefaultPadding;

        _messageLabel = new PaddedLabel(padding, padding, padding, padding)
        {
            Lines = new IntPtr(0)
        };
        
        Message = message;
        TextColor = textColor;
        Font = font;
        CharacterSpacing = characterSpacing;
        FloatView.VisualOptions.BackgroundColor = backgroundColor;
        FloatView.VisualOptions.CornerRadius = cornerRadius;

        UIImage? leadingImage = (string.IsNullOrEmpty(leadingIcon)) ? null : new UIImage(leadingIcon);
        UIImage? trailingImage = (string.IsNullOrEmpty(trailingIcon)) ? null : new UIImage(trailingIcon);

        _actionButton = new PaddedButton(padding, padding, padding, padding, null);
        _leadingButton = new PaddedButton(padding, padding, padding, padding, leadingImage);
        _trailingButton = new PaddedButton(padding, padding, padding, padding, trailingImage);
        
        _actionButton.SetContentCompressionResistancePriority((float)UILayoutPriority.Required, UILayoutConstraintAxis.Horizontal);
        _actionButton.SetContentHuggingPriority((float)UILayoutPriority.Required, UILayoutConstraintAxis.Horizontal);
        
        ActionButtonText = actionButtonText;
        ActionTextColor = actionTextColor;
        ActionButtonFont = actionButtonFont;

        _actionButton.TouchUpInside += ActionButton_label;
        _leadingButton.TouchUpInside += ActionButton_leading;
        _trailingButton.TouchUpInside += ActionButton_trailing;
        
        if(!string.IsNullOrEmpty(leadingIcon))
            FloatView.AddChild(_leadingButton);
        FloatView.AddChild(_messageLabel);
        FloatView.AddChild(_actionButton);
        if(!string.IsNullOrEmpty(trailingIcon))
            FloatView.AddChild(_trailingButton);
    }
    
    void ActionButton_label(object? sender, EventArgs e)
    {
        ActionLabel?.Invoke();
        FloatView.Dismiss();
    }
    
    void ActionButton_leading(object? sender, EventArgs e)
    {
        ActionLeading?.Invoke();
        Dismiss();
    }
    
    void ActionButton_trailing(object? sender, EventArgs e)
    {
        ActionTrailing?.Invoke();
        Dismiss();
    }

    public Action? ActionLabel { get; init; }
    public Action? ActionLeading { get; init; }
    public Action? ActionTrailing { get; init; }
    

    ~SnackbarView() => Dispose(false);

    /// <summary>
    /// Toast Message
    /// </summary>
    public string Message
    {
        get => _messageLabel.Text ??= string.Empty;
        private init => _messageLabel.Text = value;
    }
    
    /// <summary>
    /// Toast Text Color
    /// </summary>
    public UIColor TextColor
    {
        get => _messageLabel.TextColor ??= new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialLightTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>().ToPlatform();
        private init => _messageLabel.TextColor = value;
    }

    /// <summary>
    /// Toast Font
    /// </summary>
    public UIFont Font
    {
        get => _messageLabel.Font;
        private init => _messageLabel.Font = value;
    }

    /// <summary>
    /// Toast CharacterSpacing
    /// </summary>
    public double CharacterSpacing
    {
        init
        {
            var em = Font.PointSize > 0 ? GetEmFromPx(Font.PointSize, value) : 0;
            _messageLabel.AttributedText = new NSAttributedString(Message, new CTStringAttributes() { KerningAdjustment = (float)em });
        }
    }
    
    /// <summary>
    /// Text Displayed on Action Button
    /// </summary>
    public string ActionButtonText
    {
        get => _actionButton.Title(UIControlState.Normal);
        private init => _actionButton.SetTitle(value, UIControlState.Normal);
    }

    /// <summary>
    /// Action Button Text Color
    /// </summary>
    public UIColor ActionTextColor
    {
        get => _actionButton.TitleColor(UIControlState.Normal);
        private init => _actionButton.SetTitleColor(value, UIControlState.Normal);
    }

    /// <summary>
    /// Action Button Font
    /// </summary>
    public UIFont ActionButtonFont
    {
        get => _actionButton.TitleLabel.Font;
        private init => _actionButton.TitleLabel.Font = value;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (isDisposed)
        {
            _actionButton.TouchUpInside -= ActionButton_label;
            _leadingButton.TouchUpInside -= ActionButton_leading;
            _trailingButton.TouchUpInside -= ActionButton_trailing;
        }
        
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    protected virtual void Dispose(bool isDisposing)
    {
        if (!isDisposed)
        {
            if (isDisposing)
            {
                _messageLabel.Dispose();
            }

            isDisposed = true;
        }
    }

    static nfloat GetEmFromPx(nfloat defaultFontSize, double currentValue) => 100 * (NFloat)currentValue / defaultFontSize;
}

public class SnackbarOptions : ITextStyle
{
    public double CharacterSpacing { get; set; }
    
    public Microsoft.Maui.Font Font { get; set; }

    public string Text { get; set; }
    
    public Color TextColor { get; set; }
    
    public Microsoft.Maui.Font ActionButtonFont { get; set; }
    
    public Color ActionButtonTextColor { get; set; }
    
    public string ActionButtonText { get; set; }
    
    public ImageSource LeadingImage { get; set; }
    
    public ImageSource TrailingImage { get; set; }
    
    public Color BackgroundColor { get; set; }
    
    public CornerRadius CornerRadius { get; set; }
    
}