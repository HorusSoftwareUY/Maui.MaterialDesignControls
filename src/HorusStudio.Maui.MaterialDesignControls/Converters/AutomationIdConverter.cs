using System.Globalization;

namespace HorusStudio.Maui.MaterialDesignControls.Converters;

public class AutomationIdConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string valueString && !string.IsNullOrEmpty(valueString))
        {
            if (parameter is string parameterString && !string.IsNullOrEmpty(parameterString))
            {
                return $"{valueString}_{parameterString}";
            }
            else
            {
                return valueString;
            }
        }
        else
        {
            return null;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}