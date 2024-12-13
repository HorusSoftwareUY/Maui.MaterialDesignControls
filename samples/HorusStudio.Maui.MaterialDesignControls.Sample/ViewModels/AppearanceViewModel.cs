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
        private Color _selectedThemeColor = AppearanceHelper.SelectedThemeColor;

        [ObservableProperty]
        private bool _isDark = AppearanceHelper.IsDark;
        
        [ObservableProperty]
        private bool _showCover;
        
        [ObservableProperty]
        private Color _lightBorderColor = MaterialLightTheme.Primary;
        
        [ObservableProperty]
        private Color _darkBorderColor = MaterialLightTheme.OnPrimary;
        
        [ObservableProperty]
        private Color _lightFillColor = MaterialLightTheme.Primary;
        
        [ObservableProperty]
        private Color _darkFillColor = MaterialLightTheme.OnSurfaceVariant;

        public override string Title => "Appearance";

        #endregion

        public AppearanceViewModel()   
        {
            SetModeColors(IsDark);

            ThemeColors = ColorHelper.GetCustomizationColorsBySuffix("Primary", false);

            foreach (var themeColor in ThemeColors) 
            {
                themeColor.IsSelected = themeColor.Color == SelectedThemeColor;
            }
        }

        #region Commands

        [ICommand]
        private async Task ChangeTheme(string color)
        {
            var theme = Enum.Parse<Themes>(color);
            await ColorHelper.ApplyThemeAsync(theme);
            AppearanceHelper.SelectedThemeColor = SelectedThemeColor;
            await Shell.Current.Navigation.PushAsync(new MainPage(new MainViewModel(true)), false);
        }

        [ICommand]
        private async Task ChangeMode(string mode)
        {
            var isDark = mode == "Dark";

            if (IsDark == isDark) return;

            ShowCover = true;
            IsDark = isDark;

            SetModeColors(isDark);

            AppearanceHelper.IsDark = isDark;      

            Application.Current.UserAppTheme = IsDark ? AppTheme.Dark : AppTheme.Light;
            await Shell.Current.Navigation.PushAsync(new MainPage(new MainViewModel(true)), false);

            await Task.Delay(500);
            ShowCover = false;
        }

        #endregion

        private void SetModeColors(bool isDark)
        {
            DarkBorderColor = isDark ? MaterialLightTheme.Primary : MaterialLightTheme.OnPrimary;
            LightBorderColor = isDark ? MaterialLightTheme.OnPrimary : MaterialLightTheme.Primary;

            DarkFillColor = isDark ? MaterialLightTheme.Primary : MaterialLightTheme.OnSurfaceVariant;
            LightFillColor = isDark ? MaterialLightTheme.OnSurfaceVariant : MaterialLightTheme.Primary;
        }
    }
}
