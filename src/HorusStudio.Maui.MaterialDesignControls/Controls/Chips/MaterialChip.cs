using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialChipType
{
    /// <summary>Filter chips</summary>
    Filter,
    /// <summary>Assist, input and suggestion chips</summary>
    Normal
}

/// <summary>
/// Chips help people enter information, make selections, filter content, or trigger actions and follow Material Design Guidelines. <see href="https://m3.material.io/components/chips/overview">See more.</see>
/// </summary>
/// <remarks>The <see href="docs/Controls/horusstudio.maui.materialdesigncontrols.materialviewgroup.md">MaterialViewGroup</see> class allows grouping chips, providing control over the selection type (Single or Multiple), item selection through bindings, and commands that trigger when the selection changes.</remarks>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialChip.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialChip
///        Type="Normal"
///        LeadingIcon="plus.png"
///        Text="Suggestion both"
///        TrailingIcon="horus_logo.png"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var chip = new MaterialChip
/// {
///     Type = MaterialChipType.Normal,
///     LeadingIcon = "plus.png",
///     Text = "Suggestion both",
///     TrailingIcon="horus_logo.png"
/// };
///</code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ChipsPage.xaml)
/// 
/// </example>
public class MaterialChip : ContentView, ITouchableView, IGroupableView
{
    #region Attributes

    private const MaterialChipType DefaultChipType = MaterialChipType.Normal;
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
    /// The backing store for the <see cref="MaterialChipType">MaterialChipType</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialChipType), typeof(MaterialChip), defaultValue: DefaultChipType);

    /// <summary>
    /// The backing store for the <see cref="Command">Command</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialChip));
    
    /// <summary>
    /// The backing store for the <see cref="CommandParameter">CommandParameter</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialChip));
    
    /// <summary>
    /// Gets or sets the state when the Chips is selected.
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(MaterialChip), defaultValue: DefaultIsSelected, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChip self)
        {
            self.UpdatePadding();
            self.SetState();
        }
    });

    /// <summary>
    /// Gets or sets the state when the Chips is selected.
    /// bindable property.
    /// </summary>
    public static readonly new BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialChip), defaultValue: DefaultIsEnabled, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChip self)
        {
            self.SetState();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Padding">Padding</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialChip), defaultValue: DefaultPadding);

    /// <summary>
    /// The backing store for the <see cref="CornerRadius">CornerRadius</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialChip), defaultValue: DefaultCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="LeadingIcon">LeadingIcon</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(ImageSource), typeof(MaterialChip), defaultValue: DefaultLeadingIcon, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChip self)
        {
            self.UpdatePadding();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TrailingIcon">TrailingIcon</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconProperty = BindableProperty.Create(nameof(TrailingIcon), typeof(ImageSource), typeof(MaterialChip), defaultValue: DefaultTrailingIcon, propertyChanged: (bindable, _, _) =>
    {
        if (bindable is MaterialChip self)
        {
            self.UpdatePadding();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Text">Text</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialChip), defaultValue: DefaultText);

    /// <summary>
    /// The backing store for the text color
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialChip), defaultValueCreator: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="FontFamily">FontFamily</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialChip), defaultValueCreator: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="FontSize">FontSize</see> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialChip), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor">BackgroundColor</see> bindable property.
    /// </summary>
    public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialChip), defaultValueCreator: DefaultBackgroundColor);

    /// <summary>
    /// The backing store for the <see cref="BorderWidth">BorderWidth</see> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialChip), defaultValue: DefaultBorderWidth);

    /// <summary>
    /// The backing store for the <see cref="BorderColor">BorderColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialChip), defaultValueCreator: DefaultBorderColor);

    /// <summary>
    /// The backing store for the <see cref="TouchAnimationType">TouchAnimationType</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TouchAnimationTypeProperty = BindableProperty.Create(nameof(TouchAnimationType), typeof(TouchAnimationTypes), typeof(MaterialChip), defaultValueCreator: DefaultTouchAnimationType);

    /// <summary>
    /// The backing store for the <see cref="TouchAnimation">TouchAnimation</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TouchAnimationProperty = BindableProperty.Create(nameof(TouchAnimation), typeof(ITouchAnimation), typeof(MaterialChip));

    /// <summary>
    /// The backing store for the <see cref="LeadingIconTintColor">LeadingIconTintColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconTintColorProperty = BindableProperty.Create(nameof(LeadingIconTintColor), typeof(Color), typeof(MaterialChip), defaultValueCreator: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="ApplyLeadingIconTintColor">ApplyLeadingIconTintColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ApplyLeadingIconTintColorProperty = BindableProperty.Create(nameof(ApplyLeadingIconTintColor), typeof(bool), typeof(MaterialChip), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="TrailingIconTintColor">TrailingIconTintColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconTintColorProperty = BindableProperty.Create(nameof(TrailingIconTintColor), typeof(Color), typeof(MaterialChip), defaultValueCreator: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="ApplyTrailingIconTintColor">ApplyTrailingIconTintColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ApplyTrailingIconTintColorProperty = BindableProperty.Create(nameof(ApplyTrailingIconTintColor), typeof(bool), typeof(MaterialChip), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="ShadowColor">ShadowColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(MaterialChip), defaultValueCreator: DefaultShadowColor);

    /// <summary>
    /// The backing store for the <see cref="Shadow">Shadow</see> bindable property.
    /// </summary>
    public static readonly new BindableProperty ShadowProperty = BindableProperty.Create(nameof(Shadow), typeof(Shadow), typeof(MaterialChip), defaultValueCreator: DefaultShadow);
    
    /// <summary>
    /// The backing store for the <see cref="Value">Value</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(object), typeof(MaterialChip), defaultValue: null, propertyChanged: (bindableObject, oldValue, newValue) => 
    { 
        if(bindableObject is MaterialChip self)
        {
            self.GroupableViewPropertyChanged?.Invoke(self, new GroupableViewPropertyChangedEventArgs(nameof(Value), oldValue, newValue));
        }
    });
    
    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets Chip type.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialChipType.Normal">MaterialChipType.Normal</see>
    /// </default>
    /// <remarks>
    /// <para>Normal: Help narrow a user’s intent by presenting dynamically generated suggestions, such as possible responses.</para>
    /// <para>Filter: Use tags or descriptive words to filter content. They can be a good alternative to segmented buttons or checkboxes when viewing a list or search results.</para>
    /// </remarks>
    public MaterialChipType Type
    {
        get => (MaterialChipType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="ICommand">command</see> to invoke when Chip is activated.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
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
    /// Gets or sets if Chip is selected (Filter type only).
    /// Inherited from <see cref="IGroupableView.IsSelected">IGroupableView.IsSelected</see>.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="false">False</see>
    /// </default>
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    /// <summary>
    /// Gets or sets if Chip is enabled.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="true">True</see>
    /// </default>
    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Thickness">padding</see> for Chip.
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
    /// Gets or sets the <see cref="CornerRadius">corner radius</see> for Chip.
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
    /// Gets or sets a leading icon for Chip.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public ImageSource LeadingIcon
    {
        get => (ImageSource)GetValue(LeadingIconProperty);
        set => SetValue(LeadingIconProperty, value);
    }

    /// <summary>
    /// Gets or sets a trailing icon for Chip.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public ImageSource TrailingIcon
    {
        get => (ImageSource)GetValue(TrailingIconProperty);
        set => SetValue(TrailingIconProperty, value);
    }

    /// <summary>
    /// Gets or sets text for Chip.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="string.Empty">string.Empty</see>
    /// </default>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set
        {
            SetValue(TextProperty, value);
            OnPropertyChanged(nameof(Value));
        } 
    }

    /// <summary>
    /// Gets or sets text <see cref="Color">color</see> for Chip.
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
    /// Gets or sets a font family for Chip.
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
    /// Gets or sets font size for Chip.
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
    /// Gets or sets a background <see cref="Color">color</see> for Chip.
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
    /// Gets or sets border width for Chip in device-independent units.
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
    /// Gets or sets stroke <see cref="Color">color</see> for Chip.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Outline">MaterialLightTheme.Outline</see> - Dark = <see cref="MaterialDarkTheme.Outline">MaterialDarkTheme.Outline</see>
    /// </default>
    /// <remarks>
    /// <para>This property has no effect if <see cref="IBorderElement.BorderWidth">IBorderElement.BorderWidth</see> is set to 0.</para>
    /// <para>On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor">VisualElement.BackgroundColor</see> is set to a non-default color.</para>
    /// </remarks>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets tint <see cref="Color">color</see> for Chip's leading icon.
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
    /// Gets or sets if leading icon should use <see cref="LeadingIconTintColor">tint color</see> or not.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="true">True</see>
    /// </default>
    public bool ApplyLeadingIconTintColor
    {
        get => (bool)GetValue(ApplyLeadingIconTintColorProperty);
        set => SetValue(ApplyLeadingIconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets tint <see cref="Color">color</see> for Chip's trailing icon.
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
    /// Gets or sets if trailing icon should use <see cref="TrailingIconTintColor">tint color</see> or not.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="true">True</see>
    /// </default>
    public bool ApplyTrailingIconTintColor
    {
        get => (bool)GetValue(ApplyTrailingIconTintColorProperty);
        set => SetValue(ApplyTrailingIconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets shadow <see cref="Color">color</see> for Chip.
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
    /// Gets or sets an animation to be executed when Chip is activated.
    /// Inherited from <see cref="ITouchableView.TouchAnimationType">ITouchableView.TouchAnimationType</see>.
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
    /// Gets or sets a custom animation to be executed when Chip is activated.
    /// Inherited from <see cref="ITouchableView.TouchAnimation">ITouchableView.TouchAnimation</see>.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public ITouchAnimation TouchAnimation
    {
        get => (ITouchAnimation)GetValue(TouchAnimationProperty);
        set => SetValue(TouchAnimationProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a value for Chip.
    /// Inherited from <see cref="IGroupableView.Value">IGroupableView.Value</see>.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialChip.Text">MaterialChip.Text</see>
    /// </default>
    /// <remarks>If a value is not explicitly set, the control will use the <see cref="MaterialChip.Text">Text</see> property if set or the <see cref="MaterialChip.Id">Id</see> property as its default.</remarks>
    public object Value
    {
        get => GetValue(ValueProperty) ?? (!string.IsNullOrEmpty(Text) ? Text : Id);
        set => SetValue(ValueProperty, value);
    }
    
    #endregion Properties

    #region Events

    private EventHandler? _clicked;
    private readonly Lock _objectLock = new();

    /// <inheritdoc />
    public event EventHandler<GroupableViewPropertyChangedEventArgs>? GroupableViewPropertyChanged;
    
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

    /// <inheritdoc />
    public async void OnTouch(TouchEventType gestureType)
    {
        await TouchAnimationManager.AnimateAsync(this, gestureType);

        if (gestureType == TouchEventType.Released)
        {
            if (Command != null)
            {
                if (CommandParameter != null && Command.CanExecute(CommandParameter))
                {
                    Command.Execute(CommandParameter);
                }
                else if (CommandParameter == null && Command.CanExecute(IsSelected))
                {
                    Command.Execute(IsSelected);
                }
            }
            
            _clicked?.Invoke(this, new IsSelectedEventArgs(IsSelected));
        }
    }

    protected virtual void InternalPressedHandler(object? sender, EventArgs e)
    {
        if (!IsEnabled) return;
        OnTouch(TouchEventType.Pressed);
    }

    protected virtual void InternalReleasedHandler(object? sender, EventArgs e)
    {
        if (!IsEnabled) return;
        
        if (Type == MaterialChipType.Normal)
        {
            VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Normal);
        }
        else if (GroupableViewPropertyChanged != null)
        {
            GroupableViewPropertyChanged.Invoke(this, new GroupableViewPropertyChangedEventArgs(nameof(IsSelected), IsSelected, !IsSelected));
        }
        else
        {
            IsSelected = !IsSelected;
        }

        OnTouch(TouchEventType.Released);
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

    public MaterialChip()
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
    
    #endregion Methods

    #region Setters

    private void SetState()
    {
        if (!IsEnabled)
        {
            VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Disabled);
            return;
        }
        
        VisualStateManager.GoToState(this, IsSelected ? VisualStateManager.CommonStates.Selected : VisualStateManager.CommonStates.Normal);
    }

    private void UpdatePadding()
    {
        var hasLeadingIcon = LeadingIcon is { IsEmpty: false };
        var hasTrailingIcon = TrailingIcon is { IsEmpty: false };

        _container.Padding = new Thickness(hasLeadingIcon ? 8 : 16, 0, hasTrailingIcon ? 8 : 16, 0);
        _leadingIcon.IsVisible = hasLeadingIcon;
        _trailingIcon.IsVisible = hasTrailingIcon;
    }

    #endregion Setters

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabled = new VisualState { Name = VisualStateManager.CommonStates.Disabled };

        disabled.Setters.Add(
            MaterialChip.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        disabled.Setters.Add(
            MaterialChip.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        disabled.Setters.Add(
            MaterialChip.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(
            MaterialChip.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(
            MaterialChip.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabled.Setters.Add(
            MaterialChip.ShadowColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Shadow,
                Dark = MaterialDarkTheme.Shadow
            }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

        var normal = new VisualState { Name = VisualStateManager.CommonStates.Normal };

        normal.Setters.Add(
            MaterialChip.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Outline,
                Dark = MaterialDarkTheme.Outline
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialChip.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SurfaceContainerLow,
                Dark = MaterialDarkTheme.SurfaceContainerLow
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialChip.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialChip.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialChip.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Primary,
                Dark = MaterialDarkTheme.Primary
            }
            .GetValueForCurrentTheme<Color>());

        normal.Setters.Add(
            MaterialChip.ShadowColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Shadow,
                Dark = MaterialDarkTheme.Shadow
            }
                .GetValueForCurrentTheme<Color>());

        var selected = new VisualState { Name = VisualStateManager.CommonStates.Selected };

        selected.Setters.Add(
            MaterialChip.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SecondaryContainer,
                Dark = MaterialDarkTheme.SecondaryContainer
            }
                .GetValueForCurrentTheme<Color>());

        selected.Setters.Add(
            MaterialChip.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SecondaryContainer,
                Dark = MaterialDarkTheme.SecondaryContainer
            }
                .GetValueForCurrentTheme<Color>());

        selected.Setters.Add(
            MaterialChip.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSecondaryContainer,
                Dark = MaterialDarkTheme.OnSecondaryContainer
            }
                .GetValueForCurrentTheme<Color>());

        selected.Setters.Add(
            MaterialChip.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSecondaryContainer,
                Dark = MaterialDarkTheme.OnSecondaryContainer
            }
                .GetValueForCurrentTheme<Color>());

        selected.Setters.Add(
            MaterialChip.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSecondaryContainer,
                Dark = MaterialDarkTheme.OnSecondaryContainer
            }
                .GetValueForCurrentTheme<Color>());

        selected.Setters.Add(
            MaterialChip.ShadowColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Shadow,
                Dark = MaterialDarkTheme.Shadow
            }
                .GetValueForCurrentTheme<Color>());

        commonStatesGroup.States.Add(disabled);
        commonStatesGroup.States.Add(normal);
        commonStatesGroup.States.Add(selected);

        var style = new Style(typeof(MaterialChip));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }

    #endregion Styles
}