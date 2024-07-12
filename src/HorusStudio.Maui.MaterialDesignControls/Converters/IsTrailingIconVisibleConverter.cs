using System.Globalization;

namespace HorusStudio.Maui.MaterialDesignControls.Converters;

/// <summary>
/// COnvert if Trailing icon is visible based on three properties
/// The source, Show only on error and HasError.
/// to work with this you should send the parameters in this way:
/// 0: ImageSource
/// 1: HasError
/// 3: ShowOnlyOnError
/// </summary>
internal class IsTrailingIconVisibleConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is null || values.Length < 3) return false;

        if (values.Any(x => x == null)) return false;

        var source = (ImageSource)values[0];
        var hasError = (bool)values[1];
        var showOnlyOnError = (bool)values[2];

        if (source is null || (source is not null && !hasError && showOnlyOnError))
            return false;

        return true;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
