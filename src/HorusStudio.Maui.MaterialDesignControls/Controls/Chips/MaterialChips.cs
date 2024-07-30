using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls.Shapes;

namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialChipsType
{
    /// <summary>Filter chips</summary>
    Filter,
    /// <summary>Normal chips</summary>
    Normal
}

/// <summary>
/// A Chips help people enter information, make selections, filter content, or trigger actions <see href="https://m3.material.io/components/chips/overview">see here.</see>
/// </summary>
public class MaterialChips : ContentView, ITouchable
{

    #region Attributes

    private readonly static MaterialChipsType DefaultChipsType = MaterialChipsType.Normal;
    private readonly static bool DefaultIsSelected = false;
    private readonly static CornerRadius DefaultCornerRadius = new CornerRadius(8);
    private readonly static Thickness DefaultPadding = new Thickness(16, 0);
    private readonly static AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
    private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
    private readonly static ImageSource DefaultLeandingIcon = null;
    private readonly static ImageSource DefaultTrilingIcon = null;
    private readonly static Color DefaultIconTintColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialLightTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private readonly static string DefaultText = string.Empty;
    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static double DefaultFontSize = MaterialFontSize.LabelLarge;
    private readonly static Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultBorderWidth = 1;
    private readonly static Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
    private readonly static Shadow DefaultShadow = MaterialElevation.Level1;
    private readonly static Color DefaultShadowColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Shadow, Dark = MaterialDarkTheme.Shadow }.GetValueForCurrentTheme<Color>();

    #endregion

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="MaterialChipsType" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialChipsType), typeof(MaterialChips), defaultValue: DefaultChipsType, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialChips self)
        {
            self.SetType(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Command" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialChips));
    
    /// <summary>
    /// The backing store for the <see cref="CommandParameter" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialChips));
    
    /// <summary>
    /// Gets or sets the state when the Chips is selected.
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(MaterialChips), defaultValue: DefaultIsSelected);
    
    /// <summary>
    /// The backing store for the <see cref="Padding" />
    /// bindable property.
    /// </summary>
    public static new readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialChips), defaultValue: DefaultPadding);
    
    /// <summary>
    /// The backing store for the <see cref="CornerRadius" />
    /// bindable property.
    /// </summary>
    public static new readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialChips), defaultValue: DefaultCornerRadius);
    
    /// <summary>
    /// The backing store for the <see cref="LeadingIcon" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(ImageSource), typeof(MaterialChips), defaultValue: DefaultLeandingIcon, propertyChanged: (bindable, oldValue, newValue) => 
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
    public static readonly BindableProperty TrailingIconProperty = BindableProperty.Create(nameof(TrailingIcon), typeof(ImageSource), typeof(MaterialChips), defaultValue: DefaultTrilingIcon, propertyChanged: (bindable, oldValue, newValue) => 
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
    public static new readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialChips), defaultValue: DefaultText);
    
    /// <summary>
    /// The backing store for the text color
    /// bindable property.
    /// </summary>
    public static new readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialChips), defaultValue: DefaultTextColor);
    
    /// <summary>
    /// The backing store for the <see cref="FontFamily" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialChips), defaultValue: DefaultFontFamily);
    
    /// <summary>
    /// The backing store for the <see cref="FontSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialChips), defaultValue: DefaultFontSize);
    
    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" />
    /// bindable property.
    /// </summary>
    public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialChips), defaultValue: DefaultBackgroundColor);
    
    /// <summary>
    /// The backing store for the <see cref="BorderWidth"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialChips), defaultValue: DefaultBorderWidth);

    /// <summary>
    /// The backing store for the <see cref="BorderColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialChips), defaultValue: DefaultBorderColor);
    
    /// <summary>
    /// The backing store for the <see cref="Animation"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialChips), defaultValue: DefaultAnimationType);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialChips), defaultValue: DefaultAnimationParameter);
    
    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/>
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialChips));
    
    /// <summary>
    /// The backing store for the <see cref="LeadingIconTintColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconTintColorProperty = BindableProperty.Create(nameof(LeadingIconTintColor), typeof(Color), typeof(MaterialChips), defaultValue: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="TrailingIconTintColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconTintColorProperty = BindableProperty.Create(nameof(TrailingIconTintColor), typeof(Color), typeof(MaterialChips), defaultValue: DefaultIconTintColor);

    /// <summary>
    /// The backing store for the <see cref="ShadowColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(MaterialChips), defaultValue: DefaultShadowColor, propertyChanged: (bindable, oldValue, newValue) =>
    {

        if (bindable is MaterialChips self)
        {
            self.setShadowColor(self.Type);
        }

    });

    /// <summary>
    /// The backing store for the <see cref="Shadow" />
    /// bindable property.
    /// </summary>
    public static new readonly BindableProperty ShadowProperty = BindableProperty.Create(nameof(Shadow), typeof(Shadow), typeof(MaterialChips), defaultValue: DefaultShadow, propertyChanged: (bindable, oldValue, newValue) =>
    {

        if (bindable is MaterialChips self)
        {

        }

    });

    #endregion

    #region Properties


    /// <summary>
    /// Gets or sets type Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialChipsType.Normal">MaterialChipsType.Normal</see>
    /// </default>
    /// <remarks>
    /// <para>Normal chips help narrow a user’s intent by presenting dynamically generated suggestions, such as possible responses or search filters.</para>
    /// <para>Filter chips use tags or descriptive words to filter content. They can be a good alternative to segmented buttons or checkboxes when viewing a list or search results.</para>
    /// </remarks>
    public MaterialChipsType Type
    {
        get => (MaterialChipsType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the command to invoke when the Chips is activated.
    /// This is a bindable property.
    /// </summary>
    /// <remarks> This property is used to associate a command with an instance of Chips. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
    /// <para><see cref="VisualElement.IsEnabled"> VisualElement.IsEnabled</see> is controlled by the <see cref="Command.CanExecute(object)">Command.CanExecute(object)</see> if set.</para>
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
    /// Gets or sets the state when the Chips is selected.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// False
    /// </default>
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
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
    /// Empty
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
    /// Empty
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
    /// Empty
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
    public Color BackgroundColor
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
    /// <para>On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor" /> is set to a non-default color.</para>
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
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark = <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color LeadingIconTintColor
    {
        get => (Color)GetValue(LeadingIconTintColorProperty);
        set => SetValue(LeadingIconTintColorProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a color that describes the trailing icon color of the chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark = <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color TrailingIconTintColor
    {
        get => (Color)GetValue(TrailingIconTintColorProperty);
        set => SetValue(TrailingIconTintColorProperty, value);
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
    /// MaterialElevation.Level1
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
    /// Occurs when the chips is clicked/tapped.
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
        
        if (gestureType == TouchType.Pressed)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }
    }
    
    protected virtual void InternalPressedHandler(object sender, EventArgs e)
    {
        if(!IsEnabled) return;
        
        IsSelected = !IsSelected;
        UpdatePadding();
        VisualStateManager.GoToState(this, (IsSelected) ? ChipsCommonStates.Selected: ChipsCommonStates.Unselected);
        OnTouch(TouchType.Pressed);
    }

    #endregion

    #region Layout

    private MaterialCard _container;
    private HorizontalStackLayout _hStack;
    private Label _textLabel;
    private Grid _gridLeading;
    private Grid _gridTrailing;
    private Image _leadingIcon;
    private Image _trailingIcon;

    #endregion

    #region Constructor

    public MaterialChips()
    {
        HorizontalOptions = LayoutOptions.Center;
        VerticalOptions = LayoutOptions.Center;
        base.Shadow = Shadow;
        
        _container = new MaterialCard()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            CornerRadius = CornerRadius,
            BackgroundColor = BackgroundColor,
            Padding = Padding,
            StrokeThickness = BorderWidth,
            Stroke = new SolidColorBrush(BorderColor),
            HeightRequest = HeightRequest,
            WidthRequest = WidthRequest,
            MinimumHeightRequest = 32,
        };
        
        _leadingIcon = new Image()
        {
            VerticalOptions = LayoutOptions.Center,
            Source = LeadingIcon,
            IsVisible = true,
            WidthRequest = 18,
            HeightRequest = 18,
        };
        
        var leadingIconTintColor = new IconTintColorBehavior();
        leadingIconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(LeadingIconTintColor), source: this));
        _leadingIcon.Behaviors.Add(leadingIconTintColor);
        
        _gridLeading = [_leadingIcon];
        _gridLeading.IsVisible = false;
        
        _textLabel = new Label()
        {
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Start,
            BackgroundColor = Colors.Transparent,
            Text = Text,
            Margin = new Thickness(1),
            TextColor = TextColor
        };
        
        _trailingIcon = new Image()
        {
            VerticalOptions = LayoutOptions.Center,
            Source = TrailingIcon,
            IsVisible = true,
            WidthRequest = 18,
            HeightRequest = 18
        };
        
        var trailingIconTintColor = new IconTintColorBehavior();
        trailingIconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(TrailingIconTintColor), source: this));
        _leadingIcon.Behaviors.Add(trailingIconTintColor);

        _gridTrailing = [_trailingIcon];
        _gridTrailing.IsVisible = false;

        _hStack = new HorizontalStackLayout()
        {
            _gridLeading,
            _textLabel,
            _gridTrailing
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
        
        _leadingIcon.SetBinding(Image.SourceProperty, new Binding(nameof(LeadingIcon), source: this));
        _trailingIcon.SetBinding(Image.SourceProperty, new Binding(nameof(TrailingIcon), source: this));

        _container.Clicked += InternalPressedHandler;

        _container.Content = _hStack;
        Content = _container;
        
        UpdatePadding();
    }

    #endregion

    #region Setters

    private void setShadowColor(MaterialChipsType type)
    {
        if (base.Shadow != null && !ShadowColor.Equals(DefaultShadowColor))
        {
            base.Shadow = new Shadow
            {
                Brush = ShadowColor,
                Radius = base.Shadow.Radius,
                Opacity = base.Shadow.Opacity,
                Offset = base.Shadow.Offset
            };
        }
    }

    private void SetType(MaterialChipsType type)
    {
        UpdatePadding();
        VisualStateManager.GoToState(this, ChipsCommonStates.Normal);
    }

    private void UpdatePadding()
    {

        if ((LeadingIcon != null && !LeadingIcon.IsEmpty) && (TrailingIcon != null && !TrailingIcon.IsEmpty))
        {
            
            if (Type == MaterialChipsType.Normal)
            {
                _gridLeading.IsVisible = true;
                _container.Padding = new Thickness(8, 0, 8, 0);
                _gridLeading.Padding = new Thickness(0, 0, 8, 0);
            }
            else
            {
                _container.Padding = (IsSelected) ? new Thickness(8, 0, 8, 0) : new Thickness(16,0,8, 0);
                _gridLeading.Padding = (IsSelected) ? new Thickness(0, 0, 8, 0) : new Thickness(0);
                _gridLeading.IsVisible = IsSelected;
            }
            
            _gridTrailing.Padding = new Thickness(8, 0, 0, 0);
            _gridTrailing.IsVisible = true;
            return;
        }
        
        if ((LeadingIcon != null && !LeadingIcon.IsEmpty) && !(TrailingIcon != null && TrailingIcon.IsEmpty))
        {
            
            if (Type == MaterialChipsType.Normal)
            {
                _gridLeading.IsVisible = true;
                _container.Padding = new Thickness(8, 0, 16, 0);
                _gridLeading.Padding = new Thickness(0, 0, 8, 0);
            }
            else
            {
                _container.Padding = (IsSelected) ? new Thickness(8, 0, 16, 0) : new Thickness(16, 0);
                _gridLeading.Padding = (IsSelected) ? new Thickness(0, 0, 8, 0) : new Thickness(0);
                _gridLeading.IsVisible = IsSelected;
            }

            _gridTrailing.Padding = new Thickness(0, 0, 0, 0);
            _gridTrailing.IsVisible = false;
            return;
        }
        
        if (!(LeadingIcon != null && LeadingIcon.IsEmpty) && (TrailingIcon != null && !TrailingIcon.IsEmpty))
        {
            _container.Padding = new Thickness(16, 0, 8, 0);
            _gridLeading.Padding = new Thickness(0, 0, 0, 0);
            _gridTrailing.Padding = new Thickness(8, 0, 0, 0);
            _gridLeading.IsVisible = false;
            _gridTrailing.IsVisible = true;
            return;
        }

        _container.Padding = new Thickness(16,0);
        _gridLeading.Padding = new Thickness(0, 0, 0, 0);
        _gridTrailing.Padding = new Thickness(0, 0, 0, 0);
        _gridLeading.IsVisible = false;
        _gridTrailing.IsVisible = false;
    }

    #endregion

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

        var enabled = new VisualState { Name = ChipsCommonStates.Normal };

        enabled.Setters.Add(
            MaterialChips.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Outline,
                Dark = MaterialDarkTheme.Outline
            }
            .GetValueForCurrentTheme<Color>());

        enabled.Setters.Add(
            MaterialChips.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SurfaceContainerLow,
                Dark = MaterialDarkTheme.SurfaceContainerLow
            }
            .GetValueForCurrentTheme<Color>());

        enabled.Setters.Add(
            MaterialChips.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        enabled.Setters.Add(
            MaterialChips.LeadingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
            }
            .GetValueForCurrentTheme<Color>());

        enabled.Setters.Add(
            MaterialChips.TrailingIconTintColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurfaceVariant,
                Dark = MaterialDarkTheme.OnSurfaceVariant
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
        

        commonStatesGroup.States.Add(disabled);
        commonStatesGroup.States.Add(enabled);
        commonStatesGroup.States.Add(unselected);
        commonStatesGroup.States.Add(selected);
        
        var style = new Style(typeof(MaterialChips));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }

    #endregion
    
}

public class ChipsCommonStates : VisualStateManager.CommonStates
{
    public const string Selected = "Selected";
    public const string Unselected = "Unselected";
}