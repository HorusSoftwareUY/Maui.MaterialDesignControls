using HorusStudio.Maui.MaterialDesignControls.Sample.Enums;
using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using System.Text.Json;

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

        public static async Task ApplyThemeAsync(Themes theme)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                using var stream = await FileSystem.OpenAppPackageFileAsync($"{theme}Theme.json");

                if (stream == null)
                {
                    throw new FileNotFoundException($"File not found");
                }

                using var reader = new StreamReader(stream);

                string jsonContent = await reader.ReadToEndAsync();

                var themeColors = JsonSerializer.Deserialize<ThemeColors>(jsonContent, options);

                if (themeColors != null)
                {
                    MaterialLightTheme.Primary = Color.FromArgb(themeColors.Primary);
                    MaterialLightTheme.OnPrimary = Color.FromArgb(themeColors.OnPrimary);
                    MaterialLightTheme.PrimaryContainer = Color.FromArgb(themeColors.PrimaryContainer);
                    MaterialLightTheme.OnPrimaryContainer = Color.FromArgb(themeColors.OnPrimaryContainer);
                    MaterialLightTheme.Secondary = Color.FromArgb(themeColors.Secondary);
                    MaterialLightTheme.OnSecondary = Color.FromArgb(themeColors.OnSecondary);
                    MaterialLightTheme.SecondaryContainer = Color.FromArgb(themeColors.SecondaryContainer);
                    MaterialLightTheme.OnSecondaryContainer = Color.FromArgb(themeColors.OnSecondaryContainer);
                    MaterialLightTheme.Error = Color.FromArgb(themeColors.Error);
                    MaterialLightTheme.OnError = Color.FromArgb(themeColors.OnError);
                    MaterialLightTheme.ErrorContainer = Color.FromArgb(themeColors.ErrorContainer);
                    MaterialLightTheme.OnErrorContainer = Color.FromArgb(themeColors.OnErrorContainer);
                    MaterialLightTheme.Surface = Color.FromArgb(themeColors.Surface);
                    MaterialLightTheme.OnSurface = Color.FromArgb(themeColors.OnSurface);
                    MaterialLightTheme.SurfaceVariant = Color.FromArgb(themeColors.SurfaceVariant);
                    MaterialLightTheme.OnSurfaceVariant = Color.FromArgb(themeColors.OnSurfaceVariant);
                    MaterialLightTheme.SurfaceContainerHighest = Color.FromArgb(themeColors.SurfaceContainerHighest);
                    MaterialLightTheme.SurfaceContainerHigh = Color.FromArgb(themeColors.SurfaceContainerHigh);
                    MaterialLightTheme.SurfaceContainer = Color.FromArgb(themeColors.SurfaceContainer);
                    MaterialLightTheme.SurfaceContainerLow = Color.FromArgb(themeColors.SurfaceContainerLow);
                    MaterialLightTheme.SurfaceContainerLowest = Color.FromArgb(themeColors.SurfaceContainerLowest);
                    MaterialLightTheme.InverseSurface = Color.FromArgb(themeColors.InverseSurface);
                    MaterialLightTheme.InverseOnSurface = Color.FromArgb(themeColors.InverseOnSurface);
                    MaterialLightTheme.SurfaceTint = Color.FromArgb(themeColors.SurfaceTint);
                    MaterialLightTheme.SurfaceTintColor = Color.FromArgb(themeColors.SurfaceTintColor);
                    MaterialLightTheme.Outline = Color.FromArgb(themeColors.Outline);
                    MaterialLightTheme.OutlineVariant = Color.FromArgb(themeColors.OutlineVariant);
                    MaterialLightTheme.InversePrimary = Color.FromArgb(themeColors.InversePrimary);
                    MaterialLightTheme.SurfaceBright = Color.FromArgb(themeColors.SurfaceBright);
                    MaterialLightTheme.SurfaceDim = Color.FromArgb(themeColors.SurfaceDim);
                    MaterialLightTheme.Scrim = Color.FromArgb(themeColors.Scrim);
                    MaterialLightTheme.Shadow = Color.FromArgb(themeColors.Shadow);
                    MaterialLightTheme.Text = Color.FromArgb(themeColors.Text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying theme: {ex.Message}");
            }
        }
    }
}
