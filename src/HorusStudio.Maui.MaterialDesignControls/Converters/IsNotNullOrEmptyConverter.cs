using System.Globalization;

namespace HorusStudio.Maui.MaterialDesignControls.Converters;

class IsNotNullOrEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return false;
        if (value is string text)
            return !string.IsNullOrEmpty(text);
        throw new NotSupportedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}