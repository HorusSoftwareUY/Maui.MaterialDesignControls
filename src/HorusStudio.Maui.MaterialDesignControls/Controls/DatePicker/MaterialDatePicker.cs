using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A date picker <see cref="View" /> Date picker let users to select a date. They typically appear in forms and dialogs.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialDatePicker.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialButton
///     Placeholder="Creation date" /&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var button = new MaterialDatePicker
/// {
///     Placeholder="Creation date"
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/DatePickerPage.xaml)
/// 
/// </example>
/// <todoList>
/// * [iOS] Font attributes doesn´t work
/// * [iOS] Horizontal Text Aligment doesn´t work when there is a date selected
/// </todoList>
public class MaterialDatePicker : MaterialInputBase
{
    #region Attributes

    private readonly static double DefaultCharacterSpacing = MaterialFontTracking.BodyLarge;

    #endregion Attributes

    #region Layout

    private CustomDatePicker _datePicker;

    #endregion Layout

    #region Constructor

    public MaterialDatePicker()
    {
        _datePicker = new CustomDatePicker
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            IsVisible = false
        };

        _datePicker.SetBinding(DatePicker.DateProperty, new Binding(nameof(Date), source: this));
        _datePicker.SetBinding(DatePicker.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _datePicker.SetBinding(DatePicker.MinimumDateProperty, new Binding(nameof(MinimumDate), source: this));
        _datePicker.SetBinding(DatePicker.MaximumDateProperty, new Binding(nameof(MaximumDate), source: this));
        _datePicker.SetBinding(DatePicker.FormatProperty, new Binding(nameof(Format), source: this));
        _datePicker.SetBinding(DatePicker.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _datePicker.SetBinding(DatePicker.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _datePicker.SetBinding(DatePicker.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _datePicker.SetBinding(DatePicker.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _datePicker.SetBinding(DatePicker.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _datePicker.SetBinding(CustomDatePicker.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        _datePicker.SetBinding(CustomDatePicker.PlaceholderColorProperty, new Binding(nameof(PlaceholderColor), source: this));
        _datePicker.SetBinding(CustomDatePicker.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));

        InputTapCommand = new Command(() =>
        {
#if ANDROID
            var handler = _datePicker.Handler as IDatePickerHandler;
            handler.PlatformView.PerformClick();
#elif IOS || MACCATALYST
            _datePicker.Focus();
#endif
        });

        Content = _datePicker;

        Text = String.Empty;
    }

    #endregion Constructor

    #region BindableProperties

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialDatePicker), defaultValue: null);

#nullable enable
    /// <summary>
    /// The backing store for the <see cref="Date" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(MaterialDatePicker), defaultValue: null, propertyChanged: OnDateChanged, defaultBindingMode: BindingMode.TwoWay);
#nullable disable

    /// <summary>
    /// The backing store for the <see cref="MinimumDate" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(MaterialDatePicker), defaultValue: DateTime.MinValue);

    /// <summary>
    /// The backing store for the <see cref="MaximumDate" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(MaterialDatePicker), defaultValue: DateTime.MaxValue);

    /// <summary>
    /// The backing store for the <see cref="Format" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(nameof(Format), typeof(string), typeof(MaterialDatePicker), defaultValue: "MM/dd/yyyy");

    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialDatePicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialDatePicker), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialDatePicker), defaultValue: DefaultCharacterSpacing);

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
            return Date.HasValue ? Date.ToString() : null;
        }
        set => SetValue(TextProperty, Date.HasValue ? Date.ToString() : null);
    }

#nullable enable
    /// <summary>
    /// Gets or sets the Date. This is a bindable property.
    /// </summary>
    public DateTime? Date
    {
        get => (DateTime?)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }
#nullable disable

    /// <summary>
    /// Gets or sets the Minimum Date. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="DateTime.MinValue"/>
    /// </default>
    public DateTime MinimumDate
    {
        get => (DateTime)GetValue(MinimumDateProperty);
        set => SetValue(MinimumDateProperty, value);
    }

    /// <summary>
    /// Gets or sets the Maximum Date. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="DateTime.MaxValue"/>
    /// </default>
    public DateTime MaximumDate
    {
        get => (DateTime)GetValue(MaximumDateProperty);
        set => SetValue(MaximumDateProperty, value);
    }

    /// <summary>
    /// The format of the date to display to the user. This is a bindable property.
    /// </summary>
    /// <value>
    /// A valid date format.
    /// </value>
    /// <default>
    /// MM/dd/yyyy
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

    public event EventHandler DateSelected;

    public new event EventHandler<FocusEventArgs> Focused;

    public new event EventHandler<FocusEventArgs> Unfocused;

    #endregion Events

    #region Methods

    private static void OnDateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MaterialDatePicker)bindable;

        //If we set maximum date, date picker control set as default date.
        if (newValue is DateTime date && date == control.MaximumDate)
            return;

        control._datePicker.CustomDate = (DateTime?)newValue;                                                                                                                                                                                                                                                                                   
        control.DateSelected?.Invoke(control, new DateChangedEventArgs((DateTime)oldValue, (DateTime)newValue));
        control._datePicker.IsVisible = true;

        if (newValue is null)
            control._datePicker.IsVisible = false;

        control.Text = String.Empty;
    }

    protected override void SetControlTemplate(MaterialInputType type)
    {
        if (_datePicker == null) return;

#if ANDROID
        switch (type)
        {
            case MaterialInputType.Filled:
                _datePicker.VerticalOptions = LayoutOptions.Center;
                break;
            case MaterialInputType.Outlined:
                _datePicker.VerticalOptions = LayoutOptions.Center;
                break;
        }
#endif
    }

    protected override void SetControlIsEnabled()
    {
        if (_datePicker != null)
            _datePicker.IsEnabled = IsEnabled;
    }

    protected override void OnControlAppearing()
    {
        // Setup events/animations
        _datePicker.Focused += ContentFocusChanged;
        _datePicker.Unfocused += ContentFocusChanged;

    }

    protected override void OnControlDisappearing()
    {
        // Cleanup events/animations
        _datePicker.Focused -= ContentFocusChanged;
        _datePicker.Unfocused -= ContentFocusChanged;
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
            if (!this._datePicker.CustomDate.HasValue)
                Date = this._datePicker.InternalDateTime;
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
        var style = new Style(typeof(MaterialDatePicker)) { ApplyToDerivedTypes = true };

        var baseStyles = MaterialInputBase.GetBaseStyles();

        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, baseStyles);

        return new List<Style> { style };
    }

    #endregion Styles
}


