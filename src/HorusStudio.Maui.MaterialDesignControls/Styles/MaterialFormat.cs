namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// MaterialIcon define default icons used by the library
/// </summary>
public static class MaterialFormat
{
    public const string DefaultDateFormat = "dd/MM/yyyy";
    public const string DefaultTimeFormat = "t";
    
    /// <default>dd/MM/yyyy</default>
    public static string DateFormat { get; set; } = DefaultDateFormat;
    
    /// <default>[h]h:mm [a.m.|p.m.]</default>
    public static string TimeFormat { get; set; } = DefaultTimeFormat;
}