using HorusStudio.Maui.MaterialDesignControls.Sample.Enums;
using HorusStudio.Maui.MaterialDesignControls.Sample.Helpers;
using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class AppearanceViewModel : BaseViewModel
    {
        #region Attributes & Properties

        [ObservableProperty]
        private List<CustomizationColor> _themeColors;

        [ObservableProperty]
        private Color _selectedThemeColor = Color.FromArgb("#68548E");

        public override string Title => "Appearance";

        #endregion

        public AppearanceViewModel()
        {
            ThemeColors = new List<CustomizationColor>() 
            {
                new CustomizationColor{ Color = Color.FromArgb("#336940"), TextColor="Green" },
                new CustomizationColor{ Color = Color.FromArgb("#505B92"), TextColor="Blue" },
                new CustomizationColor{ Color = Color.FromArgb("#68548E"), TextColor="Purple", IsSelected = true },
                new CustomizationColor{ Color = Color.FromArgb("#8F4951"), TextColor="Red" },
            };
        }

        #region Commands

        [ICommand]
        private async Task ChangeTheme(string color)
        {
            var theme = Enum.Parse<Themes>(color);
            await ColorHelper.ApplyThemeAsync(theme);

            var parameters = new Dictionary<string, object>() { { "color", color } };
            await GoToAsync<ChangeThemeViewModel>(parameters: parameters, animate: false);
        }

        #endregion
    }
}
