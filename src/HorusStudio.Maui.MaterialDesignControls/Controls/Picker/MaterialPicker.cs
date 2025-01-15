using System.Collections;
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A picker <see cref="View" /> let users select an option. 
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialPicker.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialPicker
///     ItemsSource="{Binding ItemsSource}"
///     TrailingIcon="picker_arrow.png"
///     Placeholder="Select an option" /&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var picker = new MaterialPicker
/// {
///     ItemsSource= ItemsSource,
///     TrailingIcon="picker_arrow.png"
///     Placeholder="Select an option"
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/PickerPage.xaml)
/// 
/// </example>
public class MaterialPicker : MaterialInputBase
{
    #region Attributes

    private static readonly double DefaultCharacterSpacing = MaterialFontTracking.BodyLarge;

    #endregion Attributes

    #region Layout

    private readonly CustomPicker _picker;

    #endregion Layout

    #region Constructor

    public MaterialPicker()
    {
        _picker = new CustomPicker
        {
            HorizontalOptions = LayoutOptions.Fill
        };
        _picker.SetBinding(Picker.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        _picker.SetBinding(Picker.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _picker.SetBinding(Picker.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _picker.SetBinding(Picker.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _picker.SetBinding(Picker.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
        _picker.SetBinding(Picker.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _picker.SetBinding(Picker.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _picker.SetBinding(Picker.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _picker.SetBinding(Picker.ItemsSourceProperty, new Binding(nameof(ItemsSource), source: this));
        _picker.SetBinding(Picker.SelectedItemProperty, new Binding(nameof(SelectedItem), source: this));
        _picker.SetBinding(Picker.SelectedIndexProperty, new Binding(nameof(SelectedIndex), source: this));

        TrailingIcon = MaterialIcon.Picker;
        InputTapCommand = new Command(() => _picker.Focus());
        Content = _picker;
    }

    #endregion Constructor

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="VerticalTextAlignment" /> bindable property.
    /// </summary>
    public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(MaterialPicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialPicker), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialPicker), defaultValue: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="ItemsSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(MaterialPicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="SelectedItem" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MaterialPicker), defaultValue: null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindableObject, _, _) => 
    {
        if (bindableObject is MaterialPicker self)
        {
            self.SetText();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="SelectedIndex" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(MaterialPicker), defaultValue: -1);
    
    /// <summary>
    /// The backing store for the <see cref="SelectedIndexChangedCommand" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedIndexChangedCommandProperty = BindableProperty.Create(nameof(SelectedIndexChangedCommand), typeof(ICommand), typeof(MaterialPicker), defaultValue: null);
    
    /// <summary>
    /// The backing store for the <see cref="PropertyPath" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PropertyPathProperty = BindableProperty.Create(nameof(PropertyPath), typeof(string), typeof(MaterialPicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialPicker), defaultValue: null);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the vertical text alignment. This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public TextAlignment VerticalTextAlignment
    {
        get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
        set => SetValue(VerticalTextAlignmentProperty, value);
    }

    /// <summary>
    /// Determines whether or not the font of this entry should scale automatically according
    /// to the operating system settings. This is a bindable property.
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
    ///  <see cref="MaterialFontTracking.BodyLarge"/> 0.5
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
    /// Gets or sets the source list of items to template and display.
    /// <value>To be added.</value>
    /// </summary>
    /// <default>
    /// Null
    /// </default>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected item.
    /// <value>To be added.</value>
    /// </summary>
    /// <default>
    /// Null
    /// </default>
    /// <remarks>
    /// To be added.
    /// </remarks>
    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    
    /// <summary>
    /// Gets the index of the selected item of the picker. This is a bindable
    /// property.
    /// <value>An 0-based index representing the selected item in the list. Default is -1.</value>
    /// </summary>
    /// <remarks>
    ///  A value of -1 represents no item selected.
    /// </remarks>
    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }
    
    /// <summary>
    /// Gets or sets an ICommand to be executed when selected index has changed. This is a bindable
    /// property.
    /// </summary>
    public ICommand SelectedIndexChangedCommand
    {
        get => (ICommand)GetValue(SelectedIndexChangedCommandProperty);
        set => SetValue(SelectedIndexChangedCommandProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the property path.
    /// This property is used to map an object and display a property of it.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    /// <remarks>
    /// If it´s no defined, the control will use toString() method.
    /// </remarks>
    public string PropertyPath
    {
        get => (string)GetValue(PropertyPathProperty);
        set => SetValue(PropertyPathProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the text This property cannot be changed by the user.
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

    #endregion Properties

    #region Events

    public event EventHandler SelectedIndexChanged;
    
    #endregion Events

    #region Methods

    protected override void SetControlTemplate(MaterialInputType type)
    {
        if (_picker == null) return;

#if ANDROID
        var offset = 3;
        switch (type)
        {
            case MaterialInputType.Filled:
                _picker.VerticalOptions = LayoutOptions.Center;
                _picker.Margin = new Thickness(offset, 0, 0, offset);
                break;
            case MaterialInputType.Outlined:
                _picker.VerticalOptions = LayoutOptions.Center;
                _picker.Margin = new Thickness(offset, 0, 0, 0);
                break;
        }
#endif
    }

    protected override void SetControlIsEnabled()
    {
        if (_picker != null)
            _picker.IsEnabled = IsEnabled;
    }

    protected override void OnControlAppearing()
    {
        // Setup events/animations
        _picker.Focused += ContentFocusChanged;
        _picker.Unfocused += ContentFocusChanged;
        _picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
    }

    protected override void OnControlDisappearing()
    {
        // Cleanup events/animations
        _picker.Focused -= ContentFocusChanged;
        _picker.Unfocused -= ContentFocusChanged;
        _picker.SelectedIndexChanged -= Picker_SelectedIndexChanged;
    }
    
    private void SetText()
    {
        if (SelectedItem != null)
        {
            Text = string.IsNullOrWhiteSpace(PropertyPath) ? 
                SelectedItem?.ToString() : 
                SelectedItem.GetPropertyValue<string>(PropertyPath);
        }
        else
        {
            Text = string.Empty;
        }
    }
    
    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SelectedIndexChangedCommand?.CanExecute(null) ?? false)
        {
            SelectedIndexChangedCommand?.Execute(null);
        }
        SelectedIndexChanged?.Invoke(this, e);
    }
    
    #endregion Methods

    #region Styles
    internal static IEnumerable<Style> GetStyles()
    {
        var style = new Style(typeof(MaterialPicker)) { ApplyToDerivedTypes = true };

        var baseStyles = MaterialInputBase.GetBaseStyles();

        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, baseStyles);

        return new List<Style> { style };
    }

    #endregion Styles
}
