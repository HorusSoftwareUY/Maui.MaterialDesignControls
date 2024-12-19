using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialSegmentedType
{
    Outlined, 
    Filled
}

public class MaterialSegmented : ContentView, ITouchable
{
    
    #region Attributes

    private readonly static MaterialSegmentedType DefautlSegmentedType = MaterialSegmentedType.Outlined;
    private readonly static IEnumerable<MaterialSegmentedItem> DefaultItemsSource = null;
    private readonly static MaterialSegmentedItem DefaultSelectedItem = null;
    private readonly static bool DefaultAllowMultiSelect = false;
    private readonly static bool DefaultIsEnabled = true;
    private readonly static Thickness DefaultPadding = new(12, 0);
    private readonly static CornerRadius DefaultCornerRadius = new(16);
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static string DefaultSelectedFontFamily = MaterialFontFamily.Default;
    private readonly static double DefaultFontSize = MaterialFontSize.BodyLarge;
    private readonly static double DefaultSelectedFontSize = MaterialFontSize.BodyLarge;
    private readonly static Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultDisableBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultSelectedColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SecondaryContainer, Dark = MaterialDarkTheme.SecondaryContainer }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultUnselectedColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultDisabledUnselectedColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultSelectedTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSecondaryContainer, Dark = MaterialDarkTheme.OnSecondaryContainer }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultDisabledSelectedTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultUnselectedTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultDisabledUnselectedTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
    private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
    
    #endregion
    
    #region Bindable Properties
    
    /// <summary>
    /// The backing store for the <see cref="MaterialSegmentedType" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialSegmentedType), typeof(MaterialSegmented), defaultValue: DefautlSegmentedType, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmented self)
        {
            self.UpdateAndInitControl();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="ItemsSource" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<MaterialSegmentedItem>), typeof(MaterialSegmented), defaultValue: DefaultItemsSource, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmented self)
        {
            self.UpdateItemsSource();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="SelectedItem" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(MaterialSegmentedItem), typeof(MaterialSegmented), defaultValue: DefaultSelectedItem, BindingMode.TwoWay);
    
    
    /// <summary>
    /// The backing store for the <see cref="SelectedItems" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(IEnumerable<MaterialSegmentedItem>), typeof(MaterialSegmented), defaultValue: DefaultSelectedItem, BindingMode.OneWayToSource);
    
    /// <summary>
    /// Gets or sets the state when the Segmented permitted multiple selection
    /// The backing store for the <see cref="AllowMultiSelect" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AllowMultiSelectProperty = BindableProperty.Create(nameof(AllowMultiSelect), typeof(bool), typeof(MaterialSegmented), defaultValue: DefaultAllowMultiSelect, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmented self)
        {
            self.UpdateItemsSource();
        }
    });
    
    /// <summary>
    /// Gets or sets the state when the Segmented is enabled.
    /// bindable property.
    /// </summary>
    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialSegmented), defaultValue: DefaultIsEnabled, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmented self)
        {
            self.UpdateAndInitControl();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="Padding" />
    /// bindable property.
    /// </summary>
    public static new readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialSegmented), defaultValue: DefaultPadding);
    
    /// <summary>
    /// The backing store for the <see cref="CornerRadius" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialSegmented), defaultValue: DefaultCornerRadius);
    
    /// <summary>
    /// The backing store for the <see cref="FontFamily" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialSegmented), defaultValue: DefaultFontFamily);
    
    /// <summary>
    /// The backing store for the <see cref="SelectedFontFamily" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedFontFamilyProperty = BindableProperty.Create(nameof(SelectedFontFamily), typeof(string), typeof(MaterialSegmented), defaultValue: DefaultSelectedFontFamily);
    
    /// <summary>
    /// The backing store for the <see cref="FontSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialSegmented), defaultValue: DefaultFontSize);
    
    /// <summary>
    /// The backing store for the <see cref="SelectedFontSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedFontSizeProperty = BindableProperty.Create(nameof(SelectedFontSize), typeof(double), typeof(MaterialSegmented), defaultValue: DefaultSelectedFontSize);
    
    /// <summary>
    /// The backing store for the <see cref="BorderColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialSegmented), defaultValue: DefaultBorderColor);
    
    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" />
    /// bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialSegmented), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmented self)
        {
            self.SetBackgroundColor();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="DisableBackgroundColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty DisableBackgroundColorProperty = BindableProperty.Create(nameof(DisableBackgroundColor), typeof(Color), typeof(MaterialSegmented), defaultValue: DefaultDisableBackgroundColor);

    /// <summary>
    /// The backing store for the <see cref="SelectedColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(MaterialSegmented), defaultValue: DefaultSelectedColor);

    /// <summary>
    /// The backing store for the <see cref="UnselectedColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(MaterialSegmented), defaultValue: DefaultUnselectedColor);

    /// <summary>
    /// The backing store for the <see cref="DisabledUnselectedColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty DisabledUnselectedColorProperty = BindableProperty.Create(nameof(DisabledUnselectedColor), typeof(Color), typeof(MaterialSegmented), defaultValue: DefaultDisabledUnselectedColor);

    /// <summary>
    /// The backing store for the <see cref="SelectedTextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(MaterialSegmented), defaultValue: DefaultSelectedTextColor);

    /// <summary>
    /// The backing store for the <see cref="DisabledSelectedTextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty DisabledSelectedTextColorProperty = BindableProperty.Create(nameof(DisabledSelectedTextColor), typeof(Color), typeof(MaterialSegmented), defaultValue: DefaultDisabledSelectedTextColor);

    /// <summary>
    /// The backing store for the <see cref="UnselectedTextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty UnselectedTextColorProperty = BindableProperty.Create(nameof(UnselectedTextColor), typeof(Color), typeof(MaterialSegmented), defaultValue: DefaultUnselectedTextColor);

    /// <summary>
    /// The backing store for the <see cref="DisabledUnselectedTextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty DisabledUnselectedTextColorProperty = BindableProperty.Create(nameof(DisabledUnselectedTextColor), typeof(Color), typeof(MaterialSegmented), defaultValue: DefaultDisabledUnselectedTextColor);
    
    /// <summary>
    /// The backing store for the <see cref="Command" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialSegmented));
    
    /// <summary>
    /// The backing store for the <see cref="CommandParameter" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialSegmented));

    /// <summary>
    /// The backing store for the <see cref="Animation"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialSegmented), defaultValue: DefaultAnimationType);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialSegmented), defaultValue: DefaultAnimationParameter);
    
    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialSegmented));

    #endregion
    
    #region Properties
    
    /// <summary>
    /// Gets or sets type Segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialSegmentedType.Outlined">MaterialSegmentedType.Outlined</see>
    /// </default>
    /// <remarks>
    /// <para>Outlined:</para>
    /// <para>Filled:</para>
    /// </remarks>
    public MaterialSegmentedType Type
    {
        get => (MaterialSegmentedType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets Items source Segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Null"/>
    /// </default>
    public IEnumerable<MaterialSegmentedItem> ItemsSource
    {
        get => (IEnumerable<MaterialSegmentedItem>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    
    /// <summary>
    /// Gets or sets SelectedItems in Segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Array.Empty"/>
    /// </default>
    public IEnumerable<MaterialSegmentedItem> SelectedItems
    {
        get { return ItemsSource != null ? ItemsSource.Where(w => w.IsSelected) : Array.Empty<MaterialSegmentedItem>(); }
    }
    
    /// <summary>
    /// Gets or sets item selected in Segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Null"/>
    /// </default>
    public MaterialSegmentedItem SelectedItem
    {
        get => (MaterialSegmentedItem)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the state when the Segmented permitted multiple selection
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
    /// Gets or sets the padding for the Segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Thickness(12,0)
    /// </default>
    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the corner radius for the Segmented.
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
    /// Gets or sets the font family for the text of this Segmented.
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
    /// Gets or sets the font family for the text selected of this Segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string SelectedFontFamily
    {
        get => (string)GetValue(SelectedFontFamilyProperty);
        set => SetValue(SelectedFontFamilyProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the size of the font for the text of this segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyLarge">MaterialFontSize.BodyLarge</see> / Tablet: 19 - Phone: 16
    /// </default>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the size of the font for the text selected of this segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyLarge">MaterialFontSize.BodyLarge</see> / Tablet: 19 - Phone: 16
    /// </default>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double SelectedFontSize
    {
        get => (double)GetValue(SelectedFontSizeProperty);
        set => SetValue(SelectedFontSizeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the border color for the segmented.
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
    /// Gets or sets the Background color for the segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.SurfaceContainerLow">MaterialLightTheme.SurfaceContainerLow</see> - Dark = <see cref="MaterialDarkTheme.SurfaceContainerLow">MaterialDarkTheme.SurfaceContainerLow</see>
    /// </default>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the Disabled Background color for the segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurface">MaterialLightTheme.OnSurface</see> - Dark = <see cref="MaterialDarkTheme.OnSurface">MaterialDarkTheme.OnSurface</see>
    /// </default>
    public Color DisableBackgroundColor
    {
        get => (Color)GetValue(DisableBackgroundColorProperty);
        set => SetValue(DisableBackgroundColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the items Selected color for the segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.SecondaryContainer">MaterialLightTheme.SecondaryContainer</see> - Dark = <see cref="MaterialDarkTheme.SecondaryContainer">MaterialDarkTheme.SecondaryContainer</see>
    /// </default>
    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the items Unselected color for the segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.SurfaceContainerLow">MaterialLightTheme.SurfaceContainerLow</see> - Dark = <see cref="MaterialDarkTheme.SurfaceContainerLow">MaterialDarkTheme.SurfaceContainerLow</see>
    /// </default>
    public Color UnselectedColor
    {
        get => (Color)GetValue(UnselectedColorProperty);
        set => SetValue(UnselectedColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the items Disable Unselected color for the segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurface">MaterialLightTheme.OnSurface</see> - Dark = <see cref="MaterialDarkTheme.OnSurface">MaterialDarkTheme.OnSurface</see>
    /// </default>
    public Color DisabledUnselectedColor
    {
        get => (Color)GetValue(DisabledUnselectedColorProperty);
        set => SetValue(DisabledUnselectedColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the items text selected color for the segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSecondaryContainer">MaterialLightTheme.OnSecondaryContainer</see> - Dark = <see cref="MaterialDarkTheme.OnSecondaryContainer">MaterialDarkTheme.OnSecondaryContainer</see>
    /// </default>
    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the items disabled text selected color for the segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurface">MaterialLightTheme.OnSurface</see> - Dark = <see cref="MaterialDarkTheme.OnSurface">MaterialDarkTheme.OnSurface</see>
    /// </default>
    public Color DisabledSelectedTextColor
    {
        get => (Color)GetValue(DisabledSelectedTextColorProperty);
        set => SetValue(DisabledSelectedTextColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the items Unselected text color for the segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurface">MaterialLightTheme.OnSurface</see> - Dark = <see cref="MaterialDarkTheme.OnSurface">MaterialDarkTheme.OnSurface</see>
    /// </default>
    public Color UnselectedTextColor
    {
        get => (Color)GetValue(UnselectedTextColorProperty);
        set => SetValue(UnselectedTextColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the items disabled unselected text color for the segmented.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurface">MaterialLightTheme.OnSurface</see> - Dark = <see cref="MaterialDarkTheme.Outline">MaterialDarkTheme.OnSurface</see>
    /// </default>
    public Color DisabledUnselectedTextColor
    {
        get => (Color)GetValue(DisabledUnselectedTextColorProperty);
        set => SetValue(DisabledUnselectedTextColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the command to invoke when one each item in segmented is activated.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    /// <remarks>This property is used to associate a command with an instance of segmented. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
    /// <para><see cref="VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="Command.CanExecute(object)">Command.CanExecute(object)</see> if set.</para>
    /// </remarks>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Command"/> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
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
    
    private EventHandler _clicked;
    private readonly object _objectLock = new();
    
    /// <summary>
    /// Occurs when clicked/tapped in any items of segmented.
    /// </summary>
    public event EventHandler Clicked
    {
        add
        {
            lock (_objectLock)
            {
                _clicked += value;
                _container.Clicked += value;
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _clicked -= value;
                _container.Clicked -= value;
            }
        }
    }
    
    public async void OnTouch(TouchType gestureType)
    {
        await TouchAnimation.AnimateAsync(this, gestureType);
        if (gestureType == TouchType.Released)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }
    }
    
    protected virtual void InternalPressedHandler(object sender, EventArgs e, MaterialSegmentedItem item)
    {
        if (!AllowMultiSelect)
        {
            foreach (var items in ItemsSource)
            {
                items.IsSelected = false;
            }
        }

        item.IsSelected = !item.IsSelected;
        SelectedItem = item;
        UpdateItemsSource();
        OnTouch(TouchType.Released);
    }

    #endregion
    
    #region Layout

    private MaterialCard _container;
    private Grid _itemsContainer;

    #endregion

    #region constructor

    public MaterialSegmented()
    {
        
        _itemsContainer = new Grid
        {
            ColumnSpacing = 0,
            Padding = 3,
            BackgroundColor = Colors.Transparent
        };
        
        _container = new MaterialCard
        {
            Type = MaterialCardType.Filled,
            BackgroundColor = BackgroundColor,
            HeightRequest = 40,
            MinimumHeightRequest = 40,
            Padding = 0,
            Content = _itemsContainer,
            Shadow = MaterialElevation.Level1
        };
        
        _container.SetBinding(MaterialCard.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
        
        UpdateAndInitControl();
        
        Content = _container;
    }

    #endregion

    #region Setters

    private void UpdateAndInitControl()
    {
        SetSegmentedType();
        UpdateItemsSource();
    }

    private void UpdateItemsSource()
    {
        _itemsContainer.Children.Clear();
        _itemsContainer.ColumnDefinitions = new ColumnDefinitionCollection();
        _itemsContainer.ColumnSpacing = -2;
        if(ItemsSource == null) return;
        int indexColum = 0;
        foreach (var item in ItemsSource)
        {
            var card = new MaterialCard
            {
                Type = MaterialCardType.Custom,
                Padding = new Thickness(12, 0),
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = IsEnabled ? UnselectedColor : DisabledUnselectedColor,
                Margin = new Thickness(0),
                BorderWidth = 1
            };
			
            if (Type == MaterialSegmentedType.Outlined)
            {
                card.BorderColor = BorderColor;
                if (ItemsSource.Count() > 1)
                {
                    if (indexColum == 0)
                    {
                        card.CornerRadius =  new CornerRadius(CornerRadius.TopLeft, 0, CornerRadius.BottomLeft, 0);
                    }
                    else if (indexColum == ItemsSource.Count() - 1)
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
                card.BorderColor = Colors.Transparent;
                card.CornerRadius = CornerRadius;
                card.BorderWidth = 0;
            }

            var label = new MaterialLabel
            {
                Text = item.Text.Trim(),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = FontSize,
                FontFamily = FontFamily,
                TextColor = IsEnabled ? UnselectedTextColor : DisabledUnselectedTextColor
            };
            
            SetItemContentAndColors(card, label, item);
            if(IsEnabled)
            {
                card.Clicked += (sender, args) => InternalPressedHandler(sender, args, item);
            }
            _itemsContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(ItemsSource.Count() / 100.0, GridUnitType.Star) });
            _itemsContainer.Add(card, indexColum);
            indexColum++;
        }
        
    }
    
    private void SetSegmentedType()
    { 
        _itemsContainer.BackgroundColor = IsEnabled ? UnselectedColor : DisabledUnselectedColor;
        if (Type == MaterialSegmentedType.Outlined)
        { 
            _container.BackgroundColor = Colors.Transparent;
            _container.BorderColor = BorderColor;
            _container.BorderWidth = 1;

        }
        else
        { 
            _container.BackgroundColor = BackgroundColor;
            _container.BorderColor = Colors.Transparent;
            _container.BorderWidth = 0;
        }

        VisualStateManager.GoToState(this, IsEnabled ? SegmentedCommonStates.Normal : SegmentedCommonStates.Disabled);
    }

    private void SetBackgroundColor()
    {
        if (Type == MaterialSegmentedType.Outlined)
        {
            _itemsContainer.BackgroundColor = Colors.Transparent;
        }
        else
        {
            _itemsContainer.BackgroundColor = BackgroundColor;
        }
        
    }
    
    
    #endregion

    #region Methods

    private void SetItemContentAndColors(MaterialCard card, MaterialLabel label, MaterialSegmentedItem item)
    {
        Grid container = null;
        Image selectedIcon = null;

        if ((item.IsSelected && item.SelectedIconIsVisible) || (!item.IsSelected && item.UnselectedIconIsVisible))
        {
            double iconSize = 18; 
            container = new Grid
            {
                ColumnSpacing = 8,
                HorizontalOptions = LayoutOptions.Center
            };
            container.ColumnDefinitions.Add(new ColumnDefinition { Width = iconSize });
            container.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            selectedIcon = new Image
            {
                WidthRequest = iconSize,
                MinimumHeightRequest = iconSize,
                MinimumWidthRequest = iconSize,
                HeightRequest = iconSize,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = true
            };

            selectedIcon.SetValue(Grid.ColumnProperty, 0);
        }

        if (item.IsSelected)
        {
            card.BackgroundColor = IsEnabled ? SelectedColor : UnselectedColor;
            label.TextColor = IsEnabled ? SelectedTextColor : UnselectedTextColor;
            label.FontSize = SelectedFontSize;
            label.FontFamily = SelectedFontFamily;

            if (item.SelectedIconIsVisible)
            {
                selectedIcon.Source = item.SelectedIcon;
                var IconTintColor = new IconTintColorBehavior
                {
                    TintColor = item.SelectedIconColor
                };
                selectedIcon.Behaviors.Add(IconTintColor);
                label.SetValue(Grid.ColumnProperty, 1);
                container.Children.Add(selectedIcon);
                container.Children.Add(label);

                card.Content = container;
            }
            else
            {
                card.Content = label;
            }
        }
        else
        {
            card.BackgroundColor = IsEnabled ? UnselectedColor : DisabledUnselectedColor;
            label.TextColor = IsEnabled ? UnselectedTextColor : DisabledUnselectedTextColor;
            label.FontSize = FontSize;
            label.FontFamily = FontFamily;
            

            if (item.UnselectedIconIsVisible)
            {
                selectedIcon.Source = item.UnselectedIcon;
                var IconTintColor = new IconTintColorBehavior
                {
                    TintColor = item.UnselectedIconColor
                };
                selectedIcon.Behaviors.Add(IconTintColor);
                label.SetValue(Grid.ColumnProperty, 1);
                container.Children.Add(selectedIcon);
                container.Children.Add(label);

                card.Content = container;
            }
            else
            {
                card.Content = label;
            }
        }
    }

    #endregion

    #region Styles
    
    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };
        
        var disabled = new VisualState { Name = SegmentedCommonStates.Disabled };
        
        disabled.Setters.Add(
            MaterialSegmented.BorderColorProperty,
            new AppThemeBindingExtension 
            { 
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));
        
        disabled.Setters.Add(
            MaterialSegmented.BackgroundColorProperty,
            new AppThemeBindingExtension 
                { 
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));
        
        disabled.Setters.Add(
            MaterialSegmented.DisableBackgroundColorProperty,
            new AppThemeBindingExtension 
                { 
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));
        
        disabled.Setters.Add(
            MaterialSegmented.SelectedColorProperty,
            new AppThemeBindingExtension 
                { 
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));
        
        disabled.Setters.Add(
            MaterialSegmented.UnselectedColorProperty,
            new AppThemeBindingExtension 
                { 
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));
        
        disabled.Setters.Add(
            MaterialSegmented.DisabledUnselectedColorProperty,
            new AppThemeBindingExtension 
                { 
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));
        
        disabled.Setters.Add(
            MaterialSegmented.SelectedTextColorProperty,
            new AppThemeBindingExtension 
                { 
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));
        
        disabled.Setters.Add(
            MaterialSegmented.DisabledSelectedTextColorProperty,
            new AppThemeBindingExtension 
                { 
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));
        
        disabled.Setters.Add(
            MaterialSegmented.UnselectedTextColorProperty,
            new AppThemeBindingExtension 
                { 
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));
        
        disabled.Setters.Add(
            MaterialSegmented.DisabledUnselectedTextColorProperty,
            new AppThemeBindingExtension 
                { 
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));
        
        commonStatesGroup.States.Add(disabled);
        var style = new Style(typeof(MaterialSegmented));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });
        return new List<Style> { style };
    }

    #endregion
}

public class SegmentedCommonStates : VisualStateManager.CommonStates
{
    public const string Unselected = "Unselected";
}

public class MaterialSegmentedItem
{
    public string Text { get; set; }

    public string SelectedIcon { get; set; }
    public Color SelectedIconColor { get; set; }
    
    public string UnselectedIcon { get; set; }
    public Color UnselectedIconColor { get; set; }

    public bool IsSelected { get; set; }

    internal bool UnselectedIconIsVisible
    {
        get { return !string.IsNullOrEmpty(UnselectedIcon); }
    }

    internal bool SelectedIconIsVisible
    {
        get { return !string.IsNullOrEmpty(SelectedIcon); }
    }

    public override bool Equals(object obj)
    {
        if (obj is not MaterialSegmentedItem toCompare)
            return false;
        else
            return toCompare.Text.Equals(this.Text, System.StringComparison.InvariantCultureIgnoreCase);
    }

    public override string ToString() => !string.IsNullOrWhiteSpace(Text) ? Text : "Unavailable";
}