using HorusStudio.Maui.MaterialDesignControls.Sample.Enums;
using HorusStudio.Maui.MaterialDesignControls.Sample.Helpers;
using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using HorusStudio.Maui.MaterialDesignControls.Sample.Pages;
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
        private Color _selectedThemeColor = ColorHelper.GetColorByKey("PurplePrimary");

        [ObservableProperty]
        private bool _isDark;
        
        [ObservableProperty]
        private bool _showCover;

        public override string Title => "Appearance";

        #endregion

        public AppearanceViewModel()
        {
            ThemeColors = ColorHelper.GetCustomizationColorsBySuffix("Primary", false);
        }

        #region Commands

        [ICommand]
        private async Task ChangeTheme(string color)
        {
            var theme = Enum.Parse<Themes>(color);
            await ColorHelper.ApplyThemeAsync(theme);
            await Shell.Current.Navigation.PushAsync(new MainPage(new MainViewModel(true)), false);
        }

        [ICommand]
        private async Task SetThemeMode(string mode)
        {
            var isDark = mode == "Dark";

            if (IsDark == isDark) return;

            ShowCover = true;
            IsDark = isDark;

            Application.Current.UserAppTheme = IsDark ? AppTheme.Dark : AppTheme.Light;
            await Shell.Current.Navigation.PushAsync(new MainPage(new MainViewModel(true)), false);

            await Task.Delay(500);
            ShowCover = false;
        }

        #endregion
    }
}
