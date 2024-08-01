using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

public class MaterialTimePicker : MaterialInputBase
{
    //TODO: [iOS] Font attributes doesn´t work
    //TODO: [iOS] Horizontal Text Aligment doesn´t work when there is a date selected

    #region Attributes

    private readonly static double DefaultCharacterSpacing = MaterialFontTracking.BodyLarge;

    #endregion Attributes

    #region Layout

    private CustomTimePicker _timePicker;

    #endregion Layout

    #region Constructor

    public MaterialTimePicker()
    {
        _timePicker = new CustomTimePicker()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            IsVisible = false
        };

        _timePicker.SetBinding(TimePicker.TimeProperty, new Binding(nameof(Time), source: this));
        _timePicker.SetBinding(TimePicker.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _timePicker.SetBinding(TimePicker.FormatProperty, new Binding(nameof(Format), source: this));
        _timePicker.SetBinding(TimePicker.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _timePicker.SetBinding(TimePicker.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _timePicker.SetBinding(TimePicker.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _timePicker.SetBinding(TimePicker.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _timePicker.SetBinding(TimePicker.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _timePicker.SetBinding(CustomTimePicker.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        _timePicker.SetBinding(CustomTimePicker.PlaceholderColorProperty, new Binding(nameof(PlaceholderColor), source: this));
        _timePicker.SetBinding(CustomTimePicker.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));

        InputTapCommand = new Command(() =>
        {
#if ANDROID
            var handler = _timePicker.Handler as ITimePickerHandler;
            handler.PlatformView.PerformClick();
#elif IOS || MACCATALYST
            _timePicker.Focus();
#endif
        });

        Content = _timePicker;

        Text = String.Empty;
    }

    #endregion Constructor

    #region BindableProperties

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialTimePicker), defaultValue: null);

#nullable enable
    /// <summary>
    /// The backing store for the <see cref="Time" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan?), typeof(MaterialTimePicker), defaultValue: null, propertyChanged: OnTimeChanged, defaultBindingMode: BindingMode.TwoWay);
#nullable disable

    /// <summary>
    /// The backing store for the <see cref="Format" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(nameof(Format), typeof(string), typeof(MaterialTimePicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialTimePicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialTimePicker), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialTimePicker), defaultValue: DefaultCharacterSpacing);

    #endregion BindableProperties

    #region Properties

    /// <summary>
    /// Gets or sets the text displayed as the content of the input. This property cannot be changed by the user.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public string Text
    {
        get
        {
            return Time.HasValue ? Time.ToString() : null;
        }
        set => SetValue(TextProperty, Time.HasValue ? Time.ToString() : null);
    }

#nullable enable
    /// <summary>
    /// Gets or sets the displayed time. This is a bindable property.
    /// <value>
    /// The <see cref="System.TimeSpan"/> displayed in the TimePicker.
    /// </value>
    /// </summary>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public TimeSpan? Time
    {
        get => (TimeSpan?)GetValue(TimeProperty);
        set => SetValue(TimeProperty, value);
    }
#nullable disable

    //TODO: change default value
    /// <summary>
    /// The format of the time to display to the user. This is a bindable property.
    /// </summary>
    /// <value>
    /// A valid time format string.
    /// </value>
    /// <default>
    /// null
    /// </default>
    /// <remarks>
    /// Format string is the same is passed to DateTime.ToString (string format).
    /// </remarks>
    public string Format
    {
        get => (string)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
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

    #endregion Properties

    #region Events

    public new event EventHandler<FocusEventArgs> Focused;

    public new event EventHandler<FocusEventArgs> Unfocused;

    #endregion Events

    #region Methods

    private static void OnTimeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MaterialTimePicker)bindable;

        ////If we set maximum date, date picker control set as default date.
        //if (newValue is DateTime date && date == control.MaximumDate)
        //    return;

        //control._timePicker.CustomDate = (DateTime?)newValue;
        control._timePicker.IsVisible = true;

        if (newValue is null)
            control._timePicker.IsVisible = false;

        control.Text = String.Empty;
    }

    protected override void SetControlTemplate(MaterialInputType type)
    {
        if (_timePicker == null) return;

#if ANDROID
        switch (type)
        {
            case MaterialInputType.Filled:
                _timePicker.VerticalOptions = LayoutOptions.Center;
                break;
            case MaterialInputType.Outlined:
                _timePicker.VerticalOptions = LayoutOptions.Center;
                break;
        }
#endif
    }

    protected override void SetControlIsEnabled()
    {
        if (_timePicker != null)
            _timePicker.IsEnabled = IsEnabled;
    }

    protected override void OnControlAppearing()
    {
        // Setup events/animations
        _timePicker.Focused += ContentFocusChanged;
        _timePicker.Unfocused += ContentFocusChanged;

    }

    protected override void OnControlDisappearing()
    {
        // Cleanup events/animations
        _timePicker.Focused -= ContentFocusChanged;
        _timePicker.Unfocused -= ContentFocusChanged;
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

            // Set the default date if the user doesn't select anything
            //if (!this._timePicker.CustomDate.HasValue)
            //    Time = this._timePicker.InternalDateTime;
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
        var style = new Style(typeof(MaterialTimePicker)) { ApplyToDerivedTypes = true };

        var baseStyles = MaterialInputBase.GetBaseStyles();

        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, baseStyles);

        return new List<Style> { style };
    }

    #endregion Styles
}
