using System.Globalization;

namespace HorusStudio.Maui.MaterialDesignControls.Converters;

/// <summary>
/// Determine trailing icon source based on three properties
/// Default trailing icon source, has error flag and error trailing icon source.
/// Usage:
/// 0: TrailingIcon
/// 1: HasError
/// 2: ErrorIcon
/// </summary>
class TrailingIconIsVisibleConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is null || values.Length < 3) return false;
        
        var trailingIcon = values[0] as ImageSource;
        var hasError = (bool)values[1];
        var errorIcon = values[2] as ImageSource;

        return trailingIcon is not null || (hasError && errorIcon is not null);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
