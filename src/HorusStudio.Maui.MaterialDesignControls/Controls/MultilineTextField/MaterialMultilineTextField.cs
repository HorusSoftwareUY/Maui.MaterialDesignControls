using System.Windows.Input;

#if ANDROID
using Android.App;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Multiline text fields let users enter multiline text into a UI and follow Material Design Guidelines. <see href="https://m3.material.io/components/text-fields/overview">See more</see>.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialMultilineTextField.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialMultilineTextField
///     Placeholder="Enter text here" /&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var textField = new MaterialMultilineTextField
/// {
///     Placeholder = "Enter text here"
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/MultilineTextFieldPage.xaml)
/// 
/// </example>
/// <todoList>
/// * [iOS] FontAttributes doesn't work
/// * HeightRequest doesn't work, it is not respected when writing and making a text change.
/// * VerticalTextAlignment doesn't work
/// </todoList>
public class MaterialMultilineTextField : MaterialInputBase
{
    #region Attributes

    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCharacterSpacing = _ => MaterialFontTracking.BodyLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCursorColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();

    #endregion Attributes

    #region Layout

    private readonly CustomEditor? _editor;

    #endregion Layout

    #region Constructor

    public MaterialMultilineTextField()
    {
        _editor = new CustomEditor
        {
            HorizontalOptions = LayoutOptions.Fill,
            HeightRequest = -1.0
        };

        _editor.SetBinding(Editor.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        _editor.SetBinding(Editor.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _editor.SetBinding(Editor.TextProperty, new Binding(nameof(Text), source: this));
        _editor.SetBinding(Editor.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _editor.SetBinding(Editor.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _editor.SetBinding(InputView.KeyboardProperty, new Binding(nameof(Keyboard), source: this));
        _editor.SetBinding(InputView.TextTransformProperty, new Binding(nameof(TextTransform), source: this));
        _editor.SetBinding(InputView.MaxLengthProperty, new Binding(nameof(MaxLength), source: this));
        _editor.SetBinding(Editor.CursorPositionProperty, new Binding(nameof(CursorPosition), source: this));
        _editor.SetBinding(Editor.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
        _editor.SetBinding(Editor.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _editor.SetBinding(Editor.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _editor.SetBinding(Editor.IsTextPredictionEnabledProperty, new Binding(nameof(IsTextPredictionEnabled), source: this));
        _editor.SetBinding(InputView.IsSpellCheckEnabledProperty, new Binding(nameof(IsSpellCheckEnabled), source: this));
        _editor.SetBinding(Editor.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _editor.SetBinding(InputView.IsReadOnlyProperty, new Binding(nameof(IsReadOnly), source: this));
        _editor.SetBinding(CustomEditor.CursorColorProperty, new Binding(nameof(CursorColor), source: this));
        _editor.SetBinding(Editor.AutoSizeProperty, new Binding(nameof(AutoSize), source: this));

        InputTapCommand = new Command(() => Focus());
        LeadingIconCommand = new Command(() => Focus());
        TrailingIconCommand = new Command(() => Focus());

        Content = _editor;
    }

    #endregion Constructor

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Text">Text</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialMultilineTextField), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// The backing store for the <see cref="Keyboard">Keyboard</see> bindable property.
    /// </summary>
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialMultilineTextField), defaultValue: Keyboard.Text);

    /// <summary>
    /// The backing store for the <see cref="TextTransform">TextTransform</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(MaterialMultilineTextField), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="MaxLength">MaxLength</see> bindable property.
    /// </summary>
    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaterialMultilineTextField), defaultValue: Int32.MaxValue);

    /// <summary>
    /// The backing store for the <see cref="CursorPosition">CursorPosition</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CursorPositionProperty = BindableProperty.Create(nameof(CursorPosition), typeof(int), typeof(MaterialMultilineTextField), defaultValue: 0);

    /// <summary>
    /// The backing store for the <see cref="TextChangedCommand">TextChangedCommand</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextChangedCommandProperty = BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(MaterialMultilineTextField), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="VerticalTextAlignment">VerticalTextAlignment</see> bindable property.
    /// </summary>
    public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(MaterialMultilineTextField), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled">FontAutoScalingEnabled</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialMultilineTextField), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="IsTextPredictionEnabled">IsTextPredictionEnabled</see> bindable property.
    /// </summary>
    public static readonly BindableProperty IsTextPredictionEnabledProperty = BindableProperty.Create(nameof(IsTextPredictionEnabled), typeof(bool), typeof(MaterialMultilineTextField), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="IsSpellCheckEnabled">IsSpellCheckEnabled</see> bindable property.
    /// </summary>
    public static readonly BindableProperty IsSpellCheckEnabledProperty = BindableProperty.Create(nameof(IsSpellCheckEnabled), typeof(bool), typeof(MaterialMultilineTextField), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing">CharacterSpacing</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialMultilineTextField), defaultValueCreator: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="IsReadOnly">IsReadOnly</see> bindable property.
    /// </summary>
    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(MaterialMultilineTextField), defaultValue: false);

    /// <summary>
    /// The backing store for the <see cref="CursorColor">CursorColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CursorColorProperty = BindableProperty.Create(nameof(CursorColor), typeof(Color), typeof(MaterialMultilineTextField), defaultValueCreator: DefaultCursorColor);

    /// <summary>
    /// The backing store for the <see cref="AutoSize">AutoSize</see> bindable property.
    /// </summary>
    public static readonly BindableProperty AutoSizeProperty = BindableProperty.Create(nameof(AutoSize), typeof(EditorAutoSizeOption), typeof(MaterialMultilineTextField), defaultValue: EditorAutoSizeOption.TextChanges, propertyChanged: (bindableObject, _, newValue) => 
    {
        if (bindableObject is MaterialMultilineTextField self && newValue is EditorAutoSizeOption autoSizeOption)
        {
            self.UpdateEditorHeight(autoSizeOption);
        }
    });

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Internal implementation of the <see cref="Editor">Editor</see> control.
    /// </summary>
    /// <remarks>
    /// This property can affect the internal behavior of this control. Use only if you fully understand the potential impact.
    /// </remarks>
    public Editor? InternalEditor => _editor;

    /// <summary>
    /// Gets or sets the text displayed as the content of the input.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets input's keyboard. This is a bindable property.
    /// </summary>
    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    /// <summary>
    /// Gets or sets input's texttransform. This is a bindable property.
    /// </summary>
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value);
    }

    /// <summary>
    /// Gets or sets input's max length. This is a bindable property.
    /// </summary>
    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    /// <summary>
    /// Gets or sets input's cursor position. This is a bindable property.
    /// </summary>
    public int CursorPosition
    {
        get => (int)GetValue(CursorPositionProperty);
        set => SetValue(CursorPositionProperty, value);
    }

    /// <summary>
    /// Gets or sets input's text changed command. This is a bindable property.
    /// </summary>
    public ICommand TextChangedCommand
    {
        get => (ICommand)GetValue(TextChangedCommandProperty);
        set => SetValue(TextChangedCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the vertical text alignment. This is a bindable property.
    /// </summary>
    public TextAlignment VerticalTextAlignment
    {
        get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
        set => SetValue(VerticalTextAlignmentProperty, value);
    }

    /// <summary>
    /// Determines whether or not the font of this entry should scale automatically according
    /// to the operating system settings. Default value is true. This is a bindable property.
    /// </summary>
    /// <default>
    /// True
    /// </default>
    /// <remarks>
    /// Typically this should always be enabled for accessibility reasons.
    /// </remarks>
    public bool FontAutoScalingEnabled
    {
        get => (bool)GetValue(FontAutoScalingEnabledProperty);
        set => SetValue(FontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Determines whether text prediction and automatic text correction is enabled.
    /// </summary>
    /// <default>
    /// True
    /// </default>
    public bool IsTextPredictionEnabled
    {
        get => (bool)GetValue(IsTextPredictionEnabledProperty);
        set => SetValue(IsTextPredictionEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that controls whether spell checking is enabled.
    /// <value>true if spell checking is enabled. Otherwise false.</value>
    /// </summary>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public bool IsSpellCheckEnabled
    {
        get => (bool)GetValue(IsSpellCheckEnabledProperty);
        set => SetValue(IsSpellCheckEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates the number of device-independent units that
    /// should be in between characters in the text displayed by the Entry. Applies to
    /// Text and Placeholder.
    /// <value>The number of device-independent units that should be in between characters in the text.</value>
    /// </summary>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates whether user should be prevented from modifying the text.
    /// <value>If true, user cannot modify text. Else, false.</value>
    /// </summary>
    /// <default>
    /// False
    /// </default>
    /// <remarks>
    /// The IsReadonly property does not alter the visual appearance of the control,  unlike the IsEnabled property that also changes the visual appearance of the control
    /// </remarks>
    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    /// <summary>
    /// Gets or sets a color of the caret indicator.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark: <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
    /// </default>
    /// <remarks>
    /// This Property only works on iOS and 'Android' 29 or later
    /// </remarks>
    public Color CursorColor
    {
        get => (Color)GetValue(CursorColorProperty);
        set => SetValue(CursorColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that controls whether the editor will change size to accommodate
    /// input as the user enters it.
    /// <value>Whether the editor will change size to accommodate input as the user enters it.</value>
    /// </summary>
    /// <remarks>
    /// Automatic resizing is turned off by default.
    /// </remarks>
    public EditorAutoSizeOption AutoSize
    {
        get => (EditorAutoSizeOption)GetValue(AutoSizeProperty);
        set => SetValue(AutoSizeProperty, value);
    }

    #endregion Properties

    #region Events

    public event EventHandler? TextChanged;
    
    #endregion Events

    #region Methods

    private void TxtEditor_TextChanged(object? sender, TextChangedEventArgs e)
    {
        var invokeTextChanged = true;

        if (_editor?.Text != null)
        {
            if (TextTransform == TextTransform.Lowercase)
            {
                invokeTextChanged = _editor.Text.Where(char.IsLetter).All(char.IsLower);
            }
            else if (TextTransform == TextTransform.Uppercase)
            {
                invokeTextChanged = _editor.Text.Where(char.IsLetter).All(char.IsUpper);
            }
        }

        if (invokeTextChanged)
        {
            TextChangedCommand?.Execute(null);
            TextChanged?.Invoke(this, e);
        }

        UpdateEditorHeight(AutoSize);
    }

    protected override void SetControlTemplate(MaterialInputType type)
    {
        if (_editor == null) return;

#if ANDROID
        var horizontalOffset = 3;
        var verticalOffset = -7.5;
#elif IOS || MACCATALYST
        var horizontalOffset = -5;
        var verticalOffset = -5;
#endif
        switch (type)
        {
            case MaterialInputType.Filled:
                _editor.VerticalOptions = LayoutOptions.End;
#if ANDROID
                _editor.Margin = new Thickness(horizontalOffset, 0, 0, verticalOffset);
#elif IOS || MACCATALYST
                if (HeightRequest <= DefaultHeightRequest)
                    _editor.Margin = new Thickness(horizontalOffset, (-1)*verticalOffset, 0, verticalOffset);
#endif
                break;
            case MaterialInputType.Outlined:
                _editor.VerticalOptions = LayoutOptions.Center;
#if ANDROID
                _editor.Margin = new Thickness(horizontalOffset, verticalOffset, 0, verticalOffset);
#elif IOS || MACCATALYST
                if (HeightRequest <= DefaultHeightRequest)
                    _editor.Margin = new Thickness(horizontalOffset, verticalOffset, 0, verticalOffset);
#endif
                break;
        }
    }

    protected override void SetControlIsEnabled()
    {
        if (_editor != null)
            _editor.IsEnabled = IsEnabled;
    }

    protected override void OnControlAppearing()
    {
        if (_editor == null)
        {
            return;
        }
        
        // Setup events/animations
        _editor.Focused += ContentFocusChanged;
        _editor.Unfocused += ContentFocusChanged;
        _editor.TextChanged += TxtEditor_TextChanged;
    }

    protected override void OnControlDisappearing()
    {
        if (_editor == null)
        {
            return;
        }
        
        // Cleanup events/animations
        _editor.Focused -= ContentFocusChanged;
        _editor.Unfocused -= ContentFocusChanged;
        _editor.TextChanged -= TxtEditor_TextChanged;
    }
    
    private void UpdateEditorHeight(EditorAutoSizeOption autoSizeOption)
    {
        if (autoSizeOption == EditorAutoSizeOption.TextChanges)
        {
            this.HeightRequest = -1.0;
            this.InvalidateMeasure();
        }
    }

    /// <summary>
    /// Attempts to set focus to this element.
    /// </summary>
    /// <returns>true if the keyboard focus was set to this element; false if the call to this method did not force a focus change.</returns>
    public new bool Focus()
    {
        if (_editor != null && !IsReadOnly)
        {
            return _editor.Focus();
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Unsets keyboard focus on this element.
    /// </summary>
    public new void Unfocus()
    {
        if (_editor != null)
        {
            _editor.Unfocus();

#if ANDROID
            var view = _editor?.Handler?.PlatformView as Android.Views.View;
            Platform.CurrentActivity?.HideKeyboard(view);
#endif
        }
    }

    #endregion Methods

    #region Styles
    internal static IEnumerable<Style> GetStyles()
    {
        var style = new Style(typeof(MaterialMultilineTextField)) { ApplyToDerivedTypes = true };

        var baseStyles = MaterialInputBase.GetBaseStyles();

        var errorFocusedGroup = baseStyles.First(g => g.Name.Equals(nameof(VisualStateManager.CommonStates)));
        baseStyles.Remove(errorFocusedGroup);

        var errorFocusedStates = errorFocusedGroup.States.First(s => s.Name.Equals(MaterialInputCommonStates.ErrorFocused));

        errorFocusedGroup.States.Remove(errorFocusedStates);

        errorFocusedStates.Setters.Add(
            MaterialMultilineTextField.CursorColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Error,
                Dark = MaterialDarkTheme.Error
            }
            .GetValueForCurrentTheme<Color>());

        errorFocusedGroup.States.Add(errorFocusedStates);
        baseStyles.Add(errorFocusedGroup);

        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, baseStyles);

        return new List<Style> { style };
    }

    #endregion Styles
}
