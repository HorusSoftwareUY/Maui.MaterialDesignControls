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
                new CustomizationColor { Color = ColorHelper.GetColorByKey($"Purple{suffix}"), TextColor = "Purple", IsSelected = true, IsLight = isLight },
                new CustomizationColor { Color = ColorHelper.GetColorByKey($"Green{suffix}"),TextColor = "Green", IsLight = isLight },
                new CustomizationColor { Color = ColorHelper.GetColorByKey($"Blue{suffix}"),TextColor = "Blue", IsLight = isLight },
                new CustomizationColor { Color = ColorHelper.GetColorByKey($"Red{suffix}"),TextColor = "Red", IsLight = isLight },
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

                var schemas = JsonSerializer.Deserialize<Schemas>(jsonContent, options);

                if (schemas != null)
                {
                    //Setting light colors
                    MaterialLightTheme.Primary = Color.FromArgb(schemas.Light.Primary);
                    MaterialLightTheme.OnPrimary = Color.FromArgb(schemas.Light.OnPrimary);
                    MaterialLightTheme.PrimaryContainer = Color.FromArgb(schemas.Light.PrimaryContainer);
                    MaterialLightTheme.OnPrimaryContainer = Color.FromArgb(schemas.Light.OnPrimaryContainer);
                    MaterialLightTheme.Secondary = Color.FromArgb(schemas.Light.Secondary);
                    MaterialLightTheme.OnSecondary = Color.FromArgb(schemas.Light.OnSecondary);
                    MaterialLightTheme.SecondaryContainer = Color.FromArgb(schemas.Light.SecondaryContainer);
                    MaterialLightTheme.OnSecondaryContainer = Color.FromArgb(schemas.Light.OnSecondaryContainer);
                    MaterialLightTheme.Error = Color.FromArgb(schemas.Light.Error);
                    MaterialLightTheme.OnError = Color.FromArgb(schemas.Light.OnError);
                    MaterialLightTheme.ErrorContainer = Color.FromArgb(schemas.Light.ErrorContainer);
                    MaterialLightTheme.OnErrorContainer = Color.FromArgb(schemas.Light.OnErrorContainer);
                    MaterialLightTheme.Surface = Color.FromArgb(schemas.Light.Surface);
                    MaterialLightTheme.OnSurface = Color.FromArgb(schemas.Light.OnSurface);
                    MaterialLightTheme.SurfaceVariant = Color.FromArgb(schemas.Light.SurfaceVariant);
                    MaterialLightTheme.OnSurfaceVariant = Color.FromArgb(schemas.Light.OnSurfaceVariant);
                    MaterialLightTheme.SurfaceContainerHighest = Color.FromArgb(schemas.Light.SurfaceContainerHighest);
                    MaterialLightTheme.SurfaceContainerHigh = Color.FromArgb(schemas.Light.SurfaceContainerHigh);
                    MaterialLightTheme.SurfaceContainer = Color.FromArgb(schemas.Light.SurfaceContainer);
                    MaterialLightTheme.SurfaceContainerLow = Color.FromArgb(schemas.Light.SurfaceContainerLow);
                    MaterialLightTheme.SurfaceContainerLowest = Color.FromArgb(schemas.Light.SurfaceContainerLowest);
                    MaterialLightTheme.InverseSurface = Color.FromArgb(schemas.Light.InverseSurface);
                    MaterialLightTheme.InverseOnSurface = Color.FromArgb(schemas.Light.InverseOnSurface);
                    MaterialLightTheme.SurfaceTint = Color.FromArgb(schemas.Light.SurfaceTint);
                    MaterialLightTheme.SurfaceTintColor = Color.FromArgb(schemas.Light.Primary);
                    MaterialLightTheme.Outline = Color.FromArgb(schemas.Light.Outline);
                    MaterialLightTheme.OutlineVariant = Color.FromArgb(schemas.Light.OutlineVariant);
                    MaterialLightTheme.InversePrimary = Color.FromArgb(schemas.Light.InversePrimary);
                    MaterialLightTheme.SurfaceBright = Color.FromArgb(schemas.Light.SurfaceBright);
                    MaterialLightTheme.SurfaceDim = Color.FromArgb(schemas.Light.SurfaceDim);
                    MaterialLightTheme.Scrim = Color.FromArgb(schemas.Light.Scrim);
                    MaterialLightTheme.Shadow = Color.FromArgb("4D000000");
                    MaterialLightTheme.Text = Color.FromArgb(schemas.Light.OnSurface);
                    //Setting dark colors
                    MaterialDarkTheme.Primary = Color.FromArgb(schemas.Dark.Primary);
                    MaterialDarkTheme.OnPrimary = Color.FromArgb(schemas.Dark.OnPrimary);
                    MaterialDarkTheme.PrimaryContainer = Color.FromArgb(schemas.Dark.PrimaryContainer);
                    MaterialDarkTheme.OnPrimaryContainer = Color.FromArgb(schemas.Dark.OnPrimaryContainer);
                    MaterialDarkTheme.Secondary = Color.FromArgb(schemas.Dark.Secondary);
                    MaterialDarkTheme.OnSecondary = Color.FromArgb(schemas.Dark.OnSecondary);
                    MaterialDarkTheme.SecondaryContainer = Color.FromArgb(schemas.Dark.SecondaryContainer);
                    MaterialDarkTheme.OnSecondaryContainer = Color.FromArgb(schemas.Dark.OnSecondaryContainer);
                    MaterialDarkTheme.Error = Color.FromArgb(schemas.Dark.Error);
                    MaterialDarkTheme.OnError = Color.FromArgb(schemas.Dark.OnError);
                    MaterialDarkTheme.ErrorContainer = Color.FromArgb(schemas.Dark.ErrorContainer);
                    MaterialDarkTheme.OnErrorContainer = Color.FromArgb(schemas.Dark.OnErrorContainer);
                    MaterialDarkTheme.Surface = Color.FromArgb(schemas.Dark.Surface);
                    MaterialDarkTheme.OnSurface = Color.FromArgb(schemas.Dark.OnSurface);
                    MaterialDarkTheme.SurfaceVariant = Color.FromArgb(schemas.Dark.SurfaceVariant);
                    MaterialDarkTheme.OnSurfaceVariant = Color.FromArgb(schemas.Dark.OnSurfaceVariant);
                    MaterialDarkTheme.SurfaceContainerHighest = Color.FromArgb(schemas.Dark.SurfaceContainerHighest);
                    MaterialDarkTheme.SurfaceContainerHigh = Color.FromArgb(schemas.Dark.SurfaceContainerHigh);
                    MaterialDarkTheme.SurfaceContainer = Color.FromArgb(schemas.Dark.SurfaceContainer);
                    MaterialDarkTheme.SurfaceContainerLow = Color.FromArgb(schemas.Dark.SurfaceContainerLow);
                    MaterialDarkTheme.SurfaceContainerLowest = Color.FromArgb(schemas.Dark.SurfaceContainerLowest);
                    MaterialDarkTheme.InverseSurface = Color.FromArgb(schemas.Dark.InverseSurface);
                    MaterialDarkTheme.InverseOnSurface = Color.FromArgb(schemas.Dark.InverseOnSurface);
                    MaterialDarkTheme.SurfaceTint = Color.FromArgb(schemas.Dark.SurfaceTint);
                    MaterialDarkTheme.SurfaceTintColor = Color.FromArgb(schemas.Dark.Primary);
                    MaterialDarkTheme.Outline = Color.FromArgb(schemas.Dark.Outline);
                    MaterialDarkTheme.OutlineVariant = Color.FromArgb(schemas.Dark.OutlineVariant);
                    MaterialDarkTheme.InversePrimary = Color.FromArgb(schemas.Dark.InversePrimary);
                    MaterialDarkTheme.SurfaceBright = Color.FromArgb(schemas.Dark.SurfaceBright);
                    MaterialDarkTheme.SurfaceDim = Color.FromArgb(schemas.Dark.SurfaceDim);
                    MaterialDarkTheme.Scrim = Color.FromArgb(schemas.Dark.Scrim);
                    MaterialDarkTheme.Shadow = Color.FromArgb("4D000000");
                    MaterialDarkTheme.Text = Color.FromArgb(schemas.Dark.OnSurface);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying theme: {ex.Message}");
            }
        }
    }
}
