using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

public class MaterialTextField : MaterialInputBase
{
    //TODO [iOS] FontAttributes doesn´t work

    #region Attributes

    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialLightTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultCharacterSpacing = MaterialFontTracking.BodyLarge;
    private readonly static Color DefaultCursorColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialLightTheme.Primary }.GetValueForCurrentTheme<Color>();

    #endregion Attributes

    #region Layout

    private BorderlessEntry _entry;

    #endregion Layout

    #region Constructor

    public MaterialTextField()
    {
        _entry = new BorderlessEntry
        {
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        _entry.SetBinding(BorderlessEntry.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        _entry.SetBinding(BorderlessEntry.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _entry.SetBinding(BorderlessEntry.TextProperty, new Binding(nameof(Text), source: this));
        _entry.SetBinding(BorderlessEntry.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _entry.SetBinding(BorderlessEntry.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _entry.SetBinding(BorderlessEntry.PlaceholderColorProperty, new Binding(nameof(PlaceholderColor), source: this));
        _entry.SetBinding(BorderlessEntry.IsPasswordProperty, new Binding(nameof(IsPassword), source: this));
        _entry.SetBinding(BorderlessEntry.KeyboardProperty, new Binding(nameof(Keyboard), source: this));
        _entry.SetBinding(BorderlessEntry.TextTransformProperty, new Binding(nameof(TextTransform), source: this));
        _entry.SetBinding(BorderlessEntry.ReturnTypeProperty, new Binding(nameof(ReturnType), source: this));
        _entry.SetBinding(BorderlessEntry.ReturnCommandProperty, new Binding(nameof(ReturnCommand), source: this));
        _entry.SetBinding(BorderlessEntry.ReturnCommandParameterProperty, new Binding(nameof(ReturnCommandParameter), source: this));
        _entry.SetBinding(BorderlessEntry.MaxLengthProperty, new Binding(nameof(MaxLength), source: this));
        _entry.SetBinding(BorderlessEntry.CursorPositionProperty, new Binding(nameof(CursorPosition), source: this));
        _entry.SetBinding(BorderlessEntry.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
        _entry.SetBinding(BorderlessEntry.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _entry.SetBinding(BorderlessEntry.ClearButtonVisibilityProperty, new Binding(nameof(ClearButtonVisibility), source: this));
        _entry.SetBinding(BorderlessEntry.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _entry.SetBinding(BorderlessEntry.IsTextPredictionEnabledProperty, new Binding(nameof(IsTextPredictionEnabled), source: this));
        _entry.SetBinding(BorderlessEntry.IsSpellCheckEnabledProperty, new Binding(nameof(IsSpellCheckEnabled), source: this));
        _entry.SetBinding(BorderlessEntry.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _entry.SetBinding(BorderlessEntry.IsReadOnlyProperty, new Binding(nameof(IsReadOnly), source: this));
        _entry.SetBinding(BorderlessEntry.CursorColorProperty, new Binding(nameof(CursorColor), source: this));

        InputTapCommand = new Command(() =>  _entry.Focus());

#if ANDROID
        _entry.ReturnCommand = new Command(() =>
        {
            var view = _entry.Handler.PlatformView as Android.Views.View;
            view?.ClearFocus();

            if (ReturnCommand?.CanExecute(ReturnCommandParameter) ?? false)
            {
                ReturnCommand.Execute(ReturnCommandParameter);
            }
        });
#endif
        _entry.TextChanged += TxtEntry_TextChanged;

        Content = _entry;
    }

    #endregion Constructor

    #region BindableProperties

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialTextField), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// The backing store for the <see cref="IsPassword" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialTextField), defaultValue: false);

    /// <summary>
    /// The backing store for the <see cref="Keyboard" /> bindable property.
    /// </summary>
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialTextField), defaultValue: Keyboard.Text, propertyChanged: (bindableObject, _, newValue) => 
    { 
        if (bindableObject is MaterialTextField self && newValue is Keyboard value)
        {
            self._entry.Keyboard = value;
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TextTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(MaterialTextField), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="ReturnType" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(MaterialTextField), defaultValue: ReturnType.Default);

    /// <summary>
    /// The backing store for the <see cref="ReturnCommand" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(MaterialTextField), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="ReturnCommandParameter" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ReturnCommandParameterProperty = BindableProperty.Create(nameof(ReturnCommandParameter), typeof(object), typeof(MaterialTextField), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="MaxLength" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaterialTextField), defaultValue: Int32.MaxValue);

    /// <summary>
    /// The backing store for the <see cref="CursorPosition" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CursorPositionProperty = BindableProperty.Create(nameof(CursorPosition), typeof(int), typeof(MaterialTextField), defaultValue: 0);

    /// <summary>
    /// The backing store for the <see cref="TextChangedCommand" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextChangedCommandProperty = BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(MaterialTextField), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="VerticalTextAlignment" /> bindable property.
    /// </summary>
    public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(MaterialTextField), defaultValue: null); 
    
    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialTextField), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="ClearButtonVisibility" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ClearButtonVisibilityProperty = BindableProperty.Create(nameof(ClearButtonVisibility), typeof(ClearButtonVisibility), typeof(MaterialTextField), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialTextField), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="IsTextPredictionEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IsTextPredictionEnabledProperty = BindableProperty.Create(nameof(IsTextPredictionEnabled), typeof(bool), typeof(MaterialTextField), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="IsSpellCheckEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IsSpellCheckEnabledProperty = BindableProperty.Create(nameof(IsSpellCheckEnabled), typeof(bool), typeof(MaterialTextField), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialTextField), defaultValue: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="IsReadOnly" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(MaterialTextField), defaultValue: false);

    /// <summary>
    /// The backing store for the <see cref="CursorColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CursorColorProperty = BindableProperty.Create(nameof(CursorColor), typeof(Color), typeof(MaterialTextField), defaultValue: DefaultCursorColor);

    #endregion BindableProperties

    #region Properties

    /// <summary>
    /// Gets or sets the text displayed as the content of the input.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets if the input is password. This is a bindable property.
    /// </summary>
    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
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
    ///  Determines what the return key on the on-screen keyboard should look like. This is a bindable property.
    /// </summary>
    public ReturnType ReturnType
    {
        get => (ReturnType)GetValue(ReturnTypeProperty);
        set => SetValue(ReturnTypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to run when the user presses the return key, either
    /// physically or on the on-screen keyboard. This is a bindable property.
    /// </summary>
    public ICommand ReturnCommand
    {
        get => (ICommand)GetValue(ReturnCommandProperty);
        set => SetValue(ReturnCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter object for the Microsoft.Maui.Controls.Entry.ReturnCommand
    /// that can be used to provide extra information. This is a bindable property.
    /// </summary>
    public object ReturnCommandParameter
    {
        get => (object)GetValue(ReturnCommandParameterProperty);
        set => SetValue(ReturnCommandParameterProperty, value);
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
    /// Gets or sets a value that indicates whether the font for the text of this entry
    /// is bold, italic, or neither. This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    /// <summary>
    /// Determines the behavior of the clear text button on this entry. This is a bindable
    /// property.
    /// </summary>
    public ClearButtonVisibility ClearButtonVisibility
    {
        get => (ClearButtonVisibility)GetValue(ClearButtonVisibilityProperty);
        set => SetValue(ClearButtonVisibilityProperty, value);
    }

    /// <summary>
    /// Determines whether or not the font of this entry should scale automatically according
    /// to the operating system settings. Default value is true. This is a bindable property.
    /// Default value is true
    /// </summary>
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
    /// Default value is true.
    /// </summary>
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
    /// Gets or sets a value that indicates whether user should be prevented from modifying the text. Default is false.
    /// <value>If true, user cannot modify text. Else, false.</value>
    /// </summary>
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
    /// <remarks>
    /// This Property only works on iOS and 'ndroid' 29 or later
    /// </remarks>
    public Color CursorColor
    {
        get => (Color)GetValue(CursorColorProperty);
        set => SetValue(CursorColorProperty, value);
    }

    #endregion Properties

    #region Events

    public event EventHandler TextChanged;

    public new event EventHandler<FocusEventArgs> Focused;

    public new event EventHandler<FocusEventArgs> Unfocused;

    #endregion Events

    #region Methods

    private void TxtEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var changedByTextTransform = Text != null && _entry.Text != null && Text.ToLower() == _entry.Text.ToLower();
        this.Text = this._entry.Text;

        if (!changedByTextTransform)
        {
            this.TextChangedCommand?.Execute(null);
            this.TextChanged?.Invoke(this, e);
        }
    }

    protected override void SetControlTemplate(MaterialInputType type)
    {
#if ANDROID
        if (_entry == null) return;

        switch (type)
        {
            case MaterialInputType.Filled:
                _entry.VerticalOptions = LayoutOptions.End;
                _entry.Margin = new Thickness(0, 0, 0, -8);
                break;
            case MaterialInputType.Outlined:
                _entry.VerticalOptions = LayoutOptions.Center;
                _entry.Margin = new Thickness(0, -7.5);
                break;
        }
#endif
    }

    protected override void SetControlIsEnabled()
    {
        if (_entry != null)
            _entry.IsEnabled = IsEnabled;
    }

    protected override void OnControlAppearing()
    {
        // Setup events/animations
        _entry.Focused += ContentFocusChanged;
        _entry.Unfocused += ContentFocusChanged;
    }

    protected override void OnControlDisappearing()
    {
        // Cleanup events/animations
        _entry.Focused -= ContentFocusChanged;
        _entry.Unfocused -= ContentFocusChanged;
    }

    private void ContentFocusChanged(object sender, FocusEventArgs e)
    {
        IsFocused = e.IsFocused;
        VisualStateManager.GoToState(this, GetCurrentVisualState());
        UpdateLayoutAfterTypeChanged(Type);

        if (IsFocused || CanExecuteFocusedCommand())
        {
            FocusedCommand?.Execute(null);
            Focused?.Invoke(this, e);
        }
        else if (!IsFocused || CanExecuteUnfocusedCommand())
        {
            UnfocusedCommand?.Execute(null);
            Unfocused?.Invoke(this, e);
        }
    }

    private bool CanExecuteFocusedCommand()
    {
        return FocusedCommand?.CanExecute(null) ?? false;
    }

    private bool CanExecuteUnfocusedCommand()
    {
        return UnfocusedCommand?.CanExecute(null) ?? false;
    }

    #endregion Methods

    #region Styles
    internal static IEnumerable<Style> GetStyles()
    {
        var style = new Style(typeof(MaterialTextField)) { ApplyToDerivedTypes = true };

        var baseStyles = MaterialInputBase.GetBaseStyles();

        var errorFocusedGroup = baseStyles.First(g => g.Name.Equals(nameof(VisualStateManager.CommonStates)));
        baseStyles.Remove(errorFocusedGroup);

        var errorFocusedStates = errorFocusedGroup.States.First(s => s.Name.Equals(MaterialInputCommonStates.ErrorFocused));

        errorFocusedGroup.States.Remove(errorFocusedStates);

        errorFocusedStates.Setters.Add(
            MaterialTextField.CursorColorProperty,
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