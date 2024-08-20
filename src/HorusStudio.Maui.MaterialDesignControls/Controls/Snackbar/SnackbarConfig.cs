namespace HorusStudio.Maui.MaterialDesignControls;

public enum SnackbarActionType
{
    Timeout,
    UserInteraction,
    Cancelled
}

public enum SnackbarPosition
{
    Bottom,
    Top
}

public class SnackbarConfig
{
    public static string DefaultMessageFontFamily { get; set; }
    public static string DefaultNegativeButtonFontFamily { get; set; }
    public static int DefaultIconSize { get; set; } = 24;
    public static float DefaultCornerRadius { get; set; } = 4f;
    public static Color DefaultBackgroundColor { get; set; } = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseSurface, Dark = MaterialDarkTheme.InverseSurface }.GetValueForCurrentTheme<Color>();
    public static double DefaultTextFontSize { get; set; } = MaterialFontSize.LabelLarge;
    public static Color DefaultTextColor { get; set; } = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    public static Color DefaultActionTextColor { get; set; } = new AppThemeBindingExtension { Light = MaterialLightTheme.InversePrimary, Dark = MaterialDarkTheme.InversePrimary }.GetValueForCurrentTheme<Color>();
    public static double DefaultActionFontSize { get; set; } = MaterialFontSize.LabelLarge;
    public static SnackbarPosition DefaultPosition { get; set; } = SnackbarPosition.Bottom;
    public static string DefaultActionText = "Action";
    public static TimeSpan DefaultDuration { get; set; } = TimeSpan.FromSeconds(3);
    public static Color DefaultIconTintColor = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialLightTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>(); 
    
    public Color BackgroundColor { get; set; } = DefaultBackgroundColor;
    public float CornerRadius { get; set; } = DefaultCornerRadius;
    public double TextFontSize { get; set; } = DefaultTextFontSize;
    public Color TextColor { get; set; } = DefaultTextColor;
    public Color ActionTextColor { get; set; } = DefaultActionTextColor;
    public double ActionFontSize { get; set; } = DefaultActionFontSize;
    public SnackbarPosition Position { get; set; } = DefaultPosition;
    public int IconSize = DefaultIconSize;
    public Color IconTintColor = DefaultIconTintColor;
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