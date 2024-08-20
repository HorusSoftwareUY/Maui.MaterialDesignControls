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
    public static double DefaultIconSize { get; set; } = 24;
    public static float DefaultCornerRadius { get; set; } = 4f;
    public static Color DefaultBackgroundColor { get; set; } = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseSurface, Dark = MaterialDarkTheme.InverseSurface }.GetValueForCurrentTheme<Color>();
    public static double DefaultMessageFontSize { get; set; } = MaterialFontSize.LabelLarge;
    public static Color DefaultMessageColor { get; set; } = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    public static Color DefaultNegativeButtonTextColor { get; set; } = new AppThemeBindingExtension { Light = MaterialLightTheme.InverseOnSurface, Dark = MaterialDarkTheme.InverseOnSurface }.GetValueForCurrentTheme<Color>();
    public static double DefaultNegativeButtonFontSize { get; set; } = MaterialFontSize.LabelLarge;
    public static SnackbarPosition DefaultPosition { get; set; } = SnackbarPosition.Bottom;
    public static string DefaultActionText = "Action";
    public static TimeSpan DefaultDuration { get; set; } = TimeSpan.FromSeconds(3);
    
    public string MessageFontFamily { get; set; } = DefaultMessageFontFamily;
    public string NegativeButtonFontFamily { get; set; } = DefaultNegativeButtonFontFamily;
    public Color BackgroundColor { get; set; } = DefaultBackgroundColor;
    public float CornerRadius { get; set; } = DefaultCornerRadius;
    public double MessageFontSize { get; set; } = DefaultMessageFontSize;
    public Color MessageColor { get; set; } = DefaultMessageColor;
    public Color NegativeButtonTextColor { get; set; } = DefaultNegativeButtonTextColor;
    public double NegativeButtonFontSize { get; set; } = DefaultNegativeButtonFontSize;
    public SnackbarPosition Position { get; set; } = DefaultPosition;
    public double IconSize = DefaultIconSize;

    public string Message { get; set; }
    public string LeadingIcon { get; set; }
    public string TrailingIcon { get; set; }
    public TimeSpan Duration { get; set; } = DefaultDuration;
    public Action<SnackbarActionType> Action { get; set; }
    public Action<SnackbarActionType> ActionLeading { get; set; }
    public Action<SnackbarActionType> ActionTrailing { get; set; }
    
    public string ActionText { get; set; } = DefaultActionText;

    public SnackbarConfig SetMessage(string message)
    {
        this.Message = message;
        return this;
    }
    
    public SnackbarConfig SetActionText(string text)
    {
        this.ActionText = text;
        return this;
    }

    public SnackbarConfig SetLeadingIcon(string icon)
    {
        this.LeadingIcon = icon;
        return this;
    }
    
    public SnackbarConfig SetTrailingIcon(string icon)
    {
        this.TrailingIcon = icon;
        return this;
    }

    public SnackbarConfig SetAction(Action<SnackbarActionType> action)
    {
        this.Action = action;
        return this;
    }
    
    public SnackbarConfig SetActionLeading(Action<SnackbarActionType> action)
    {
        this.ActionLeading = action;
        return this;
    }
    
    public SnackbarConfig SetActionTrailing(Action<SnackbarActionType> action)
    {
        this.ActionTrailing = action;
        return this;
    }

    public SnackbarConfig SetDuration(int millis) => this.SetDuration(TimeSpan.FromMilliseconds(millis));

    public SnackbarConfig SetDuration(TimeSpan? duration = null)
    {
        this.Duration = duration ?? DefaultDuration;
        return this;
    }
}