using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class SnackbarViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Snackbar";

        #endregion

        public SnackbarViewModel()
        {
            Subtitle = "Snackbars show short updates about app processes at the bottom of the screen";
        }

        [ICommand]
        private async void SnackbarLeading()
        {
            await DisplayAlert(Title, "Leading Tapped", "OK");
        }
        
        [ICommand]
        private async void SnackbarTrailing()
        {
            await DisplayAlert(Title, "Trailing tapped!", "OK");
        }
        
        [ICommand]
        private async void SnackbarAction()
        {
            await DisplayAlert(Title, "Action tapped!", "OK");
        }
    }
}

