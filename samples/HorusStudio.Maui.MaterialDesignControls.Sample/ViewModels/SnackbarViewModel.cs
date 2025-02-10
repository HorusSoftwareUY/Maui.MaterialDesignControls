using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class SnackbarViewModel : BaseViewModel
    {
        #region Attributes & Properties
        
        private readonly ISnackbarUser _snackbarUser;
        
        public override string Title => "Snackbar";

        #endregion

        public SnackbarViewModel(ISnackbarUser snackbarUser)
        {
            _snackbarUser = snackbarUser;
            Subtitle = "Snackbars show short updates about app processes at the bottom of the screen";
        }

        [ICommand]
        private async void ShowSnackbarExample()
        {
            _snackbarUser.ShowSnackbar(new SnackbarConfig
            {
                Message = "This is snackbar",
                Action = SnackbarAction,
            });
        }
        
        [ICommand]
        private async void ShowSnackbarIconExample()
        {
            _snackbarUser.ShowSnackbar(new SnackbarConfig
            {
                LeadingIcon = "horus_logo.png",
                TrailingIcon = "horus_logo.png",
                Message = "Lorem ipsum dolor sit amet",
                LeadingIconTintColor = Colors.Red,
                Action = SnackbarAction,
                ActionLeading = SnackbarLeading,
                ActionTrailing = SnackbarTrailing,
            });
        }
        
        [ICommand]
        private async void ShowSnackbarFullAPIExample()
        {
            _snackbarUser.ShowSnackbar(new SnackbarConfig
            {
                LeadingIcon = "horus_logo.png",
                TrailingIcon = "horus_logo.png",
                Message = "Lorem ipsum dolor sit amet",
                ActionText = "Action API",
                TextColor = Colors.Aqua,
                ActionTextColor = Colors.Fuchsia,
                BackgroundColor = Colors.Green,
                LeadingIconTintColor = Colors.Red,
                TrailingIconTintColor = Colors.Coral,
                Action = SnackbarAction,
                ActionFontSize = 28,
                IconSize = 35,
                ActionLeading = SnackbarLeading,
                ActionTrailing = SnackbarTrailing,
            });
        }

        [ICommand]
        private async void SnackbarLeading()
        {
            await DisplayAlert(Title, "Leading icon tapped!", "OK");
        }
        
        [ICommand]
        private async void SnackbarTrailing()
        {
            await DisplayAlert(Title, "Trailing icon tapped!", "OK");
        }
        
        [ICommand]
        private async void SnackbarAction()
        {
            await DisplayAlert(Title, "Action tapped!", "OK");
        }
    }
}

