using System.Windows.Input;
#if ANDROID
using Microsoft.Maui.Handlers;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Date Pickers let users select a date. They typically appear in forms and dialogs and, partially, follow Material Design Guidelines. <see href="https://m3.material.io/components/date-pickers/overview">See more.</see>
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
/// var datePicker = new MaterialDatePicker
/// {
///     Placeholder="Creation date"
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/DatePickerPage.xaml)
/// 
/// </example>
/// <todoList>
/// * [iOS] Font attributes doesn't work
/// * [iOS] Horizontal text alignment doesn't work when there is a date selected
/// * [Android] Use the colors defined in Material in the date picker dialog
/// </todoList>
public class MaterialDatePicker : MaterialInputBase
{
    #region Attributes

    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCharacterSpacing = _ => MaterialFontTracking.BodyLarge;

    #endregion Attributes

    #region Layout

    private readonly CustomDatePicker _datePicker;

    #endregion Layout

    #region Constructor

    public MaterialDatePicker()
    {
        _datePicker = new CustomDatePicker
        {
            HorizontalOptions = LayoutOptions.Fill,
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
        
        InputTapCommand = new Command(() => Focus());
        LeadingIconCommand = new Command(() => Focus());
        TrailingIcon = MaterialIcon.DatePicker;
        TrailingIconCommand = new Command(() => Focus());
        Content = _datePicker;
    }

    #endregion Constructor

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Text">Text</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialDatePicker), defaultValue: null);

#nullable enable
    /// <summary>
    /// The backing store for the <see cref="Date">Date</see> bindable property.
    /// </summary>
    public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(MaterialDatePicker), defaultValue: null, defaultBindingMode: BindingMode.TwoWay, propertyChanged:
        (bindable, oldValue, newValue) =>
        {
            var self = (MaterialDatePicker)bindable;
            self.OnDateChanged(oldValue as DateTime?, newValue as DateTime?);
        });
#nullable disable

    /// <summary>
    /// The backing store for the <see cref="MinimumDate">MinimumDate</see> bindable property.
    /// </summary>
    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(MaterialDatePicker), defaultValue: DateTime.MinValue);

    /// <summary>
    /// The backing store for the <see cref="MaximumDate">MaximumDate</see> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(MaterialDatePicker), defaultValue: DateTime.MaxValue);

    /// <summary>
    /// The backing store for the <see cref="Format">Format</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(nameof(Format), typeof(string), typeof(MaterialDatePicker), defaultValue: MaterialFormat.DateFormat);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled">FontAutoScalingEnabled</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialDatePicker), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing">CharacterSpacing</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialDatePicker), defaultValueCreator: DefaultCharacterSpacing);
    
    /// <summary>
    /// The backing store for the <see cref="DateSelectedCommand">DateSelectedCommand</see> bindable property.
    /// </summary>
    public static readonly BindableProperty DateSelectedCommandProperty = BindableProperty.Create(nameof(DateSelectedCommand), typeof(ICommand), typeof(MaterialDatePicker));

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Internal implementation of the <see cref="DatePicker">DatePicker</see> control.
    /// </summary>
    /// <remarks>
    /// This property can affect the internal behavior of this control. Use only if you fully understand the potential impact.
    /// </remarks>
    public DatePicker InternalDatePicker => _datePicker;

    /// <summary>
    /// Gets or sets the text displayed as the content of the input. This property cannot be changed by the user.
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

#nullable enable
    /// <summary>
    /// Gets or sets the selected date.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="DateTime.Now">DateTime.Now</see>
    /// </default>
    public DateTime? Date
    {
        get => (DateTime?)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }
#nullable disable

    /// <summary>
    /// Gets or sets the Minimum Date.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="DateTime.MinValue">DateTime.MinValue</see>
    /// </default>
    public DateTime MinimumDate
    {
        get => (DateTime)GetValue(MinimumDateProperty);
        set => SetValue(MinimumDateProperty, value);
    }

    /// <summary>
    /// Gets or sets the Maximum Date.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="DateTime.MaxValue">DateTime.MaxValue</see>
    /// </default>
    public DateTime MaximumDate
    {
        get => (DateTime)GetValue(MaximumDateProperty);
        set => SetValue(MaximumDateProperty, value);
    }

    /// <summary>
    /// The format of the date to display to the user.
    /// This is a bindable property.
    /// </summary>
    /// <value>
    /// A valid date format.
    /// </value>
    /// <default>
    /// <see cref="MaterialFormat.DateFormat">MaterialFormat.DateFormat</see>: dd/MM/yyyy
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
    /// Determines whether font of this entry should scale automatically according
    /// to the operating system settings. Default value is true.
    /// This is a bindable property.
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
    /// <default>
    /// <see cref="MaterialFontTracking.BodyLarge">MaterialFontTracking.BodyLarge</see>: 0.5
    /// </default>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }
    
    /// <summary>
    /// Gets or sets an ICommand to be executed when selected date changed
    /// </summary>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public ICommand DateSelectedCommand
    {
        get => (ICommand)GetValue(DateSelectedCommandProperty);
        set => SetValue(DateSelectedCommandProperty, value);
    }

    #endregion Properties

    #region Events

    public event EventHandler<DateSelectedEventArgs> DateSelected;
    
    #endregion Events

    #region Methods

    protected override void SetControlTemplate(MaterialInputType type)
    {
        if (_datePicker == null) return;

#if ANDROID
        var hOffset = 4;
        var vOffset = 2;
        switch (type)
        {
            case MaterialInputType.Filled:
                _datePicker.VerticalOptions = LayoutOptions.Center;
                _datePicker.Margin = new Thickness(hOffset, 0, 0, vOffset);
                break;
            case MaterialInputType.Outlined:
                _datePicker.VerticalOptions = LayoutOptions.Center;
                _datePicker.Margin = new Thickness(hOffset, 0, 0, 0);
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
    
    protected override void ContentFocusChanged(object sender, FocusEventArgs e)
    {
        base.ContentFocusChanged(sender, e);
        
        if (!IsFocused && !_datePicker.CustomDate.HasValue)
        {
            // Set the default date if the user doesn't select anything
            Date = _datePicker.InternalDateTime;
        }
    }
    
    private void OnDateChanged(DateTime? oldValue, DateTime? newValue)
    {
        _datePicker.CustomDate = newValue;
        _datePicker.IsVisible = newValue is not null;
        Text =  newValue?.ToString(Format) ?? string.Empty;
        
        //If we set maximum date, date picker control set as default date.
        if (newValue is not null && newValue.Value == MaximumDate) return;
        if (DateSelectedCommand?.CanExecute(null) ?? false)
        {
            DateSelectedCommand?.Execute(null);
        }                               
        DateSelected?.Invoke(this, new DateSelectedEventArgs(oldValue, newValue));
    }

    /// <summary>
    /// Attempts to set focus to this element.
    /// </summary>
    /// <returns>true if the keyboard focus was set to this element; false if the call to this method did not force a focus change.</returns>
    public new bool Focus()
    {
        if (_datePicker != null && IsEnabled)
        {
#if ANDROID
            var handler = _datePicker.Handler as IDatePickerHandler;
            handler?.PlatformView.PerformClick();
            return true;
#elif IOS || MACCATALYST
            return _datePicker.Focus();
#endif
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
        if (_datePicker != null)
        {
            _datePicker.Unfocus();
        }
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
