using System.Collections;
using System.Windows.Input;
using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Pickers let users select an option. They typically appear in forms and dialogs. 
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
/// <todoList>
/// * [Android] Use the colors defined in Material in the picker dialog
/// </todoList>
public class MaterialPicker : MaterialInputBase
{
    #region Attributes

    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCharacterSpacing = _ => MaterialFontTracking.BodyLarge;

    #endregion Attributes

    #region Layout

    private readonly CustomPicker? _picker;

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

        InputTapCommand = new Command(() => Focus());
        LeadingIconCommand = new Command(() => Focus());
        TrailingIcon = MaterialIcon.Picker;
        TrailingIconCommand = new Command(() => Focus());
        Content = _picker;
    }

    #endregion Constructor

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="VerticalTextAlignment">VerticalTextAlignment</see> bindable property.
    /// </summary>
    public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(MaterialPicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled">FontAutoScalingEnabled</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialPicker), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing">CharacterSpacing</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialPicker), defaultValueCreator: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="ItemsSource">ItemsSource</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(MaterialPicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="SelectedItem">SelectedItem</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MaterialPicker), defaultValue: null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindableObject, _, _) => 
    {
        if (bindableObject is MaterialPicker self)
        {
            self.SetText();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="SelectedIndex">SelectedIndex</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(MaterialPicker), defaultValue: -1);
    
    /// <summary>
    /// The backing store for the <see cref="SelectedIndexChangedCommand">SelectedIndexChangedCommand</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedIndexChangedCommandProperty = BindableProperty.Create(nameof(SelectedIndexChangedCommand), typeof(ICommand), typeof(MaterialPicker), defaultValue: null);
    
    /// <summary>
    /// The backing store for the <see cref="ItemDisplayPath">ItemDisplayPath</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemDisplayPathProperty = BindableProperty.Create(nameof(ItemDisplayPath), typeof(string), typeof(MaterialPicker), defaultValue: null, propertyChanged:
        (bindableObject, _, newValue) =>
        {
            if (bindableObject is MaterialPicker self && self._picker != null)
            {
                self._picker.ItemDisplayBinding = new Binding(newValue as string);
            }
        });
    
    /// <summary>
    /// The backing store for the <see cref="Text">Text</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialPicker), defaultValue: null);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Internal implementation of the <see cref="Picker">Picker</see> control.
    /// </summary>
    /// <remarks>
    /// This property can affect the internal behavior of this control. Use only if you fully understand the potential impact.
    /// </remarks>
    public Picker? InternalPicker => _picker;

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
    ///  <see cref="MaterialFontTracking.BodyLarge">MaterialFontTracking.BodyLarge</see>
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
    public object? SelectedItem
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
    public string ItemDisplayPath
    {
        get => (string)GetValue(ItemDisplayPathProperty);
        set => SetValue(ItemDisplayPathProperty, value);
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

    public event EventHandler? SelectedIndexChanged;
    
    #endregion Events

    #region Methods

    protected override void SetControlTemplate(MaterialInputType type)
    {
        if (_picker == null)
        {
            return;
        }

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
        {
            _picker.IsEnabled = IsEnabled;
        }
    }

    protected override void OnControlAppearing()
    {
        if (_picker == null)
        {
            return;
        }
        
        // Setup events/animations
        _picker.Focused += ContentFocusChanged;
        _picker.Unfocused += ContentFocusChanged;
        _picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
    }

    protected override void OnControlDisappearing()
    {
        if (_picker == null)
        {
            return;
        }
        
        // Cleanup events/animations
        _picker.Focused -= ContentFocusChanged;
        _picker.Unfocused -= ContentFocusChanged;
        _picker.SelectedIndexChanged -= Picker_SelectedIndexChanged;
    }
    
    private void SetText()
    {
        if (SelectedItem != null)
        {
            Text = (string.IsNullOrWhiteSpace(ItemDisplayPath) ? 
                SelectedItem.ToString() : 
                SelectedItem.GetPropertyValue<string>(ItemDisplayPath)) ?? string.Empty;
        }
        else
        {
            Text = string.Empty;

#if IOS || MACCATALYST
            if (_picker != null && _picker.Handler is MaterialPickerHandler handler)
            {
                handler.ClearText();
            }
#endif
        }
    }
    
    private void Picker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (SelectedIndexChangedCommand?.CanExecute(null) ?? false)
        {
            SelectedIndexChangedCommand?.Execute(null);
        }
        SelectedIndexChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Attempts to set focus to this element.
    /// </summary>
    /// <returns>true if the keyboard focus was set to this element; false if the call to this method did not force a focus change.</returns>
    public new bool Focus()
    {
        if (_picker != null && IsEnabled)
        {
            return _picker.Focus();
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
        if (_picker != null)
        {
            _picker.Unfocus();
        }
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
