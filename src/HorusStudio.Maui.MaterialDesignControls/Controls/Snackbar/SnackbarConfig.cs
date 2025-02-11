namespace HorusStudio.Maui.MaterialDesignControls;

public enum SnackbarPosition
{
    Bottom,
    Top
}

public class SnackbarConfig(string message)
{
    #region Default valuea
    
    public static Thickness DefaultMargin { get; set; } = new(24);
    public static Thickness DefaultPadding { get; set; } = new(16,0);
    public static SnackbarPosition DefaultPosition { get; set; } = SnackbarPosition.Bottom;
    public static int DefaultIconSize { get; set; } = 24;
    public static float DefaultCornerRadius { get; set; } = 4f;
    public static Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseSurface, Dark = MaterialDarkTheme.InverseSurface }.GetValueForCurrentTheme<Color>();
    public static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    public static double DefaultFontSize = MaterialFontSize.BodyMedium;
    public static Color DefaultActionColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InversePrimary, Dark = MaterialDarkTheme.InversePrimary }.GetValueForCurrentTheme<Color>();
    public static double DefaultActionSize { get; set; } = MaterialFontSize.BodyMedium;
    public static Color DefaultIconColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    public static TimeSpan DefaultDuration { get; set; } = TimeSpan.FromSeconds(3);
    public static int DefaultSpacing = 16;
    
    #endregion
    
    public string Message { get; private set; } = message;
    
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
    
    public Action? DimissAction { get; set; }
    
    private int? _spacing;
    public int Spacing
    {
        get => _spacing ?? DefaultSpacing; 
        set => _spacing = value;
    }
    
    public class IconConfig(ImageSource source) : BaseActionConfig
    {
        public ImageSource Source { get; private set; } = source;

        private int? _size;

        public int Size
        {
             get => _size ?? DefaultIconSize;
             set => _size = value;
        }
    }

    public class ActionConfig(string text) : BaseActionConfig
    {
        public string Text { get; private set; } = text;

        private double? _fontSize;
        public double FontSize
        {
            get => _fontSize ?? DefaultActionSize; 
            set => _fontSize = value;
        }
    }

    public abstract class BaseActionConfig
    {
        private Color? _color;
        public Color Color
        {
            get => _color ?? (this is IconConfig ? DefaultIconColor : DefaultActionColor); 
            set => _color = value;
        }
        
        public Action? Action { get; set; }
    }
}

