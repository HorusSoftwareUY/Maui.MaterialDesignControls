namespace HorusStudio.Maui.MaterialDesignControls;

public enum SnackbarPosition
{
    Bottom,
    Top
}

public class SnackbarConfig(string message)
{
    #region Default values
    
    public static Thickness DefaultMargin { get; set; } = new(24);
    public static Thickness DefaultPadding { get; set; } = new(16);
    public static SnackbarPosition DefaultPosition { get; set; } = SnackbarPosition.Bottom;
    public static int DefaultIconSize { get; set; } = 24;
    public static float DefaultCornerRadius { get; set; } = 4f;
    public static Color DefaultBackgroundColor { get; set; } = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseSurface, Dark = MaterialDarkTheme.InverseSurface }.GetValueForCurrentTheme<Color>();
    public static Color DefaultTextColor { get; set; } = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    public static double DefaultFontSize { get; set; } = MaterialFontSize.BodyMedium;
    public static Color DefaultActionColor { get; set; } = new AppThemeBindingExtension { Light = MaterialLightTheme.InversePrimary, Dark = MaterialDarkTheme.InversePrimary }.GetValueForCurrentTheme<Color>();
    public static double DefaultActionSize { get; set; } = MaterialFontSize.BodyMedium;
    public static Color DefaultIconColor { get; set; } = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    public static TimeSpan DefaultDuration { get; set; } = TimeSpan.FromSeconds(3);
    public static int DefaultSpacing { get; set; } = 16;
    
    #endregion Default values
    
    public string Message { get; } = message;
    
    private Color? _backgroundColor;
    public Color BackgroundColor
    {
        get => _backgroundColor ?? DefaultBackgroundColor;
        set => _backgroundColor = value;
    }

    private float? _cornerRadius;
    public float CornerRadius
    {
        get => _cornerRadius ?? DefaultCornerRadius; 
        set => _cornerRadius = value;
    }
    
    private double? _fontSize;
    public double FontSize
    {
        get => _fontSize ?? DefaultFontSize; 
        set => _fontSize = value;
    }
    
    private Color? _textColor;
    public Color TextColor 
    {
        get => _textColor ?? DefaultTextColor;
        set => _textColor = value;
    }

    private Thickness? _margin;
    public Thickness Margin 
    { 
        get => _margin ?? DefaultMargin;
        set => _margin = value;
    }
    
    private Thickness? _padding;
    public Thickness Padding 
    { 
        get => _padding ?? DefaultPadding;
        set => _padding = value;
    }

    private SnackbarPosition? _position;
    public SnackbarPosition Position
    {
        get => _position ?? DefaultPosition; 
        set => _position = value;
    }

    private TimeSpan? _duration;
    public TimeSpan Duration
    {
        get => _duration ?? DefaultDuration; 
        set => _duration = value;
    }
    
    public IconConfig? LeadingIcon { get; set; }
    
    public IconConfig? TrailingIcon { get; set; }
    
    public ActionConfig? Action { get; set; }
    
    public Action? OnDismissed { get; set; }
    
    private int? _spacing;
    public int Spacing
    {
        get => _spacing ?? DefaultSpacing; 
        set => _spacing = value;
    }

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
    
    public class IconConfig(ImageSource source, Action action) : BaseActionConfig(action)
    {
        public ImageSource Source { get; } = source;

        private int? _size;

        public int Size
        {
             get => _size ?? DefaultIconSize;
             set => _size = value;
        }
    }

    public class ActionConfig(string text, Action action) : BaseActionConfig(action)
    {
        public string Text { get; } = text;

        private double? _fontSize;
        public double FontSize
        {
            get => _fontSize ?? DefaultActionSize; 
            set => _fontSize = value;
        }
    }

    public abstract class BaseActionConfig(Action action)
    {
        private Color? _color;
        public Color Color
        {
            get => _color ?? (this is IconConfig ? DefaultIconColor : DefaultActionColor); 
            set => _color = value;
        }
        
        public Action Action { get; } = action;
    }
}

