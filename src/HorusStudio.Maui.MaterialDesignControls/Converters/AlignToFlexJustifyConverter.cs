using Microsoft.Maui.Layouts;
using System.Globalization;

namespace HorusStudio.Maui.MaterialDesignControls.Converters
{
    public class AlignToFlexJustifyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Enum.TryParse(value.ToString(), out Align align))
            {
                switch (align)
                {
                    case Align.Start:
                        return FlexJustify.Start;
                    case Align.Center:
                        return FlexJustify.Center;
                    case Align.End:
                        return FlexJustify.End;
                }
            }

            return FlexJustify.Start;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
