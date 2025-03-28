namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Define <see cref="MaterialSnackbar" /> position on screen
/// </summary>
public enum MaterialSnackbarPosition
{
    /// <summary>
    /// Display at bottom of screen             
    /// </summary>
    Bottom,
    /// <summary>
    /// Display at top of screen
    /// </summary>
    Top
}

/// <summary>
/// User-defined configuration to display a <see cref="MaterialSnackbar" />
/// </summary>
/// <param name="message">Message to be displayed on snackbar</param>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSnackbar.gif</img>
///
/// <h3>C# sample</h3>
/// <code>
/// IMaterialSnackbar _snackbar = ...
/// var config = new MaterialSnackbarConfig("Lorem ipsum dolor sit amet")
/// {
///     LeadingIcon = new MaterialSnackbarConfig.IconConfig("info.png", () => Console.WriteLine("Leading icon tapped!"))),
///     TrailingIcon = new MaterialSnackbarConfig.IconConfig("ic_close.png", () => Console.WriteLine("Trailing icon tapped!")))
/// };
/// _snackbar.Show(config);
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SnackbarPage.xaml)
/// 
/// </example>
/// <todoList>
/// * Add FontFamily configuration
/// </todoList>
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public class MaterialSnackbarConfig(string message)
{
    #region Default values
    
    private static readonly Func<Color> DefaultBackgroundColorBuilder = () => new AppThemeBindingExtension { Light = MaterialLightTheme.InverseSurface, Dark = MaterialDarkTheme.InverseSurface }.GetValueForCurrentTheme<Color>();
    private static readonly Func<Color> DefaultTextColorBuilder = () => new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    private static readonly Func<double> DefaultFontSizeBuilder = () => MaterialFontSize.BodyMedium;
    private static readonly Func<Color> DefaultActionColorBuilder = () => new AppThemeBindingExtension { Light = MaterialLightTheme.InversePrimary, Dark = MaterialDarkTheme.InversePrimary }.GetValueForCurrentTheme<Color>();
    private static readonly Func<double> DefaultActionSizeBuilder = () => MaterialFontSize.BodyMedium;
    private static readonly Func<Color> DefaultIconColorBuilder = () => new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();

    private static Color? _defaultBackgroundColor;
    private static Color? _defaultTextColor;
    private static double? _defaultFontSize;
    private static Color? _defaultActionColor;
    private static double? _defaultActionSize;
    private static Color? _defaultIconColor;
    
    /// <summary>
    /// Margin to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>24</default>
    public static Thickness DefaultMargin { get; set; } = new(24);
    
    /// <summary>
    /// Padding to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>16</default>
    public static Thickness DefaultPadding { get; set; } = new(16);
    
    /// <summary>
    /// Position to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default><see cref="MaterialSnackbarPosition.Bottom">MaterialSnackbarPosition.Bottom</see></default>
    public static MaterialSnackbarPosition DefaultPosition { get; set; } = MaterialSnackbarPosition.Bottom;
    
    /// <summary>
    /// Icon size to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>24</default>
    public static int DefaultIconSize { get; set; } = 24;
    
    /// <summary>
    /// Corner radius to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>8</default>
    public static float DefaultCornerRadius { get; set; } = 8f;

    /// <summary>
    /// Background <see cref="Color"/> to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.InverseSurface">MaterialLightTheme.InverseSurface</see> - Dark: <see cref="MaterialDarkTheme.InverseSurface">MaterialDarkTheme.InverseSurface</see>
    /// </default>
    public static Color DefaultBackgroundColor
    {
        get => _defaultBackgroundColor ?? DefaultBackgroundColorBuilder();
        set => _defaultBackgroundColor = value;
    }
    
    /// <summary>
    /// Text <see cref="Color"/> to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.InverseOnSurface">MaterialLightTheme.InverseOnSurface</see> - Dark: <see cref="MaterialDarkTheme.InverseOnSurface">MaterialDarkTheme.InverseOnSurface</see>
    /// </default>
    public static Color DefaultTextColor
    {
        get => _defaultTextColor ?? DefaultTextColorBuilder();
        set => _defaultTextColor = value;
    }
    
    /// <summary>
    /// Text font size to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyMedium">MaterialFontSize.BodyMedium</see>
    /// </default>
    public static double DefaultFontSize 
    {
        get => _defaultFontSize ?? DefaultFontSizeBuilder();
        set => _defaultFontSize = value;
    }
    
    /// <summary>
    /// Action text <see cref="Color"/> to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.InversePrimary">MaterialLightTheme.InversePrimary</see> - Dark: <see cref="MaterialDarkTheme.InversePrimary">MaterialDarkTheme.InversePrimary</see>
    /// </default>
    public static Color DefaultActionColor 
    {
        get => _defaultActionColor ?? DefaultActionColorBuilder();
        set => _defaultActionColor = value;
    }
    
    /// <summary>
    /// Action font size to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyMedium">MaterialFontSize.BodyMedium</see>
    /// </default>
    public static double DefaultActionSize 
    {
        get => _defaultActionSize ?? DefaultActionSizeBuilder();
        set => _defaultActionSize = value;
    }
    
    /// <summary>
    /// Icon <see cref="Color"/> to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.InverseOnSurface">MaterialLightTheme.InverseOnSurface</see> - Dark: <see cref="MaterialDarkTheme.InverseOnSurface">MaterialDarkTheme.InverseOnSurface</see>
    /// </default>
    public static Color DefaultIconColor 
    {
        get => _defaultIconColor ?? DefaultIconColorBuilder();
        set => _defaultIconColor = value;
    }
    
    /// <summary>
    /// Duration applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>3 seconds</default>
    public static TimeSpan DefaultDuration { get; set; } = TimeSpan.FromSeconds(3);
    
    /// <summary>
    /// Spacing between components to be applied by default to every <see cref="MaterialSnackbar"/> that doesn't set one
    /// </summary>
    /// <default>16</default>
    public static int DefaultSpacing { get; set; } = 16;
    
    #endregion Default values
    
    #region Attributes
    
    private Color? _backgroundColor;
    private float? _cornerRadius;
    private double? _fontSize;
    private Color? _textColor;
    private Thickness? _margin;
    private Thickness? _padding;
    private MaterialSnackbarPosition? _position;
    private TimeSpan? _duration;
    private int? _spacing;
    
    #endregion Attributes
    
    #region Properties
    
    /// <summary>
    /// Gets text to be displayed on snackbar.
    /// </summary>
    public string Message { get; } = message;
    
    /// <summary>
    /// Gets or sets a color that describes the background color of snackbar.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.InverseSurface">MaterialLightTheme.InverseSurface</see> - Dark: <see cref="MaterialDarkTheme.InverseSurface">MaterialDarkTheme.InverseSurface</see>
    /// </default>
    public Color BackgroundColor
    {
        get => _backgroundColor ?? DefaultBackgroundColor;
        set => _backgroundColor = value;
    }

    /// <summary>
    /// Gets or sets corner radius for snackbar
    /// </summary>
    /// <default>8</default>
    public float CornerRadius
    {
        get => _cornerRadius ?? DefaultCornerRadius; 
        set => _cornerRadius = value;
    }
    
    /// <summary>
    /// Gets or sets text size for snackbar.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyMedium">MaterialFontSize.BodyMedium</see>
    /// </default>
    public double FontSize
    {
        get => _fontSize ?? DefaultFontSize; 
        set => _fontSize = value;
    }
    
    /// <summary>
    /// Gets or sets a color for text on snackbar.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.InverseOnSurface">MaterialLightTheme.InverseOnSurface</see> - Dark: <see cref="MaterialDarkTheme.InverseOnSurface">MaterialDarkTheme.InverseOnSurface</see>
    /// </default>
    public Color TextColor 
    {
        get => _textColor ?? DefaultTextColor;
        set => _textColor = value;
    }

    /// <summary>
    /// Gets or sets snackbar margin.
    /// </summary>
    /// <default>24</default>
    public Thickness Margin 
    { 
        get => _margin ?? DefaultMargin;
        set => _margin = value;
    }
    
    /// <summary>
    /// Gets or sets snackbar padding.
    /// </summary>
    /// <default>16</default>
    public Thickness Padding 
    { 
        get => _padding ?? DefaultPadding;
        set => _padding = value;
    }

    /// <summary>
    /// Gets or sets snackbar position on screen.
    /// </summary>
    /// <default>
    /// <see cref="MaterialSnackbarPosition.Bottom">MaterialSnackbarPosition.Bottom</see>
    /// </default>
    public MaterialSnackbarPosition Position
    {
        get => _position ?? DefaultPosition; 
        set => _position = value;
    }

    /// <summary>
    /// Gets or sets time that snackbar will be displayed.
    /// </summary>
    /// <default>3 seconds</default>
    public TimeSpan Duration
    {
        get => _duration ?? DefaultDuration; 
        set => _duration = value;
    }
    
    /// <summary>
    /// Gets or sets configuration for leading icon, if available.
    /// </summary>
    /// <default><see langword="null"/></default>
    public IconConfig? LeadingIcon { get; set; }
    
    /// <summary>
    /// Gets or sets configuration for trailing icon, if available.
    /// </summary>
    /// <default><see langword="null"/></default>
    public IconConfig? TrailingIcon { get; set; }
    
    /// <summary>
    /// Gets or sets configuration for custom action, if available.
    /// </summary>
    /// <default><see langword="null"/></default>
    public ActionConfig? Action { get; set; }
    
    /// <summary>
    /// Action to be executed when snackbar is dismissed.
    /// </summary>
    /// <default><see langword="null"/></default>
    public Action? OnDismissed { get; set; }
    
    /// <summary>
    /// Gets or sets space between snackbar components.
    /// </summary>
    /// <default>16</default>
    public int Spacing
    {
        get => _spacing ?? DefaultSpacing; 
        set => _spacing = value;
    }

    #endregion Properties
    
    internal static void Configure(MaterialSnackbarOptions options)
    {
        if (options.DefaultDuration != null) DefaultDuration = options.DefaultDuration.Value;
        if (options.DefaultActionColor != null) DefaultActionColor = options.DefaultActionColor;
        if (options.DefaultBackgroundColor != null) DefaultBackgroundColor = options.DefaultBackgroundColor;
        if (options.DefaultIconColor != null) DefaultIconColor = options.DefaultIconColor;
        if (options.DefaultTextColor != null) DefaultTextColor = options.DefaultTextColor;
        if (options.DefaultPadding != null) DefaultPadding = options.DefaultPadding.Value;
        if (options.DefaultMargin != null) DefaultMargin = options.DefaultMargin.Value;
        if (options.DefaultPosition != null) DefaultPosition = options.DefaultPosition.Value;
        if (options.DefaultSpacing != null) DefaultSpacing = options.DefaultSpacing.Value;
        if (options.DefaultActionSize != null) DefaultActionSize = options.DefaultActionSize.Value;
        if (options.DefaultCornerRadius != null) DefaultCornerRadius = options.DefaultCornerRadius.Value;
        if (options.DefaultFontSize != null) DefaultFontSize = options.DefaultFontSize.Value;
        if (options.DefaultIconSize != null) DefaultIconSize = options.DefaultIconSize.Value;
    }
    
    /// <summary>
    /// User-defined configuration for an icon button on <see cref="MaterialSnackbar"/>
    /// </summary>
    /// <param name="source">Icon <see cref="ImageSource"/></param>
    /// <param name="action">Action to be executed when icon is tapped</param>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class IconConfig(ImageSource source, Action? action = null) : BaseActionConfig(action)
    {
        /// <summary>
        /// Gets icon source
        /// </summary>
        /// <remarks>Required</remarks>
        public ImageSource Source { get; } = source;

        private int? _size;
        /// <summary>
        /// Gets or sets icon size for snackbar.
        /// </summary>
        /// <default>24</default>
        public int Size
        {
             get => _size ?? DefaultIconSize;
             set => _size = value;
        }
    }

    /// <summary>
    /// User-defined configuration for an action button on <see cref="MaterialSnackbar"/>
    /// </summary>
    /// <param name="text">Text for action button</param>
    /// <param name="action">Action to be executed when button is tapped</param>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class ActionConfig(string text, Action action) : BaseActionConfig(action)
    {
        /// <summary>
        /// Gets text for action button
        /// </summary>
        /// <remarks>Required</remarks>
        public string Text { get; } = text;

        private double? _fontSize;
        /// <summary>
        /// Gets or sets font size for action text.
        /// </summary>
        /// <default>
        /// <see cref="MaterialFontSize.BodyMedium">MaterialFontSize.BodyMedium</see>
        /// </default>
        public double FontSize
        {
            get => _fontSize ?? DefaultActionSize; 
            set => _fontSize = value;
        }
    }

    /// <summary>
    /// User-defined base configuration for an actions on <see cref="MaterialSnackbar"/>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public abstract class BaseActionConfig
    {
        private Color? _color;
        /// <summary>
        /// Gets or sets color for action/icon.
        /// </summary>
        /// <default>
        /// Action: [ Light: <see cref="MaterialLightTheme.InversePrimary">MaterialLightTheme.InversePrimary</see> - Dark: <see cref="MaterialDarkTheme.InversePrimary">MaterialDarkTheme.InversePrimary</see> ]
        /// Icon: [ Light: <see cref="MaterialLightTheme.InverseOnSurface">MaterialLightTheme.InverseOnSurface</see> - Dark: <see cref="MaterialDarkTheme.InverseOnSurface">MaterialDarkTheme.InverseOnSurface</see> ]
        /// </default>
        public Color Color
        {
            get => _color ?? (this is IconConfig ? DefaultIconColor : DefaultActionColor); 
            set => _color = value;
        }
        
        private Action? _action;

        /// <summary>
        /// Gets or sets an action to be executed when action/icon is tapped.
        /// </summary>
        /// <default>
        /// <see langword="null"/>
        /// </default>
        /// <remarks>Required for action button</remarks>
        public Action? Action
        {
            get => _action; 
            set => _action = value;
        }
        
        protected BaseActionConfig() {}

        protected BaseActionConfig(Action? action)
        {
            Action = action;
        }
    }
}

