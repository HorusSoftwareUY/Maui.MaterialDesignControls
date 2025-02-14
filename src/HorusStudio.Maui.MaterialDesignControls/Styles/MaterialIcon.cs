namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// MaterialIcon define default icons used by the library
/// </summary>
public static class MaterialIcon
{
    public const string DefaultErrorIcon = "mdc_ic_error.png";
    public const string DefaultDatePickerIcon = "mdc_ic_datepicker.png";
    public const string DefaultPickerIcon = "mdc_ic_picker.png";
    public const string DefaultTimePickerIcon = "mdc_ic_timepicker.png";
    
    /// <default><see langword="null"/></default>
    public static ImageSource Picker { get; set; } = DefaultPickerIcon;
    
    /// <default><see langword="null"/></default>
    public static ImageSource Error { get; set; } = DefaultErrorIcon;
    
    /// <default><see langword="null"/></default>
    public static ImageSource DatePicker { get; set; } = DefaultDatePickerIcon;
    
    /// <default><see langword="null"/></default>
    public static ImageSource TimePicker { get; set; } = DefaultTimePickerIcon;

    internal static void Configure(MaterialIconOptions options)
    {
        if (options.DatePicker != null) DatePicker = options.DatePicker;
        if (options.Picker != null) Picker = options.Picker;
        if (options.TimePicker != null) TimePicker = options.TimePicker;
        if (options.Error != null) Error = options.Error;
    }
}