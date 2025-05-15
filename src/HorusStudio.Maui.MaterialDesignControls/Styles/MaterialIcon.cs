namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// MaterialIcon define default icons used by the library
/// </summary>
public static class MaterialIcon
{
    private const string DefaultErrorIcon = "mdc_ic_error.png";
    private const string DefaultDatePickerIcon = "mdc_ic_datepicker.png";
    private const string DefaultPickerIcon = "mdc_ic_picker.png";
    private const string DefaultTimePickerIcon = "mdc_ic_timepicker.png";
    
    private static ImageSource GetResource(string resourceName) => ImageSource.FromResource($"{typeof(MaterialIcon).Namespace}.Images.{resourceName}"); 
    
    /// <default><see href="https://fonts.google.com/icons?selected=Material+Icons+Outlined:error"/></default>
    public static ImageSource Picker { get; set; } = GetResource(DefaultPickerIcon);
    
    /// <default><see href="https://fonts.google.com/icons?selected=Material+Icons+Outlined:expand_more"/></default>
    public static ImageSource Error { get; set; } = GetResource(DefaultErrorIcon);
    
    /// <default><see href="https://fonts.google.com/icons?selected=Material+Icons+Outlined:calendar_today"/></default>
    public static ImageSource DatePicker { get; set; } = GetResource(DefaultDatePickerIcon);
    
    /// <default><see href="https://fonts.google.com/icons?selected=Material+Icons+Outlined:schedule"/></default>
    public static ImageSource TimePicker { get; set; } = GetResource(DefaultTimePickerIcon);

    internal static void Configure(MaterialIconOptions options)
    {
        if (options.DatePicker != null) DatePicker = options.DatePicker;
        if (options.Picker != null) Picker = options.Picker;
        if (options.TimePicker != null) TimePicker = options.TimePicker;
        if (options.Error != null) Error = options.Error;
    }
}