using HorusStudio.Maui.MaterialDesignControls.Sample.Models;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Helpers
{
    public class ColorHelper
    {
        public static Color GetColorByKey(string key)
        {
            if (Application.Current.Resources.TryGetValue(key, out var value) && value is Color color)
            {
                return color;
            }

            throw new ArgumentException($"El color con la clave '{key}' no fue encontrado o no es un tipo Color.");
        }

        public static List<CustomizationColor> GetCustomizationColorsBySuffix(string suffix, bool isLight) 
        {
            return new List<CustomizationColor>
            {
                new CustomizationColor { Color = ColorHelper.GetColorByKey($"Purple{suffix}"), IsSelected = true, IsLight = isLight },
                new CustomizationColor { Color = ColorHelper.GetColorByKey($"Green{suffix}"), IsLight = isLight },
                new CustomizationColor { Color = ColorHelper.GetColorByKey($"Blue{suffix}"), IsLight = isLight },
                new CustomizationColor { Color = ColorHelper.GetColorByKey($"Red{suffix}"), IsLight = isLight },
            };
        }
    }
}
