using System.Collections;
using System.Collections.Specialized;

namespace HorusStudio.Maui.MaterialDesignControls;

public class MaterialPicker : MaterialInputBase
{
    #region Attributes

    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialLightTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultCharacterSpacing = MaterialFontTracking.BodyLarge;

    #endregion Attributes

    #region Layout

    private CustomPicker _picker;

    #endregion Layout

    #region Constructor

    public MaterialPicker()
    {
        ShowPlaceholder = false;

        _picker = new CustomPicker
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
        };

        _picker.SetBinding(Picker.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        _picker.SetBinding(Picker.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _picker.SetBinding(Picker.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _picker.SetBinding(Picker.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _picker.SetBinding(Picker.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
        _picker.SetBinding(Picker.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _picker.SetBinding(Picker.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _picker.SetBinding(Picker.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _picker.SetBinding(Picker.TitleProperty, new Binding(nameof(Placeholder), source: this));
        _picker.SetBinding(Picker.TitleColorProperty, new Binding(nameof(PlaceholderColor), source: this));

        _picker.SelectedIndexChanged += Picker_SelectedIndexChanged;

        Content = _picker;
        Text = String.Empty;
    }

    #endregion Constructor

    #region BindableProperties

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
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(MaterialPicker), defaultValue: null, propertyChanged: (bindableObject, _, newValue) => 
    { 
        if (bindableObject is MaterialPicker self)
        {
            self.OnItemsSourceChanged(self, newValue);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="SelectedItem" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MaterialPicker), defaultValue: null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindableObject, _, newValue) => 
    { 
        if (bindableObject is MaterialPicker self)
        {
            self.OnSelectedItemChanged(self, newValue);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="PickerRowHeight" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PickerRowHeightProperty = BindableProperty.Create(nameof(PickerRowHeight), typeof(int), typeof(MaterialPicker), defaultValue: 50);

    /// <summary>
    /// The backing store for the <see cref="SelectedIndex" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(MaterialPicker), defaultValue: -1);

    /// <summary>
    /// The backing store for the <see cref="PropertyPath" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PropertyPathProperty = BindableProperty.Create(nameof(PropertyPath), typeof(string), typeof(MaterialPicker), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialPicker), defaultValue: null);

    #endregion BindableProperties

    #region Properties

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
    /// <remarks>
    /// To be added.
    /// </remarks>
    public object SelectedItem
    {
        get => (object)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    /// <summary>
    /// Gets or sets the picker row height
    /// </summary>
    public int PickerRowHeight
    {
        get => (int)GetValue(PickerRowHeightProperty);
        set => SetValue(PickerRowHeightProperty, value);
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
        get
        {
            if (this.ItemsSource != null)
            {
                var index = 0;
                foreach (var item in this.ItemsSource)
                {
                    if (index.Equals(this._picker.SelectedIndex))
                    {
                        return index;
                    }
                    index++;
                }
            }

            return -1;
        }
    }

    /// <summary>
    /// Gets or sets the property path.
    /// This property is used to map an object and display a property of it.
    /// </summary>
    /// <remarks>
    /// If it´s no defined, the control will use toString() method.
    /// </remarks>
    public string PropertyPath
    {
        get => (string)GetValue(PropertyPathProperty);
        set => SetValue(PropertyPathProperty, value);
    }

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
            return SelectedIndex == -1 ? null : SelectedIndex.ToString();
        }
        set => SetValue(TextProperty, SelectedIndex == -1 ? null : SelectedIndex.ToString());
    }

    #endregion Properties

    #region Events

    public event EventHandler SelectedIndexChanged;

    public new event EventHandler<FocusEventArgs> Focused;

    public new event EventHandler<FocusEventArgs> Unfocused;

    #endregion Events

    #region Methods

    protected override void SetControlTemplate(MaterialInputType type)
    {
        if (_picker == null) return;

#if ANDROID
        switch (type)
        {
            case MaterialInputType.Filled:
                _picker.VerticalOptions = LayoutOptions.Center;
                break;
            case MaterialInputType.Outlined:
                _picker.VerticalOptions = LayoutOptions.Center;
                break;
        }
#endif
    }

    private void OnItemsSourceChanged(MaterialPicker control, object newValue)
    {
        control._picker.Items.Clear();
        if (!Equals(newValue, null) && newValue is IEnumerable)
        {
            foreach (var item in (IEnumerable)newValue)
            {
                var newItem = string.IsNullOrWhiteSpace(control.PropertyPath) ? item.ToString() : GetPropertyValue(item, control.PropertyPath);
                control._picker.Items.Add(newItem);
            }
        }

        if (newValue is INotifyCollectionChanged collection)
        {
            collection.CollectionChanged -= Picker_ItemsSourceChanged;
            collection.CollectionChanged += Picker_ItemsSourceChanged;
        }

        control.InternalUpdateSelectedIndex();
    }

    private void InternalUpdateSelectedIndex()
    {
        var selectedIndex = -1;
        if (this.ItemsSource != null)
        {
            var index = 0;
            foreach (var item in this.ItemsSource)
            {
                if (item != null && this.SelectedItem != null && string.IsNullOrWhiteSpace(this.PropertyPath) && item.ToString().Equals(this.SelectedItem.ToString()))
                {
                    selectedIndex = index;
                    break;
                }
                else if (item != null && this.SelectedItem != null && !string.IsNullOrWhiteSpace(this.PropertyPath))
                {
                    var itemValue = GetPropertyValue(item, this.PropertyPath);
                    var selectedItemValue = GetPropertyValue(this.SelectedItem, this.PropertyPath);
                    if (itemValue.Equals(selectedItemValue))
                    {
                        selectedIndex = index;
                        break;
                    }
                }
                index++;
            }
        }
        this._picker.SelectedIndex = selectedIndex;
    }


    private void OnSelectedItemChanged(MaterialPicker self, object newValue)
    {
        if (newValue is not null)
        {
            var newItem = string.IsNullOrWhiteSpace(self.PropertyPath) ? newValue.ToString() : GetPropertyValue(newValue, self.PropertyPath);
            self._picker.SelectedItem = newItem;
            self.InternalUpdateSelectedIndex();
        }
        else
        {
            self._picker.SelectedItem = null;
            self._picker.SelectedIndex = -1;
        }
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
    }

    protected override void OnControlDisappearing()
    {
        // Cleanup events/animations
        _picker.Focused -= ContentFocusChanged;
        _picker.Unfocused -= ContentFocusChanged;
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

    private string GetPropertyValue(object item, string propertyToSearch)
    {
        var properties = item.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (property.Name.Equals(propertyToSearch, StringComparison.InvariantCultureIgnoreCase))
            {
                return property.GetValue(item, null).ToString();
            }
        }
        return item.ToString();
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ItemsSource != null)
        {
            var index = 0;
            foreach (var item in this.ItemsSource)
            {
                if (index.Equals(this._picker.SelectedIndex))
                {
                    this.SelectedItem = item;
                    this.SelectedIndexChanged?.Invoke(this, e);
                    break;
                }
                index++;
            }
        }
        this.Text = String.Empty;
        this._picker.Unfocus();
    }

    private void Picker_ItemsSourceChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (var item in e.NewItems)
            {
                var newItem = string.IsNullOrWhiteSpace(PropertyPath) ? item.ToString() : GetPropertyValue(item, PropertyPath);
                _picker.Items.Add(newItem);
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            if (_picker.Items.Count > 0)
            {
                _picker.Items.RemoveAt(e.OldStartingIndex);
            }
        }

        InternalUpdateSelectedIndex();
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
