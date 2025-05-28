using System.Globalization;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Define <see cref="MaterialSegmentedButtons" /> types
/// </summary>
public enum MaterialSegmentedButtonsType
{
    /// <summary>Outlined segmented buttons</summary>
    Outlined,
    /// <summary>Filled segmented buttons</summary>
    Filled
}

/// <summary>
/// Segmented buttons <see cref="View" /> help people select options, switch views, or sort elements, and follows Material Design Guidelines <see href="https://m3.material.io/components/segmented-buttons/overview" />.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSegmentedButtons.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialSegmentedButtons 
///     ItemsSource="{Binding Items}"
///     SelectionCommand="{Binding OnItemSelectedCommand}"
///     Type="Outlined"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var segmentedButtons = new MaterialSegmentedButtons
/// {
///     SelectionCommand = OnItemSelectedCommand,
///     ItemsSource = Items,
///     Type = MaterialSegmentedButtonsType.Outlined
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SegmentedButtonsPage.xaml)
/// 
/// </example>
/// <todoList>
/// * [iOS] FontAttributes doesn't work (MAUI issue)
/// </todoList>
public class MaterialSegmentedButtons : ContentView
{   
    #region Attributes

    private readonly static MaterialSegmentedButtonsType DefautlSegmentedType = MaterialSegmentedButtonsType.Outlined;
    private readonly static IEnumerable<MaterialSegmentedButtonsItem>? DefaultItemsSource = null;
    private readonly static MaterialSegmentedButtonsItem? DefaultSelectedItem = null;
    private readonly static bool DefaultAllowMultiSelect = false;
    private readonly static bool DefaultIsEnabled = true;
    private readonly static Thickness DefaultPadding = new(12, 0);
    private readonly static CornerRadius DefaultCornerRadius = new(16);
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static double DefaultFontSize = MaterialFontSize.LabelLarge;
    private readonly static Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultBorderWidth = 1;
    private readonly static Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Surface, Dark = MaterialDarkTheme.Surface }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultSelectedBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SecondaryContainer, Dark = MaterialDarkTheme.SecondaryContainer }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultSelectedTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultIconSize = 18;
    private readonly static AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
    private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
    
    #endregion
    
    #region Bindable Properties
    
    /// <summary>
    /// The backing store for the <see cref="MaterialSegmentedButtonsType" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialSegmentedButtonsType), typeof(MaterialSegmentedButtons), defaultValue: DefautlSegmentedType, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButtons self)
        {
            self.UpdateType();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="ItemsSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<MaterialSegmentedButtonsItem>), typeof(MaterialSegmentedButtons), defaultValue: DefaultItemsSource, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButtons self)
        {
            self.UpdateItemsSource();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="SelectedItem" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(MaterialSegmentedButtonsItem), typeof(MaterialSegmentedButtons), defaultValue: DefaultSelectedItem, BindingMode.TwoWay);
    
    /// <summary>
    /// The backing store for the <see cref="SelectedItems" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(IEnumerable<MaterialSegmentedButtonsItem>), typeof(MaterialSegmentedButtons), defaultValue: DefaultSelectedItem, BindingMode.OneWayToSource);
    
    /// <summary>
    /// The backing store for the <see cref="AllowMultiSelect" /> bindable property.
    /// </summary>
    public static readonly BindableProperty AllowMultiSelectProperty = BindableProperty.Create(nameof(AllowMultiSelect), typeof(bool), typeof(MaterialSegmentedButtons), defaultValue: DefaultAllowMultiSelect, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButtons self)
        {
            self.UpdateItemsSource();
        }
    });

    /// <summary>
    /// Gets or sets the state when the Segmented is enabled.
    /// bindable property.
    /// </summary>
    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialSegmentedButtons), defaultValue: DefaultIsEnabled);

    /// <summary>
    /// The backing store for the <see cref="Padding" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialSegmentedButtons), defaultValue: DefaultPadding);
    
    /// <summary>
    /// The backing store for the <see cref="CornerRadius" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialSegmentedButtons), defaultValue: DefaultCornerRadius);
    
    /// <summary>
    /// The backing store for the <see cref="FontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialSegmentedButtons), defaultValue: DefaultFontFamily);
    
    /// <summary>
    /// The backing store for the <see cref="FontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialSegmentedButtons), defaultValue: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialSegmentedButtons), Button.CharacterSpacingProperty.DefaultValue);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialSegmentedButtons), defaultValue: Button.FontAttributesProperty.DefaultValue);

    /// <summary>
    /// The backing store for the <see cref="TextTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(MaterialSegmentedButtons), defaultValue: Button.TextTransformProperty.DefaultValue);

    /// <summary>
    /// The backing store for the <see cref="TextDecorations" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextDecorationsProperty = BindableProperty.Create(nameof(TextDecorations), typeof(TextDecorations), typeof(MaterialSegmentedButtons), defaultValue: TextDecorations.None);

    /// <summary>
    /// The backing store for the <see cref="BorderColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialSegmentedButtons), defaultValue: DefaultBorderColor);

    /// <summary>
    /// The backing store for the <see cref="BorderWidth"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialSegmentedButtons), defaultValue: DefaultBorderWidth);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialSegmentedButtons), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButtons self)
        {
            self.SetBackgroundColor();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="SelectedBackgroundColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(MaterialSegmentedButtons), defaultValue: DefaultSelectedBackgroundColor);

    /// <summary>
    /// The backing store for the <see cref="TextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialSegmentedButtons), defaultValue: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="SelectedTextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(MaterialSegmentedButtons), defaultValue: DefaultSelectedTextColor);

    /// <summary>
    /// The backing store for the <see cref="IconSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(MaterialSegmentedButtons), defaultValue: DefaultIconSize);

    /// <summary>
    /// The backing store for the <see cref="Command" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectionCommandProperty = BindableProperty.Create(nameof(SelectionCommand), typeof(ICommand), typeof(MaterialSegmentedButtons));
    
    /// <summary>
    /// The backing store for the <see cref="Animation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialSegmentedButtons), defaultValue: DefaultAnimationType);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialSegmentedButtons), defaultValue: DefaultAnimationParameter);
    
    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialSegmentedButtons));

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the segmented buttons type according to <see cref="MaterialSegmentedButtonsType"/> enum.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialButtonType.Outlined"/>
    /// </default>
    public MaterialSegmentedButtonsType Type
    {
        get => (MaterialSegmentedButtonsType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets items source mapped to segmented buttons.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Null"/>
    /// </default>
    public IEnumerable<MaterialSegmentedButtonsItem> ItemsSource
    {
        get => (IEnumerable<MaterialSegmentedButtonsItem>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets the selected buttons.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Array.Empty"/>
    /// </default>
    /// <remarks>Useful when you set <see cref="AllowMultiSelect"/> to <see langword="True"/></remarks>
    public IEnumerable<MaterialSegmentedButtonsItem> SelectedItems
    {
        get { return ItemsSource != null ? ItemsSource.Where(w => w.IsSelected) : Array.Empty<MaterialSegmentedButtonsItem>(); }
    }

    /// <summary>
    /// Gets the selected button.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Null"/>
    /// </default>
    /// <remarks>Useful when you set <see cref="AllowMultiSelect"/> to <see langword="False"/></remarks>
    public MaterialSegmentedButtonsItem? SelectedItem
    {
        get { return ItemsSource != null ? ItemsSource.FirstOrDefault(w => w.IsSelected) : null; }
    }

    /// <summary>
    /// Gets or sets the if the control allows multiple selection.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="False"/>
    /// </default>
    public bool AllowMultiSelect
    {
        get => (bool)GetValue(AllowMultiSelectProperty);
        set => SetValue(AllowMultiSelectProperty, value);
    }

    /// <summary>
    /// Gets or sets the state when the Segmented is enabled.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="True"/>
    /// </default>
    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets the corner radius for the control.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// CornerRadius(16)
    /// </default>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the font family for segmented button text.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the font size for segmented button text.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.LabelLarge">MaterialFontSize.LabelLarge</see> / Tablet: 17 - Phone: 14
    /// </default>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between each of the characters of <see cref="Text"/> when displayed on the segmented buttons.
    /// This is a bindable property.
    /// </summary>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the font for the text of segmented buttons is bold, italic, or neither.
    /// This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    /// <summary>
    /// Applies text transformation to the <see cref="Text"/> displayed on segmented buttons.
    /// This is a bindable property.
    /// </summary>
    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value);
    }

    /// <summary>
    /// Gets or sets <see cref="TextDecorations" /> for the text of segmented buttons.
    /// This is a bindable property.
    /// </summary>
    public TextDecorations TextDecorations
    {
        get => (TextDecorations)GetValue(TextDecorationsProperty);
        set => SetValue(TextDecorationsProperty, value);
    }

    /// <summary>
    /// Gets or sets the border color for the control.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Outline">MaterialLightTheme.Outline</see> - Dark = <see cref="MaterialDarkTheme.Outline">MaterialDarkTheme.Outline</see>
    /// </default>
    /// <remarks>
    /// <para>This property has no effect if <see cref="IBorderElement.BorderWidth">IBorderElement.BorderWidth</see> is set to 0.</para>
    /// <para>On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor" />is set to a non-default color.</para>
    /// </remarks>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the border, in device-independent units.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 1
    /// </default>
    /// <remarks>This property has no effect if <see cref="MaterialSegmentedButtons.Type"> is set to <see cref="MaterialSegmentedButtonsType.Filled">.</remarks>
    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the Background color for the control.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Surface">MaterialLightTheme.Surface</see> - Dark = <see cref="MaterialDarkTheme.Surface">MaterialDarkTheme.Surface</see>
    /// </default>
    /// <remarks>This property has no effect if <see cref="MaterialSegmentedButtons.Type"> is set to <see cref="MaterialSegmentedButtonsType.Outlined">.</remarks>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the Background color for selected segmented button items.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.SecondaryContainer">MaterialLightTheme.SecondaryContainer</see> - Dark = <see cref="MaterialDarkTheme.SecondaryContainer">MaterialDarkTheme.SecondaryContainer</see>
    /// </default>
    public Color SelectedBackgroundColor
    {
        get => (Color)GetValue(SelectedBackgroundColorProperty);
        set => SetValue(SelectedBackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the text color for segmented buttons.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurface">MaterialLightTheme.OnSurface</see> - Dark = <see cref="MaterialDarkTheme.OnSurface">MaterialDarkTheme.OnSurface</see>
    /// </default>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the text color for selected segmented buttons.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurface">MaterialLightTheme.OnSurface</see> - Dark = <see cref="MaterialDarkTheme.OnSurface">MaterialDarkTheme.OnSurface</see>
    /// </default>
    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon size for segmented button.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 18
    /// </default>
    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the selection of one of the segmented buttons changes.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    /// <remarks>
    /// The command parameter type varies based on <see cref="AllowMultiSelect"/>: when <see langword="True"/>, it is <see cref="IEnumerable<MaterialSegmentedButton>"/>; when <see langword="False"/>, it is <see cref="MaterialSegmentedButtonsItem"/>.
    /// </remarks>
    public ICommand SelectionCommand
    {
        get => (ICommand)GetValue(SelectionCommandProperty);
        set => SetValue(SelectionCommandProperty, value);
    }
    
    /// <summary>
    /// Gets or sets an animation to be executed when is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="AnimationTypes.Fade"> AnimationTypes.Fade </see>
    /// </default>
    public AnimationTypes Animation
    {
        get => (AnimationTypes)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Animation"/> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public double? AnimationParameter
    {
        get => (double?)GetValue(AnimationParameterProperty);
        set => SetValue(AnimationParameterProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom animation to be executed when is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public ICustomAnimation CustomAnimation
    {
        get => (ICustomAnimation)GetValue(CustomAnimationProperty);
        set => SetValue(CustomAnimationProperty, value);
    }

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the selection of one of the segmented buttons changes.
    /// </summary>
    public event EventHandler<SegmentedButtonsSelectedEventArgs>? SelectionChanged;

    private void OnSegmentedButtonTap(MaterialSegmentedButtonsItem segmentedButton)
    {
        if (!IsEnabled)
        {
            return;
        }

        if (!AllowMultiSelect)
        {
            foreach (var itemCollection in ItemsSource)
            {
                itemCollection.IsSelected = false;
            }
        }

        var Items = ItemsSource.ToList();
        Items[Items.IndexOf(segmentedButton)].IsSelected = !segmentedButton.IsSelected;
        ItemsSource = Items;

        object commandParameter = AllowMultiSelect ? SelectedItems : SelectedItem;
        if (SelectionCommand != null && SelectionCommand.CanExecute(commandParameter))
        {
            SelectionCommand.Execute(commandParameter);
        }

        if (AllowMultiSelect)
        {
            SelectionChanged?.Invoke(this, new SegmentedButtonsSelectedEventArgs(SelectedItems));
        }
        else
        {
            SelectionChanged?.Invoke(this, new SegmentedButtonsSelectedEventArgs(SelectedItem));
        }
    }

    #endregion
    
    #region Layout

    private MaterialCard _container;
    private Grid _itemsContainer;

    #endregion

    #region Constructor

    public MaterialSegmentedButtons()
    {
        _itemsContainer = new Grid
        {
            ColumnSpacing = 0,
            Padding = 0,
        };
        
        _container = new MaterialCard
        {
            Type = MaterialCardType.Filled,
            BorderWidth = 0,
            BorderColor = Colors.Transparent,
            BackgroundColor = Colors.Transparent,
            HeightRequest = 40,
            MinimumHeightRequest = 40,
            Padding = 0,
            Content = _itemsContainer
        };

        _container.SetBinding(MaterialCard.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));

        UpdateType();
        
        Content = _container;
    }

    #endregion

    #region Setters

    private void UpdateType()
    {
        SetBackgroundColor();
        UpdateItemsSource();
    }

    private void UpdateItemsSource()
    {
        _itemsContainer.Children.Clear();
        _itemsContainer.ColumnDefinitions = new ColumnDefinitionCollection();
        _itemsContainer.ColumnSpacing = Type == MaterialSegmentedButtonsType.Outlined ? -2 : 0;

        if (ItemsSource == null)
        {
            return;
        }

        var indexColum = 0;
        foreach (var item in ItemsSource)
        {
            var itemView = CreateItemView(indexColum, item);
            _itemsContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(ItemsSource.Count() / 100.0, GridUnitType.Star) });
            _itemsContainer.Add(itemView, indexColum);
            indexColum++;
        }
    }

    private View CreateItemView(int index, MaterialSegmentedButtonsItem item)
    {
        var card = new MaterialCard
        {
            Type = MaterialCardType.Filled,
            ShadowColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            Margin = new Thickness(0),
            Padding = new Thickness(12, 0),
            BorderWidth = 0,
            BorderColor = Colors.Transparent,
            Command = new Command(() => OnSegmentedButtonTap(item), () => IsEnabled)
        };

        card.SetBinding(MaterialCard.BackgroundColorProperty, new Binding(nameof(item.IsSelected), source: item, converter: new ItemSelectedToBackgroundColorConverter(this)));

        card.SetBinding(MaterialCard.AnimationProperty, new Binding(nameof(Animation), source: this));
        card.SetBinding(MaterialCard.AnimationParameterProperty, new Binding(nameof(AnimationParameter), source: this));
        card.SetBinding(MaterialCard.CustomAnimationProperty, new Binding(nameof(CustomAnimation), source: this));

        if (Type == MaterialSegmentedButtonsType.Outlined)
        {
            card.Type = MaterialCardType.Custom;
            card.SetBinding(MaterialCard.BorderColorProperty, new Binding(nameof(BorderColor), source: this));
            card.SetBinding(MaterialCard.BorderWidthProperty, new Binding(nameof(BorderWidth), source: this));
            if (ItemsSource.Count() > 1)
            {
                if (index == 0)
                {
                    card.CornerRadius = new CornerRadius(CornerRadius.TopLeft, 0, CornerRadius.BottomLeft, 0);
                }
                else if (index == ItemsSource.Count() - 1)
                {
                    card.CornerRadius = new CornerRadius(0, CornerRadius.TopRight, 0, CornerRadius.BottomRight);
                }
                else
                {
                    card.CornerRadius = 0;
                    card.Margin = new Thickness(-1, 0);
                }
            }
            else
            {
                card.CornerRadius = CornerRadius;
            }
        }
        else
        {
            card.SetBinding(MaterialCard.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
        }

        SetItemContent(card, item);

        return card;
    }

    private void SetItemContent(MaterialCard card, MaterialSegmentedButtonsItem item)
    {
        var label = new MaterialLabel
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(item.IsSelected), source: item, converter: new ItemSelectedToTextColorConverter(this)));
        label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(item.Text), source: item));

        label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(TextTransform), source: this));
        label.SetBinding(MaterialLabel.TextDecorationsProperty, new Binding(nameof(TextDecorations), source: this));

        if ((item.IsSelected && item.SelectedIcon != null) || (!item.IsSelected && item.UnselectedIcon != null))
        {
            var container = new Grid
            {
                ColumnSpacing = 8,
                HorizontalOptions = LayoutOptions.Center
            };
            var iconColumnDefinition = new ColumnDefinition();
            iconColumnDefinition.SetBinding(ColumnDefinition.WidthProperty, new Binding(nameof(IconSize), source: this));
            container.ColumnDefinitions.Add(iconColumnDefinition);
            container.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            var icon = new Image
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = true
            };

            icon.SetBinding(Image.SourceProperty, new Binding(nameof(item.IsSelected), source: item, converter: new ItemSelectedToIconConverter(item)));

            icon.SetBinding(Image.WidthRequestProperty, new Binding(nameof(IconSize), source: this));
            icon.SetBinding(Image.MinimumWidthRequestProperty, new Binding(nameof(IconSize), source: this));
            icon.SetBinding(Image.HeightRequestProperty, new Binding(nameof(IconSize), source: this));
            icon.SetBinding(Image.MinimumHeightRequestProperty, new Binding(nameof(IconSize), source: this));

            icon.SetValue(Grid.ColumnProperty, 0);

            var iconTintColor = new IconTintColorBehavior();
            iconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(item.IsSelected), source: item, converter: new ItemSelectedToTextColorConverter(this)));
            iconTintColor.SetBinding(IconTintColorBehavior.IsEnabledProperty, new Binding(nameof(item.ApplyIconTintColor), source: item));
            icon.Behaviors.Add(iconTintColor);

            container.Children.Add(icon);
            label.SetValue(Grid.ColumnProperty, 1);
            container.Children.Add(label);

            card.Content = container;
        }
        else
        {
            card.Content = label;
        }
    }

    private void SetBackgroundColor()
    {
        if (_itemsContainer == null)
        {
            return;
        }

        if (Type == MaterialSegmentedButtonsType.Outlined)
        {
            _container.BackgroundColor = Colors.Transparent;
            _itemsContainer.BackgroundColor = Colors.Transparent;
        }
        else
        {
            _container.BackgroundColor = BackgroundColor;
            _itemsContainer.BackgroundColor = BackgroundColor;
        }
    }

    #endregion

    #region Converters

    private class ItemSelectedToIconConverter(MaterialSegmentedButtonsItem item) : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? item.SelectedIcon : item.UnselectedIcon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    private class ItemSelectedToBackgroundColorConverter(MaterialSegmentedButtons materialSegmentedButtons) : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return materialSegmentedButtons.SelectedBackgroundColor;
            }
            else
            {
                return materialSegmentedButtons.Type == MaterialSegmentedButtonsType.Outlined ? Colors.Transparent : materialSegmentedButtons.BackgroundColor;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    private class ItemSelectedToTextColorConverter(MaterialSegmentedButtons materialSegmentedButtons) : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return materialSegmentedButtons.SelectedTextColor;
            }
            else
            {
                return materialSegmentedButtons.TextColor;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    #endregion Converters
}