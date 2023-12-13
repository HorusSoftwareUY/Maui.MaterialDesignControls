using System;
using System.Globalization;

namespace HorusStudio.Maui.MaterialDesignControls.Converters
{
	class AddOpacityToColorConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ArgumentNullException.ThrowIfNull(value);

            float opacity;
            try
            {
                opacity = float.Parse(parameter.ToString());
            }
            catch
            {
                opacity = 1;
            }

            if (value is Color cValue)
            {
                return cValue.WithAlpha(opacity);
            }

            if (value is string sValue)
            {
                try
                {
                    Color colorValue = Color.FromRgba(sValue);
                    return colorValue.WithAlpha(opacity);
                }
                catch { }
            }

            throw new ArgumentException("'value' must be of type string hex or Color");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

