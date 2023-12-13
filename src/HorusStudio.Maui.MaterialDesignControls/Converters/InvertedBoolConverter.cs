using System;
using System.Globalization;

namespace HorusStudio.Maui.MaterialDesignControls.Converters
{
	class InvertedBoolConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ArgumentNullException.ThrowIfNull(value);

            return value.ToString().Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
