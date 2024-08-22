namespace HorusStudio.Maui.MaterialDesignControls;

public enum SnackbarPosition
{
    Bottom,
    Top
}

public class SnackbarConfig
{
    public static int DefaultIconSize { get; set; } = 24;
    public static float DefaultCornerRadius { get; set; } = 4f;
    
    private static Color? _defaultBackgroundColor;
    public static Color? DefaultBackgroundColor 
    {
        get
        {
            try
            {
                return _defaultBackgroundColor?? new AppThemeBindingExtension { Light = MaterialLightTheme.InverseSurface, Dark = MaterialDarkTheme.InverseSurface }.GetValueForCurrentTheme<Color>();
            }
            catch (Exception e)
            {
                return MaterialLightTheme.InverseSurface;
            }
        }
        set
        {
            if (_defaultBackgroundColor != value) _defaultBackgroundColor = value;
        }
    }
    public static double DefaultTextFontSize { get; set; } = MaterialFontSize.LabelLarge;

    private static Color? _defaultTextColor;
    public static Color? DefaultTextColor 
    {
        get
        {
            try
            {
                return _defaultTextColor?? new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
            }
            catch (Exception e)
            {
                return MaterialLightTheme.InverseOnSurface;
            }
        }
        set
        {
            if (_defaultTextColor != value) _defaultTextColor = value;
        }
    }

    private static Color? _defaultActionTextColor;
    public static Color? DefaultActionTextColor 
    { 
        get
        {
            try
            {
                return _defaultActionTextColor?? new AppThemeBindingExtension { Light = MaterialLightTheme.InversePrimary, Dark = MaterialDarkTheme.InversePrimary }.GetValueForCurrentTheme<Color>();
            }
            catch (Exception e)
            {
                return MaterialLightTheme.InversePrimary;
            }
        }
        set
        {
            if ( _defaultActionTextColor != value) _defaultActionTextColor = value;
        }
    }
    public static double DefaultActionFontSize { get; set; } = MaterialFontSize.LabelLarge;
    public static SnackbarPosition DefaultPosition { get; set; } = SnackbarPosition.Bottom;
    public static string DefaultActionText = "Action";
    public static TimeSpan DefaultDuration { get; set; } = TimeSpan.FromSeconds(3);

    private static Color? _defaultIconTintColor;
    public static Color? DefaultIconTintColor 
    { 
        get
        {
            try
            {
                return _defaultIconTintColor?? new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
            }
            catch (Exception e)
            {
                return MaterialLightTheme.InverseOnSurface;
            }
        }
        set
        {
            if ( _defaultIconTintColor != value) _defaultIconTintColor = value;
        }
    }

    public Color BackgroundColor { get; set; } = DefaultBackgroundColor;
    public float CornerRadius { get; set; } = DefaultCornerRadius;
    public double TextFontSize { get; set; } = DefaultTextFontSize;
    public Color TextColor { get; set; } = DefaultTextColor;
    public Color ActionTextColor { get; set; } = DefaultActionTextColor;
    public double ActionFontSize { get; set; } = DefaultActionFontSize;
    public SnackbarPosition Position { get; set; } = DefaultPosition;
    public int IconSize = DefaultIconSize;
    public Color LeadingIconTintColor { get; set; } = DefaultIconTintColor;
    
    public Color TrailingIconTintColor { get; set; } = DefaultIconTintColor;
    public string Message { get; set; }
    public string LeadingIcon { get; set; }
    public string TrailingIcon { get; set; }
    public TimeSpan Duration { get; set; } = DefaultDuration;
    public Action Action { get; set; }
    public Action ActionLeading { get; set; }
    public Action ActionTrailing { get; set; }
    public Action DimissAction { get; set; }
    public string ActionText { get; set; } = DefaultActionText;
}