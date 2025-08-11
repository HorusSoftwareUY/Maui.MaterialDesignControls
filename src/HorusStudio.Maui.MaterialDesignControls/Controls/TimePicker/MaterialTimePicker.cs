using System.Windows.Input;
#if ANDROID
using Microsoft.Maui.Handlers;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Time pickers let users select a time. They typically appear in forms and dialogs and, partially, follow Material Design Guidelines. <see href="https://m3.material.io/components/time-pickers/overview">See more.</see>
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialTimePicker.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialTimePicker
///    Placeholder="Creation time" /&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var timePicker = new MaterialTimePicker
/// {
///     Placeholder="Creation time"
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/TimePickerPage.xaml)
/// 
/// </example>
/// <todoList>
/// * [iOS] Font attributes doesn't work
/// * [iOS] Horizontal text alignment doesn't work when there is a date selected
/// * [Android] Use the colors defined in Material in the time picker dialog
/// </todoList>
public class MaterialTimePicker : MaterialInputBase
{
    #region Attributes

    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCharacterSpacing = _ => MaterialFontTracking.BodyLarge;

    #endregion Attributes

    #region Layout

    private readonly CustomTimePicker _timePicker;

    #endregion Layout

    #region Constructor

    public MaterialTimePicker()
    {
        _timePicker = new CustomTimePicker
        {
            HorizontalOptions = LayoutOptions.Fill,
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
        
        InputTapCommand = new Command(() => Focus());
        LeadingIconCommand = new Command(() => Focus());
        TrailingIcon = MaterialIcon.TimePicker;
        TrailingIconCommand = new Command(() => Focus());
        Content = _timePicker;
    }

    #endregion Constructor

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Text">Text</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialTimePicker), defaultValue: null);

#nullable enable
    /// <summary>
    /// The backing store for the <see cref="Time">Time</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan?), typeof(MaterialTimePicker), defaultBindingMode: BindingMode.TwoWay, defaultValue: null, propertyChanged:
    (bindable, oldValue, newValue) =>
    {
        var self = (MaterialTimePicker)bindable;
        self.OnTimeChanged(oldValue as TimeSpan?, newValue as TimeSpan?);
    });
#nullable disable

    /// <summary>
    /// The backing store for the <see cref="Format">Format</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(nameof(Format), typeof(string), typeof(MaterialTimePicker), defaultValue: MaterialFormat.TimeFormat);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled">FontAutoScalingEnabled</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialTimePicker), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing">CharacterSpacing</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialTimePicker), defaultValueCreator: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="TimeSelectedCommand">TimeSelectedCommand<see/> bindable property.
    /// </summary>
    public static readonly BindableProperty TimeSelectedCommandProperty = BindableProperty.Create(nameof(TimeSelectedCommand), typeof(ICommand), typeof(MaterialTimePicker));

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Internal implementation of the <see cref="TimePicker">TimePicker</see> control.
    /// </summary>
    /// <remarks>
    /// This property can affect the internal behavior of this control. Use only if you fully understand the potential impact.
    /// </remarks>
    public TimePicker InternalTimePicker => _timePicker;

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
    /// Gets or sets the displayed time. This is a bindable property.
    /// <value>
    /// The <see cref="System.TimeSpan">TimeSpan</see> displayed in the TimePicker.
    /// </value>
    /// </summary>
    /// <default>
    /// null
    /// </default>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public TimeSpan? Time
    {
        get => (TimeSpan?)GetValue(TimeProperty);
        set => SetValue(TimeProperty, value);
    }
#nullable disable

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
    /// Determines whether font of this entry should scale automatically according
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
    /// Gets or sets an ICommand to be executed when selected time changed
    /// </summary>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public ICommand TimeSelectedCommand
    {
        get => (ICommand)GetValue(TimeSelectedCommandProperty);
        set => SetValue(TimeSelectedCommandProperty, value);
    }
    
    #endregion Properties

    #region Events

    public event EventHandler<TimeSelectedEventArgs> TimeSelected;
    
    #endregion Events

    #region Methods

    protected override void SetControlTemplate(MaterialInputType type)
    {
        if (_timePicker == null) return;

#if ANDROID
        var hOffset = 4;
        var vOffset = 2;
        switch (type)
        {
            case MaterialInputType.Filled:
                _timePicker.VerticalOptions = LayoutOptions.Center;
                _timePicker.Margin = new Thickness(hOffset, 0, 0, vOffset);
                break;
            case MaterialInputType.Outlined:
                _timePicker.VerticalOptions = LayoutOptions.Center;
                _timePicker.Margin = new Thickness(hOffset, 0, 0, 0);
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
    
    protected override void ContentFocusChanged(object sender, FocusEventArgs e)
    {
        base.ContentFocusChanged(sender, e);
        
        if (!IsFocused && !_timePicker.CustomTime.HasValue)
        {
            // Set the default date if the user doesn't select anything
            Time = _timePicker.InternalTime;
        }
    }
    
    private void OnTimeChanged(TimeSpan? oldValue, TimeSpan? newValue)
    {
        _timePicker.CustomTime = newValue;
        _timePicker.IsVisible = newValue is not null;
        Text =  newValue?.ToString(Format) ?? string.Empty;
        _timePicker.SetBinding(TimePicker.FormatProperty, new Binding(nameof(Format), source: this));
        if (TimeSelectedCommand?.CanExecute(null) ?? false)
        {
            TimeSelectedCommand?.Execute(null);
        }                               
        TimeSelected?.Invoke(this, new TimeSelectedEventArgs(oldValue, newValue));
    }

    /// <summary>
    /// Attempts to set focus to this element.
    /// </summary>
    /// <returns>true if the keyboard focus was set to this element; false if the call to this method did not force a focus change.</returns>
    public new bool Focus()
    {
        if (_timePicker != null && IsEnabled)
        {
#if ANDROID
            var handler = _timePicker.Handler as ITimePickerHandler;
            handler?.PlatformView.PerformClick();
            return true;
#elif IOS || MACCATALYST
            return _timePicker.Focus();
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
        if (_timePicker != null)
        {
            _timePicker.Unfocus();
        }
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
