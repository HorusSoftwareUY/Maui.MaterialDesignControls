using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialChipsType
{
    /// <summary>Filter chips</summary>
    Filter,
    /// <summary>Assist, input and suggestion chips</summary>
    Normal
}

public enum IconStateType
{
    /// <summary>Visible both icon when selected</summary>
    BothVisible,
    /// <summary>Visible only Leading icon when selected</summary>
    LeadingVisible,
    /// <summary>Visible only Trailing icon when selected</summary>
    TrailingVisible
}

/// <summary>
/// A Chips help people enter information, make selections, filter content, or trigger actions <see href="https://m3.material.io/components/chips/overview">see here.</see>
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialChips.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialChips
///        Type="Normal"
///        IconStateOnSelection="BothVisible"
///        LeadingIcon="plus.png"
///        Text="Suggestion both"
///        TrailingIcon="horus_logo.png"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var chips = new MaterialChips
/// {
///     Type = MaterialChipsType.Normal,
///     IconStateOnSelection = IconStateType.BothVisible,
///     LeadingIcon = "plus.png",
///     Text = "Suggestion both",
///     TrailingIcon="horus_logo.png"
/// };
///</code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ChipsPage.xaml)
/// 
/// </example>
/// <todoList>
/// * .NET 7 not work LineBreakMode.
/// </todoList>
public class MaterialChips : ContentView, ITouchableView, IGroupableView
{
    #region Attributes

    private const MaterialChipsType DefaultChipsType = MaterialChipsType.Normal;
    private const IconStateType DefaultIconStateOnSelection = IconStateType.BothVisible;
    private const bool DefaultIsSelected = false;
    private const bool DefaultIsEnabled = true;
    private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(8);
    private static readonly Thickness DefaultPadding = new Thickness(16, 0);
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTouchAnimationType = _ => MaterialAnimation.TouchAnimationType;
    private static readonly ImageSource DefaultLeadingIcon = null!;
    private static readonly ImageSource DefaultTrailingIcon = null!;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultIconTintColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialLightTheme.Primary }.GetValueForCurrentTheme<Color>();
    private static readonly string DefaultText = string.Empty;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.LabelLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBackgroundColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow }.GetValueForCurrentTheme<Color>();
    private const double DefaultBorderWidth = 1;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBorderColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultShadow = _ => MaterialElevation.Level1;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultShadowColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Shadow, Dark = MaterialDarkTheme.Shadow }.GetValueForCurrentTheme<Color>();

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="MaterialChipsType" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialChipsType), typeof(MaterialChips), defaultValue: DefaultChipsType, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChips self)
        {
            self.SetState(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IconStateOnSelection" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IconStateOnSelectionProperty = BindableProperty.Create(nameof(IconStateOnSelection), typeof(IconStateType), typeof(MaterialChips), defaultValue: DefaultIconStateOnSelection, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChips self)
        {
            self.UpdatePadding();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Command" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialChips));
    
    /// <summary>
    /// Gets or sets the state when the Chips is selected.
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(MaterialChips), defaultValue: DefaultIsSelected, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChips self)
        {
            self.SetState(self.Type);
        }
    });

    /// <summary>
    /// Gets or sets the state when the Chips is selected.
    /// bindable property.
    /// </summary>
    public static readonly new BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialChips), defaultValue: DefaultIsEnabled, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChips self)
        {
            self.SetState(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Padding" />
    /// bindable property.
    /// </summary>
    public new static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialChips), defaultValue: DefaultPadding);

    /// <summary>
    /// The backing store for the <see cref="CornerRadius" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialChips), defaultValue: DefaultCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="LeadingIcon" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(ImageSource), typeof(MaterialChips), defaultValue: DefaultLeadingIcon, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChips self)
        {
            self.UpdatePadding();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TrailingIcon" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconProperty = BindableProperty.Create(nameof(TrailingIcon), typeof(ImageSource), typeof(MaterialChips), defaultValue: DefaultTrailingIcon, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChips self)
        {
            self.UpdatePadding();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Text" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialChips), defaultValue: DefaultText);

    /// <summary>
    /// The backing store for the text color
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialChips), defaultValueCreator: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="FontFamily" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialChips), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="FontSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialChips), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" />
    /// bindable property.
    /// </summary>
    public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialChips), defaultValueCreator: DefaultBackgroundColor);

    /// <summary>
    /// The backing store for the <see cref="BorderWidth"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialChips), defaultValue: DefaultBorderWidth);

    /// <summary>
    /// The backing store for the <see cref="BorderColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialChips), defaultValueCreator: DefaultBorderColor);

    /// <summary>
    /// The backing store for the <see cref="TouchAnimationType"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TouchAnimationTypeProperty = BindableProperty.Create(nameof(TouchAnimationType), typeof(TouchAnimationTypes), typeof(MaterialChips), defaultValueCreator: DefaultTouchAnimationType);

    /// <summary>
    /// The backing store for the <see cref="TouchAnimation"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TouchAnimationProperty = BindableProperty.Create(nameof(TouchAnimation), typeof(ITouchAnimation), typeof(MaterialChips));

    /// <summary>
    /// The backing store for the <see cref="LeadingIconTintColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconTintColorProperty = BindableProperty.Create(nameof(LeadingIconTintColor), typeof(Color), typeof(MaterialChips), defaultValueCreator: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="ApplyLeadingIconTintColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ApplyLeadingIconTintColorProperty = BindableProperty.Create(nameof(ApplyLeadingIconTintColor), typeof(bool), typeof(MaterialChips), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="TrailingIconTintColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconTintColorProperty = BindableProperty.Create(nameof(TrailingIconTintColor), typeof(Color), typeof(MaterialChips), defaultValueCreator: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="ApplyTrailingIconTintColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ApplyTrailingIconTintColorProperty = BindableProperty.Create(nameof(ApplyTrailingIconTintColor), typeof(bool), typeof(MaterialChips), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="ShadowColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(MaterialChips), defaultValueCreator: DefaultShadowColor);

    /// <summary>
    /// The backing store for the <see cref="Shadow" />
    /// bindable property.
    /// </summary>
    public static readonly new BindableProperty ShadowProperty = BindableProperty.Create(nameof(Shadow), typeof(Shadow), typeof(MaterialChips), defaultValueCreator: DefaultShadow);

    /// <summary>
    /// The backing store for the <see cref="GroupName" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty GroupNameProperty = BindableProperty.Create(nameof(GroupName), typeof(string), typeof(MaterialChips), defaultValue: null, propertyChanged: (bindableObject, oldValue, newValue) =>
    {
        if (bindableObject is MaterialChips self && oldValue is string oldGroupName && newValue is string newGroupName)
        {
            self.OnGroupNamePropertyChanged(oldGroupName, newGroupName);
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="Value"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(object), typeof(MaterialChips), defaultValue: null, propertyChanged: (bindableObject, _, _) => 
    { 
        if(bindableObject is MaterialChips self)
        {
            self.OnValuePropertyChanged();
        }
    });
    
    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets type Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialChipsType.Normal">MaterialChipsType.Normal</see>
    /// </default>
    /// <remarks>
    /// <para>Normal: They are for the types assist, input amd suggestion, chips help narrow a user’s intent by presenting dynamically generated suggestions, such as possible responses or search filters.</para>
    /// <para>Filter: chips use tags or descriptive words to filter content. They can be a good alternative to segmented buttons or checkboxes when viewing a list or search results.</para>
    /// </remarks>
    public MaterialChipsType Type
    {
        get => (MaterialChipsType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the badge type according to <see cref="IconStateType"/> enum.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="IconStateType.BothVisible">IconStateType.BothVisible</see>
    /// </default>
    public IconStateType IconStateOnSelection
    {
        get => (IconStateType)GetValue(IconStateOnSelectionProperty);
        set => SetValue(IconStateOnSelectionProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to invoke when the Chips is activated.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    /// <remarks>This property is used to associate a command with an instance of Chips. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
    /// <para><see cref="VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="Command.CanExecute(object)">Command.CanExecute(object)</see> if set.</para>
    /// </remarks>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the state when the Chips is selected.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="False"/>
    /// </default>
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    /// <summary>
    /// Gets or sets the state when the Chips is enabled.
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
    /// Gets or sets the padding for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Thickness(16,0)
    /// </default>
    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    /// <summary>
    /// Gets or sets the corner radius for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// CornerRadius(8)
    /// </default>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets image leading for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public ImageSource LeadingIcon
    {
        get => (ImageSource)GetValue(LeadingIconProperty);
        set => SetValue(LeadingIconProperty, value);
    }

    /// <summary>
    /// Gets or sets image trailing for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public ImageSource TrailingIcon
    {
        get => (ImageSource)GetValue(TrailingIconProperty);
        set => SetValue(TrailingIconProperty, value);
    }

    /// <summary>
    /// Gets or sets text the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="string.Empty"/>
    /// </default>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets text color the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark = <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for the text of this chips.
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
    /// Gets or sets the size of the font for the text of this chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.LabelLarge">MaterialFontSize.LabelLarge</see> / Tablet: 14 - Phone: 11
    /// </default>
    [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the background color of the chips.
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
    /// Gets or sets the width of the border, in device-independent units.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 1
    /// </default>
    /// <remarks>Set this value to a non-zero value in order to have a visible border.</remarks>
    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the border stroke color of the chips.
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
    /// Gets or sets a color that describes the leading icon color of the chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark = <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
    /// </default>
    public Color LeadingIconTintColor
    {
        get => (Color)GetValue(LeadingIconTintColorProperty);
        set => SetValue(LeadingIconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the if the leading icon applies the tint color.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="true"/>
    /// </default>
    public bool ApplyLeadingIconTintColor
    {
        get => (bool)GetValue(ApplyLeadingIconTintColorProperty);
        set => SetValue(ApplyLeadingIconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the trailing icon color of the chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark = <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
    /// </default>
    public Color TrailingIconTintColor
    {
        get => (Color)GetValue(TrailingIconTintColorProperty);
        set => SetValue(TrailingIconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the if the trailing icon applies the tint color.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="true"/>
    /// </default>
    public bool ApplyTrailingIconTintColor
    {
        get => (bool)GetValue(ApplyTrailingIconTintColorProperty);
        set => SetValue(ApplyTrailingIconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the shadow color of the chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Shadow">MaterialLightTheme.Shadow</see> - Dark = <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.Shadow</see>
    /// </default>
    public Color ShadowColor
    {
        get => (Color)GetValue(ShadowColorProperty);
        set => SetValue(ShadowColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the shadow effect cast by the element.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialElevation.Level1">MaterialElevation.Level1</see>
    /// </default>
    public new Shadow Shadow
    {
        get => (Shadow)GetValue(ShadowProperty);
        set => SetValue(ShadowProperty, value);
    }

    /// <summary>
    /// Gets or sets an animation to be executed when is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="TouchAnimationTypes.Fade"> TouchAnimationTypes.Fade </see>
    /// </default>
    public TouchAnimationTypes TouchAnimationType
    {
        get => (TouchAnimationTypes)GetValue(TouchAnimationTypeProperty);
        set => SetValue(TouchAnimationTypeProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom animation to be executed when is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public ITouchAnimation TouchAnimation
    {
        get => (ITouchAnimation)GetValue(TouchAnimationProperty);
        set => SetValue(TouchAnimationProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the <see cref="string" /> GroupName for the chips. 
    /// This is a bindable property.
    /// </summary>
    public string GroupName
    {
        get => (string)GetValue(GroupNameProperty);
        set => SetValue(GroupNameProperty, value);
    }
    
    /// <summary>
    /// Defines the value of radio button selected
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public object Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public bool AllowEmptySelection { get; set; } = true;

    #endregion Properties

    #region Events

    private EventHandler? _clicked;
    private readonly object _objectLock = new();

    /// <summary>
    /// Occurs when the chips is clicked/tapped.
    /// </summary>
    public event EventHandler? Clicked
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

    public async void OnTouch(TouchEventType gestureType)
    {
        await TouchAnimationManager.AnimateAsync(this, gestureType);

        if (gestureType == TouchEventType.Released)
        {
            MaterialViewGroup.UpdateGroupSelection(this);
            
            if (Command != null && Command.CanExecute(IsSelected))
            {
                Command.Execute(IsSelected);
            }
            else if (_clicked != null)
            {
                _clicked.Invoke(this, new IsSelectedEventArgs(IsSelected));
            }
        }
    }

    protected virtual void InternalPressedHandler(object? sender, EventArgs e)
    {
        if (IsEnabled)
        {
            OnTouch(TouchEventType.Pressed);
        }
    }

    protected virtual void InternalReleasedHandler(object? sender, EventArgs e)
    {
        if (IsEnabled)
        {
            if (Type == MaterialChipsType.Normal)
            {
                VisualStateManager.GoToState(this, ChipsCommonStates.Normal);
            }
            else if (AllowEmptySelection || !IsSelected)
            {
                IsSelected = !IsSelected;
                UpdatePadding();
                VisualStateManager.GoToState(this, (IsSelected) ? ChipsCommonStates.Selected : ChipsCommonStates.Unselected);
            }

            OnTouch(TouchEventType.Released);
        }
    }

    #endregion Events

    #region Layout

    private MaterialCard _container = null!;
    private HorizontalStackLayout _hStack = null!;
    private MaterialLabel _textLabel = null!;
    private Image _leadingIcon = null!;
    private Image _trailingIcon = null!;

    #endregion Layout

    #region Constructors

    public MaterialChips(bool toGroup)
    {
        CreateLayout(toGroup);
    }

    public MaterialChips()
    {
        CreateLayout();
    }

    #endregion Constructors

    #region Methods

    private void CreateLayout(bool toGroup = false)
    {
        _container = new MaterialCard
        {
            MinimumHeightRequest = 32,
            Type = MaterialCardType.Custom
        };

        if (!toGroup)
        {
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            _container.HorizontalOptions = HorizontalOptions;
            _container.VerticalOptions = VerticalOptions;
        }

        _leadingIcon = new Image
        {
            Margin = new Thickness(0, 0, 8, 0),
            Aspect = Aspect.AspectFit,
            IsVisible = false,
            HeightRequest = 18,
            WidthRequest = 18,
        };

        var leadingIconTintColor = new IconTintColorBehavior();
        leadingIconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(LeadingIconTintColor), source: this));
        leadingIconTintColor.SetBinding(IconTintColorBehavior.IsEnabledProperty, new Binding(nameof(ApplyLeadingIconTintColor), source: this));
        _leadingIcon.Behaviors.Add(leadingIconTintColor);

        _textLabel = new MaterialLabel
        {
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Start,
            BackgroundColor = Colors.Transparent,
            Margin = new Thickness(0),
            LineBreakMode = LineBreakMode.TailTruncation
        };

        _trailingIcon = new Image
        {
            Margin = new Thickness(8, 0, 0, 0),
            Aspect = Aspect.AspectFit,
            IsVisible = false,
            HeightRequest = 18,
            WidthRequest = 18,
        };

        var trailingIconTintColor = new IconTintColorBehavior();
        trailingIconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(TrailingIconTintColor), source: this));
        trailingIconTintColor.SetBinding(IconTintColorBehavior.IsEnabledProperty, new Binding(nameof(ApplyTrailingIconTintColor), source: this));
        _trailingIcon.Behaviors.Add(trailingIconTintColor);

        _hStack = new HorizontalStackLayout
        {
            _leadingIcon,
            _textLabel,
            _trailingIcon
        };

        _hStack.HorizontalOptions = LayoutOptions.Center;
        _hStack.VerticalOptions = LayoutOptions.Center;
        _hStack.Spacing = 0;

        _textLabel.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
        _textLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _textLabel.SetBinding(Label.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _textLabel.SetBinding(Label.FontSizeProperty, new Binding(nameof(FontSize), source: this));

        _container.SetBinding(MaterialCard.PaddingProperty, new Binding(nameof(Padding), source: this));
        _container.SetBinding(MaterialCard.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
        _container.SetBinding(MaterialCard.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
        _container.SetBinding(MaterialCard.HeightRequestProperty, new Binding(nameof(HeightRequest), source: this));
        _container.SetBinding(MaterialCard.WidthRequestProperty, new Binding(nameof(WidthRequest), source: this));
        _container.SetBinding(MaterialCard.StrokeThicknessProperty, new Binding(nameof(BorderWidth), source: this));
        _container.SetBinding(MaterialCard.StrokeProperty, new Binding(nameof(BorderColor), source: this));
        _container.SetBinding(MaterialCard.ShadowProperty, new Binding(nameof(Shadow), source: this));
        _container.SetBinding(MaterialCard.ShadowColorProperty, new Binding(nameof(ShadowColor), source: this));
        _container.SetBinding(MaterialCard.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));

        _leadingIcon.SetBinding(Image.SourceProperty, new Binding(nameof(LeadingIcon), source: this));
        _trailingIcon.SetBinding(Image.SourceProperty, new Binding(nameof(TrailingIcon), source: this));

        _container.Pressed += InternalPressedHandler;
        _container.Released += InternalReleasedHandler;

        _container.Content = _hStack;
        Content = _container;
    }
    
    void OnValuePropertyChanged()
    {
        if (!IsSelected || string.IsNullOrEmpty(GroupName))
        {
            return;
        }

        var controller = MaterialViewGroupController.GetGroupController(this);
        controller?.HandleMaterialViewValueChanged(this);
    }

    void OnGroupNamePropertyChanged(string oldGroupName, string newGroupName)
    {
        if (!string.IsNullOrEmpty(oldGroupName) && !string.IsNullOrEmpty(newGroupName) && newGroupName != oldGroupName)
        {
            var controller = MaterialViewGroupController.GetGroupController(this);
            controller?.HandleMaterialViewGroupNameChanged(oldGroupName);
        }
    }
    
    public void OnGroupSelectionChanged(IGroupableView groupableView)
    {
        var controller = MaterialViewGroupController.GetGroupController(this);
        controller?.HandleMaterialViewGroupSelectionChanged(groupableView);
    }
    
    #endregion Methods

    #region Setters

    private void SetState(MaterialChipsType type)
    {
        if (type == MaterialChipsType.Normal)
        {
            VisualStateManager.GoToState(this, ChipsCommonStates.Normal);
        }
        else
        {
            if (IsSelected)
            {
                VisualStateManager.GoToState(this, ChipsCommonStates.Selected);
            }
            else
            {
                VisualStateManager.GoToState(this, ChipsCommonStates.Unselected);
            }
        }

        if (!IsEnabled)
        {
            VisualStateManager.GoToState(this, ChipsCommonStates.Disabled);
        }
    }

    private void UpdatePadding()
    {
        bool containLeadingIcon = (LeadingIcon != null && !LeadingIcon.IsEmpty);
        bool containTrailingIcon = (TrailingIcon != null && !TrailingIcon.IsEmpty);

        if (Type == MaterialChipsType.Normal)
        {
            _container.Padding = new Thickness(containLeadingIcon ? 8 : 16, 0, containTrailingIcon ? 8 : 16, 0);
            _leadingIcon.IsVisible = containLeadingIcon;
            _trailingIcon.IsVisible = containTrailingIcon;
        }
        else
        {
            switch (IconStateOnSelection)
            {
                case IconStateType.BothVisible:
                    _container.Padding = new Thickness((containLeadingIcon && IsSelected) ? 8 : 16, 0, (containTrailingIcon && IsSelected) ? 8 : 16, 0);
                    _leadingIcon.IsVisible = containLeadingIcon && IsSelected;
                    _trailingIcon.IsVisible = containTrailingIcon && IsSelected;
                    break;
                case IconStateType.LeadingVisible:
                    _container.Padding = new Thickness((containLeadingIcon && IsSelected) ? 8 : 16, 0, 16, 0);
                    _leadingIcon.IsVisible = containLeadingIcon && IsSelected;
                    _trailingIcon.IsVisible = false;
                    break;
                case IconStateType.TrailingVisible:
                    _container.Padding = new Thickness(16, 0, (containTrailingIcon && IsSelected) ? 8 : 16, 0);
                    _leadingIcon.IsVisible = false;
                    _trailingIcon.IsVisible = containTrailingIcon && IsSelected;
                    break;
                default:
                    _container.Padding = new Thickness((containLeadingIcon && IsSelected) ? 8 : 16, 0, (containTrailingIcon && IsSelected) ? 8 : 16, 0);
                    _leadingIcon.IsVisible = containLeadingIcon && IsSelected;
                    _trailingIcon.IsVisible = containTrailingIcon && IsSelected;
                    break;
            }
        }
    }

    #endregion Setters

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabled = new VisualState { Name = ChipsCommonStates.Disabled };

        disabled.Setters.Add(
            MaterialChips.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        disabled.Setters.Add(
            MaterialChips.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        disabled.Setters.Add(
            MaterialChips.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(
            MaterialChips.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(
            MaterialChips.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(
            MaterialChips.ShadowColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Shadow,
                Dark = MaterialDarkTheme.Shadow
            }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

        var normal = new VisualState { Name = ChipsCommonStates.Normal };

        normal.Setters.Add(
            MaterialChips.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Outline,
                Dark = MaterialDarkTheme.Outline
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialChips.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SurfaceContainerLow,
                Dark = MaterialDarkTheme.SurfaceContainerLow
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialChips.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialChips.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialChips.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialChips.ShadowColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Shadow,
                Dark = MaterialDarkTheme.Shadow
            }
                .GetValueForCurrentTheme<Color>());

        var unselected = new VisualState { Name = ChipsCommonStates.Unselected };

        unselected.Setters.Add(
            MaterialChips.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Outline,
                Dark = MaterialDarkTheme.Outline
            }
            .GetValueForCurrentTheme<Color>());

        unselected.Setters.Add(
            MaterialChips.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SurfaceContainerLow,
                Dark = MaterialDarkTheme.SurfaceContainerLow
            }
            .GetValueForCurrentTheme<Color>());

        unselected.Setters.Add(
            MaterialChips.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        unselected.Setters.Add(
            MaterialChips.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>());

        unselected.Setters.Add(
            MaterialChips.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        unselected.Setters.Add(
            MaterialChips.ShadowColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Shadow,
                Dark = MaterialDarkTheme.Shadow
            }
                .GetValueForCurrentTheme<Color>());

        var selected = new VisualState { Name = ChipsCommonStates.Selected };

        selected.Setters.Add(
            MaterialChips.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SecondaryContainer,
                Dark = MaterialDarkTheme.SecondaryContainer
            }
                .GetValueForCurrentTheme<Color>());

        selected.Setters.Add(
            MaterialChips.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SecondaryContainer,
                Dark = MaterialDarkTheme.SecondaryContainer
            }
                .GetValueForCurrentTheme<Color>());

        selected.Setters.Add(
            MaterialChips.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSecondaryContainer,
                Dark = MaterialDarkTheme.OnSecondaryContainer
            }
                .GetValueForCurrentTheme<Color>());

        selected.Setters.Add(
            MaterialChips.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSecondaryContainer,
                Dark = MaterialDarkTheme.OnSecondaryContainer
            }
                .GetValueForCurrentTheme<Color>());

        selected.Setters.Add(
            MaterialChips.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSecondaryContainer,
                Dark = MaterialDarkTheme.OnSecondaryContainer
            }
                .GetValueForCurrentTheme<Color>());

        selected.Setters.Add(
            MaterialChips.ShadowColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Shadow,
                Dark = MaterialDarkTheme.Shadow
            }
                .GetValueForCurrentTheme<Color>());

        commonStatesGroup.States.Add(disabled);
        commonStatesGroup.States.Add(normal);
        commonStatesGroup.States.Add(unselected);
        commonStatesGroup.States.Add(selected);

        var style = new Style(typeof(MaterialChips));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }

    #endregion Styles
}

public class ChipsCommonStates : VisualStateManager.CommonStates
{
    public const string Unselected = "Unselected";
}