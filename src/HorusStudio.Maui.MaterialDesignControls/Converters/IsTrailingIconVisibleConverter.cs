using System.Globalization;

namespace HorusStudio.Maui.MaterialDesignControls.Converters;

/// <summary>
/// Convert if Trailing icon is visible based on three properties
/// The source, Show only on error and HasError.
/// to work with this you should send the parameters in this way:
/// 0: ImageSource
/// 1: HasError
/// 3: ShowOnlyOnError
/// </summary>
class IsTrailingIconVisibleConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is null || values.Length < 3 || values.Any(x => x is null)) return false;
        
        _ = (ImageSource)values[0];
        var hasError = (bool)values[1];
        var showOnlyOnError = (bool)values[2];

        if (hasError || !showOnlyOnError)
            return true;

        return false;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
