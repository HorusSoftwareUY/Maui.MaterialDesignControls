
namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialInputType
{
    Filled, Outlined
}

public abstract partial class MaterialInputBase : ContentView
{
    #region Attributes

    private readonly static MaterialInputType DefaultInputType = MaterialInputType.Filled;
    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultTintColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    //private readonly static string DefaultFontFamily = MaterialFontFamily.Medium;
    //private readonly static double DefaultFontSize = MaterialFontSize.LabelLarge;
    private readonly static Brush DefaultBackground = Entry.BackgroundProperty.DefaultValue as Brush;
    private readonly static Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerHighest, Dark = MaterialDarkTheme.SurfaceContainerHighest }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultBorderWidth = 1;
    private readonly static Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private readonly static CornerRadius DefaultCornerRadius = new(4, 4, 0, 0);
    //private readonly static double DefaultHeightRequest = 40;
    //private readonly static Thickness DefaultPadding = new(24, 0);
    //private readonly static Thickness DefaultLeftIconPadding = new(16, 0, 24, 0);
    //private readonly static Thickness DefaultRightIconPadding = new(24, 0, 16, 0);
    //private readonly static AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
    //private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
    //private readonly static Color DefaultBusyIndicatorColor = MaterialLightTheme.Primary;
    //private readonly static double DefaultBusyIndicatorSize = 24;
    //private readonly static Shadow DefaultShadow = null;

    //private readonly Dictionary<MaterialInputBaseType, object> _backgroundColors = new()
    //{
    //    { MaterialInputBaseType.Elevated, new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow } },
    //    { MaterialInputBaseType.Filled, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } },
    //    { MaterialInputBaseType.Tonal, new AppThemeBindingExtension { Light = MaterialLightTheme.SecondaryContainer, Dark = MaterialDarkTheme.SecondaryContainer } },
    //    { MaterialInputBaseType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } }
    //};

    //private readonly Dictionary<MaterialInputBaseType, object> _textColors = new()
    //{
    //    { MaterialInputBaseType.Elevated, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } },
    //    { MaterialInputBaseType.Filled, new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimary, Dark = MaterialDarkTheme.OnPrimary } },
    //    { MaterialInputBaseType.Tonal, new AppThemeBindingExtension { Light = MaterialLightTheme.OnSecondaryContainer, Dark = MaterialDarkTheme.OnSecondaryContainer } },
    //    { MaterialInputBaseType.Outlined, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } },
    //    { MaterialInputBaseType.Text, new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary } },
    //    { MaterialInputBaseType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.OnPrimary, Dark = MaterialDarkTheme.OnPrimary } }
    //};

    //private readonly Dictionary<MaterialInputBaseType, Shadow> _shadows = new()
    //{
    //    { MaterialInputBaseType.Elevated, MaterialElevation.Level1 },
    //    { MaterialInputBaseType.Custom, MaterialElevation.Level1 }
    //};

    //private readonly Dictionary<MaterialInputBaseType, object> _borderColors = new()
    //{
    //    { MaterialInputBaseType.Outlined, new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline } },
    //    { MaterialInputBaseType.Custom, new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline } }
    //};

    //private readonly Dictionary<MaterialInputBaseType, double> _borderWidths = new()
    //{
    //    { MaterialInputBaseType.Outlined, 1 },
    //    { MaterialInputBaseType.Custom, 1 }
    //};

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Type" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialInputType), typeof(MaterialInputBase), defaultValue: DefaultInputType, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialInputBase self)
        {
            self.SetTemplate(self.Type);
        }
    });

    ///// <summary>
    ///// The backing store for the <see cref="Command" /> bindable property.
    ///// </summary>
    //public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialInputBase));

    ///// <summary>
    ///// The backing store for the <see cref="CommandParameter" /> bindable property.
    ///// </summary>
    //public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialInputBase));

    ///// <summary>
    ///// The backing store for the <see cref="ContentLayout" /> bindable property.
    ///// </summary>
    //public static readonly BindableProperty ContentLayoutProperty = BindableProperty.Create(nameof(ContentLayout), typeof(ButtonContentLayout), typeof(MaterialInputBase), defaultValue: DefaultContentLayout, propertyChanged: (bindable, oldValue, newValue) =>
    //{
    //    if (bindable is MaterialInputBase self)
    //    {
    //        if ((newValue == null && oldValue != null) ||
    //            (newValue is ButtonContentLayout newLayout && (oldValue == null || (oldValue is ButtonContentLayout oldLayout && !oldLayout.Equals(newLayout)))))
    //        {
    //            self.UpdatePadding();
    //        }
    //    }
    //});

    /// <summary>
    /// The backing store for the <see cref="Label" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="Placeholder" /> bindable property.
    /// </summary>
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="SupportingText" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(MaterialInputBase));

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialInputBase), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// The backing store for the <see cref="TextColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultTextColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetTextColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="IconTintColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IconTintColorProperty = BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultTintColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetTintColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="TintColor" /> bindable property.
    /// </summary>
    internal static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultTintColor);

    ///// <summary>
    ///// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    ///// </summary>
    //public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialInputBase), Button.CharacterSpacingProperty.DefaultValue);

    ///// <summary>
    ///// The backing store for the <see cref="FontFamily" /> bindable property.
    ///// </summary>
    //public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialInputBase), defaultValue: DefaultFontFamily);

    ///// <summary>
    ///// The backing store for the <see cref="FontSize" /> bindable property.
    ///// </summary>
    //public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultFontSize);

    ///// <summary>
    ///// The backing store for the <see cref="TextTransform" /> bindable property.
    ///// </summary>
    //public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(MaterialInputBase), defaultValue: Button.TextTransformProperty.DefaultValue);

    ///// <summary>
    ///// The backing store for the <see cref="FontAttributes" /> bindable property.
    ///// </summary>
    //public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialInputBase), defaultValue: Button.FontAttributesProperty.DefaultValue);

    ///// <summary>
    ///// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    ///// </summary>
    //public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialInputBase), defaultValue: Button.FontAutoScalingEnabledProperty.DefaultValue);

    /// <summary>
    /// The backing store for the <see cref="Background" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(MaterialInputBase), defaultValue: DefaultBackground, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetBackground(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetBackgroundColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderWidth"/> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultBorderWidth, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetBorderWidth(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="BorderColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultBorderColor, propertyChanged: (bindable, o, n) =>
    {
        if (bindable is MaterialInputBase self)
        {
            //self.SetBorderColor(self.Type);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="CornerRadius"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialInputBase), defaultValue: DefaultCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="LeadingIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LeadingIconSourceProperty = BindableProperty.Create(nameof(LeadingIconSource), typeof(ImageSource), typeof(MaterialInputBase), propertyChanged: (bindable, oldValue, newValue) =>
    {
        //if (bindable is MaterialInputBase self)
        //{
        //    if ((newValue == null && oldValue != null) ||
        //        (newValue is ImageSource newImage && (oldValue == null || (oldValue is ImageSource oldImage && !oldImage.Equals(newImage)))))
        //    {
        //        self.UpdatePadding();
        //    }
        //}
    });

    /// <summary>
    /// The backing store for the <see cref="TrailingIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TrailingIconSourceProperty = BindableProperty.Create(nameof(TrailingIconSource), typeof(ImageSource), typeof(MaterialInputBase), propertyChanged: (bindable, oldValue, newValue) =>
    {
        //if (bindable is MaterialInputBase self)
        //{
        //    if ((newValue == null && oldValue != null) ||
        //        (newValue is ImageSource newImage && (oldValue == null || (oldValue is ImageSource oldImage && !oldImage.Equals(newImage)))))
        //    {
        //        self.UpdatePadding();
        //    }
        //}
    });

    ///// <summary>
    ///// The backing store for the <see cref="Padding" /> bindable property.
    ///// </summary>
    //public static new readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialInputBase), defaultValue: DefaultPadding, propertyChanged: (bindable, oldValue, newValue) =>
    //{
    //    if (bindable is MaterialInputBase self)
    //    {
    //        self.UpdatePadding();
    //    }
    //});

    ///// <summary>
    ///// The backing store for the <see cref="LineBreakMode"/> bindable property.
    ///// </summary>
    //public static readonly BindableProperty LineBreakModeProperty = BindableProperty.Create(nameof(LineBreakMode), typeof(LineBreakMode), typeof(MaterialInputBase), defaultValue: Button.LineBreakModeProperty.DefaultValue);

    ///// <summary>
    ///// The backing store for the <see cref="Animation"/> bindable property.
    ///// </summary>
    //public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialInputBase), defaultValue: DefaultAnimationType);

    ///// <summary>
    ///// The backing store for the <see cref="AnimationParameter"/> bindable property.
    ///// </summary>
    //public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialInputBase), defaultValue: DefaultAnimationParameter);

    ///// <summary>
    ///// The backing store for the <see cref="CustomAnimation"/> bindable property.
    ///// </summary>
    //public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialInputBase));

    ///// <summary>
    ///// The backing store for the <see cref="HeightRequest" /> bindable property.
    ///// </summary>
    //public static new readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultHeightRequest);

    ///// <summary>
    ///// The backing store for the <see cref="IsBusy"/> bindable property.
    ///// </summary>
    //public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(MaterialInputBase), defaultValue: false, propertyChanged: (bindable, _, newValue) =>
    //{
    //    if (bindable is MaterialInputBase self)
    //    {
    //        self._button.IsVisible = !(bool)newValue;
    //        self._internalActivityIndicator.IsVisible = !self._button.IsVisible;
    //    }
    //});

    ///// <summary>
    ///// The backing store for the <see cref="BusyIndicatorColor"/> bindable property.
    ///// </summary>
    //public static readonly BindableProperty BusyIndicatorColorProperty = BindableProperty.Create(nameof(BusyIndicatorColor), typeof(Color), typeof(MaterialInputBase), defaultValue: DefaultBusyIndicatorColor);

    ///// <summary>
    ///// The backing store for the <see cref="BusyIndicatorSize"/> bindable property.
    ///// </summary>
    //public static readonly BindableProperty BusyIndicatorSizeProperty = BindableProperty.Create(nameof(BusyIndicatorSize), typeof(double), typeof(MaterialInputBase), defaultValue: DefaultBusyIndicatorSize);

    ///// <summary>
    ///// The backing store for the <see cref="CustomBusyIndicator"/> bindable property.
    ///// </summary>
    //public static readonly BindableProperty CustomBusyIndicatorProperty = BindableProperty.Create(nameof(CustomBusyIndicator), typeof(View), typeof(MaterialInputBase), propertyChanged: (bindable, _, newValue) =>
    //{
    //    if (bindable is MaterialInputBase self)
    //    {
    //        if (self._mainLayout.Children.Count > 1)
    //        {
    //            self._mainLayout.Children.RemoveAt(1);

    //            self._internalActivityIndicator = newValue as View ?? self._activityIndicator;
    //            self._mainLayout.Add(self._internalActivityIndicator);
    //        }
    //    }
    //});

    ///// <summary>
    ///// The backing store for the <see cref="Shadow" /> bindable property.
    ///// </summary>
    //public static new readonly BindableProperty ShadowProperty = BindableProperty.Create(nameof(Shadow), typeof(Shadow), typeof(MaterialInputBase), defaultValue: DefaultShadow, propertyChanged: (bindable, o, n) =>
    //{
    //    if (bindable is MaterialInputBase self)
    //    {
    //        self.SetShadow(self.Type);
    //    }
    //});

    /// <summary>
    /// The backing store for the <see cref="IsFocused"/> bindable property.
    /// </summary>
    public static new readonly BindableProperty IsFocusedProperty = BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(MaterialInputBase), defaultValue: false);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the input type according to <see cref="MaterialInputType"/> enum.
    /// The default value is <see cref="MaterialInputBaseType.Filled"/>. This is a bindable property.
    /// </summary>
    public MaterialInputType Type
    {
        get => (MaterialInputType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    ///// <summary>
    ///// Gets or sets the command to invoke when the button is activated. This is a bindable property.
    ///// </summary>
    ///// <remarks>This property is used to associate a command with an instance of a button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel. <see cref="VisualElement.IsEnabled" /> is controlled by the <see cref="Command.CanExecute(object)"/> if set.</remarks>
    //public ICommand Command
    //{
    //    get => (ICommand)GetValue(CommandProperty);
    //    set => SetValue(CommandProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets the parameter to pass to the <see cref="Command"/> property.
    ///// The default value is <see langword="null"/>. This is a bindable property.
    ///// </summary>
    //public object CommandParameter
    //{
    //    get => GetValue(CommandParameterProperty);
    //    set => SetValue(CommandParameterProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets an object that controls the position of the button image and the spacing between the button's image and the button's text.
    ///// This is a bindable property.
    ///// </summary>
    //public ButtonContentLayout ContentLayout
    //{
    //    get => (ButtonContentLayout)GetValue(ContentLayoutProperty);
    //    set => SetValue(ContentLayoutProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets the padding for the button. This is a bindable property.
    ///// </summary>
    //public new Thickness Padding
    //{
    //    get => (Thickness)GetValue(PaddingProperty);
    //    set => SetValue(PaddingProperty, value);
    //}

    ///// <summary>
    ///// Determines how <see cref="Text"/> is shown when the length is overflowing the size of this button.
    ///// This is a bindable property.
    ///// </summary>
    //public LineBreakMode LineBreakMode
    //{
    //    get => (LineBreakMode)GetValue(LineBreakModeProperty);
    //    set => SetValue(LineBreakModeProperty, value);
    //}

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> that describes the background of the button. This is a bindable property.
    /// </summary>
    public new Brush Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the background color of the button. This is a bindable property.
    /// </summary>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the border stroke color of the button. This is a bindable property.
    /// </summary>
    /// <remarks>This property has no effect if <see cref="IBorderElement.BorderWidth" /> is set to 0. On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor" /> is set to a non-default color.</remarks>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the corner radius for the button, in device-independent units. This is a bindable property.
    /// </summary>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the border, in device-independent units. This is a bindable property.
    /// </summary>
    /// <remarks>Set this value to a non-zero value in order to have a visible border.</remarks>
    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image on the Button. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public ImageSource LeadingIconSource
    {
        get => (ImageSource)GetValue(LeadingIconSourceProperty);
        set => SetValue(LeadingIconSourceProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image on the Button. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public ImageSource TrailingIconSource
    {
        get => (ImageSource)GetValue(TrailingIconSourceProperty);
        set => SetValue(TrailingIconSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the content of the button.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    /// <remarks>Changing the text of a button will trigger a layout cycle.</remarks>
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the content of the button.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    /// <remarks>Changing the text of a button will trigger a layout cycle.</remarks>
    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the content of the button.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    /// <remarks>Changing the text of a button will trigger a layout cycle.</remarks>
    public string SupportingText
    {
        get => (string)GetValue(SupportingTextProperty);
        set => SetValue(SupportingTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the text displayed as the content of the button.
    /// The default value is <see langword="null"/>. This is a bindable property.
    /// </summary>
    /// <remarks>Changing the text of a button will trigger a layout cycle.</remarks>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button. This is a bindable property.
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button. This is a bindable property.
    /// </summary>
    public Color? IconTintColor
    {
        get => (Color?)GetValue(IconTintColorProperty);
        set => SetValue(IconTintColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the text of the button. This is a bindable property.
    /// </summary>
    public Color? TintColor
    {
        get => (Color?)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    ///// <summary>
    ///// Gets or sets the spacing between each of the characters of <see cref="Text"/> when displayed on the button.
    ///// This is a bindable property.
    ///// </summary>
    //public double CharacterSpacing
    //{
    //    get => (double)GetValue(CharacterSpacingProperty);
    //    set => SetValue(CharacterSpacingProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets a value that indicates whether the font for the text of this button is bold, italic, or neither.
    ///// This is a bindable property.
    ///// </summary>
    //public FontAttributes FontAttributes
    //{
    //    get => (FontAttributes)GetValue(FontAttributesProperty);
    //    set => SetValue(FontAttributesProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets the font family for the text of this entry. This is a bindable property.
    ///// </summary>
    //public string FontFamily
    //{
    //    get => (string)GetValue(FontFamilyProperty);
    //    set => SetValue(FontFamilyProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets the size of the font for the text of this entry. This is a bindable property.
    ///// </summary>
    //[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
    //public double FontSize
    //{
    //    get => (double)GetValue(FontSizeProperty);
    //    set => SetValue(FontSizeProperty, value);
    //}

    ///// <summary>
    ///// Determines whether or not the font of this entry should scale automatically according to the operating system settings. Default value is <see langword="true"/>.
    ///// This is a bindable property.
    ///// </summary>
    ///// <remarks>Typically this should always be enabled for accessibility reasons.</remarks>
    //public bool FontAutoScalingEnabled
    //{
    //    get => (bool)GetValue(FontAutoScalingEnabledProperty);
    //    set => SetValue(FontAutoScalingEnabledProperty, value);
    //}

    ///// <summary>
    ///// Applies text transformation to the <see cref="Text"/> displayed on this button.
    ///// This is a bindable property.
    ///// </summary>
    //public TextTransform TextTransform
    //{
    //    get => (TextTransform)GetValue(TextTransformProperty);
    //    set => SetValue(TextTransformProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets an animation to be executed when button is clicked.
    ///// The default value is <see cref="AnimationTypes.Fade"/>.
    ///// This is a bindable property.
    ///// </summary>
    //public AnimationTypes Animation
    //{
    //    get => (AnimationTypes)GetValue(AnimationProperty);
    //    set => SetValue(AnimationProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets the parameter to pass to the <see cref="Animation"/> property.
    ///// The default value is <see langword="null"/>.
    ///// This is a bindable property.
    ///// </summary>
    //public double? AnimationParameter
    //{
    //    get => (double?)GetValue(AnimationParameterProperty);
    //    set => SetValue(AnimationParameterProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets a custom animation to be executed when button is clicked.
    ///// The default value is <see langword="null"/>.
    ///// This is a bindable property.
    ///// </summary>
    //public ICustomAnimation CustomAnimation
    //{
    //    get => (ICustomAnimation)GetValue(CustomAnimationProperty);
    //    set => SetValue(CustomAnimationProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets the desired height override of this element. This is a bindable property.
    ///// </summary>
    ///// <remarks>
    ///// <para>The default value is -1, which means the value is unset; the effective minimum height will be zero.</para>
    ///// <para><see cref="HeightRequest"/> does not immediately change the <see cref="Bounds"/> of an element; setting the <see cref="HeightRequest"/> will change the resulting height of the element during the next layout pass.</para>
    ///// </remarks>
    //public new double HeightRequest
    //{
    //    get => (double)GetValue(HeightRequestProperty);
    //    set => SetValue(HeightRequestProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets if button is on busy state (executing Command).
    ///// The default value is <see langword="false"/>.
    ///// This is a bindable property.
    ///// </summary>
    //public bool IsBusy
    //{
    //    get => (bool)GetValue(IsBusyProperty);
    //    set => SetValue(IsBusyProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets the <see cref="Color" /> for the busy indicator.
    ///// This is a bindable property.
    ///// </summary>
    //public Color BusyIndicatorColor
    //{
    //    get => (Color)GetValue(BusyIndicatorColorProperty);
    //    set => SetValue(BusyIndicatorColorProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets the size for the busy indicator.
    ///// This is a bindable property.
    ///// </summary>
    //public double BusyIndicatorSize
    //{
    //    get => (double)GetValue(BusyIndicatorSizeProperty);
    //    set => SetValue(BusyIndicatorSizeProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets a custom <see cref="View" /> for busy indicator.
    ///// This is a bindable property.
    ///// </summary>
    //public View CustomBusyIndicator
    //{
    //    get => (View)GetValue(CustomBusyIndicatorProperty);
    //    set => SetValue(CustomBusyIndicatorProperty, value);
    //}

    ///// <summary>
    ///// Gets or sets the shadow effect cast by the element. This is a bindable property.
    ///// </summary>
    //public new Shadow Shadow
    //{
    //    get { return (Shadow)GetValue(ShadowProperty); }
    //    set { SetValue(ShadowProperty, value); }
    //}

    /// <summary>
    /// Gets or sets the shadow effect cast by the element. This is a bindable property.
    /// </summary>
    public new bool IsFocused
    {
        get => (bool)GetValue(IsFocusedProperty);
        set => SetValue(IsFocusedProperty, value);
    }

    #endregion Properties

    #region Layout

    //IMaterialInputBase _content;

    #endregion

    public MaterialInputBase()
    {
        InitializeComponent();

        CreateLayout();
        if (Type == DefaultInputType)
        {
            UpdateLayoutAfterTypeChanged(Type);
        }
    }

    private void CreateLayout()
    {
        //Content = CreateView();
    }

    protected abstract View CreateView();

    private void UpdateLayoutAfterTypeChanged(MaterialInputType type)
    {
        SetTemplate(type);
        //SetBackground(type);
        //SetBackgroundColor(type);
        //SetTextColor(type);
        //SetTintColor(type);
        //SetBorderColor(type);
        //SetBorderWidth(type);
        //SetShadow(type);
    }

    private void SetTemplate(MaterialInputType type)
    {
        if (type == MaterialInputType.Filled && Resources.TryGetValue("FilledControlTemplate", out object filledTemplate) && filledTemplate is ControlTemplate filledControlTemplate)
        {
            ControlTemplate = filledControlTemplate;
        }
        else if (type == MaterialInputType.Outlined && Resources.TryGetValue("OutlinedControlTemplate", out object outlinedTemplate) && outlinedTemplate is ControlTemplate outlinedControlTemplate)
        {
            ControlTemplate = outlinedControlTemplate;
        }
    }
}