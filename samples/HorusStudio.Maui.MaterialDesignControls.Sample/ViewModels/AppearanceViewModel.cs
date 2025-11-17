using HorusStudio.Maui.MaterialDesignControls.Sample.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class AppearanceViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.Appearance;


        [ObservableProperty]
        private AppTheme _selectedThemeMode = AppTheme.Light;
        #endregion

        public AppearanceViewModel()
        {
            LoadCurrentTheme();
        }

        private void LoadCurrentTheme()
        {
            SelectedThemeMode = Application.Current?.RequestedTheme ?? AppTheme.Unspecified;
        }

        [ICommand]
        private void SelectThemeMode(AppTheme theme)
        {
            SelectedThemeMode = theme;

            if (Application.Current != null)
                Application.Current.UserAppTheme = theme;

            var currentTheme = Application.Current?.RequestedTheme;
            var isDark = currentTheme == AppTheme.Dark;

            StatusBarHelper.SetStatusBarColor(isDark ? MaterialDarkTheme.Surface : MaterialLightTheme.Surface, !isDark);
        }
    }
}