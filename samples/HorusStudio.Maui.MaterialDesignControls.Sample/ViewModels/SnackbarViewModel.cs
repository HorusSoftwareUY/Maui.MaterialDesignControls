using System;
using HorusStudio.Maui.MaterialDesignControls.Interface;
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
        private async void ShowSnackbarExample()
        {
            var snack = new MaterialSnackbar()
            {
                Text = "Lorem ipsum dolor sit amet",
                ActionText = "Action"
            };
            var snackbarState = Snackbar.Make(snack, test, test, test);
            await snackbarState.Show();
        }
        
        [ICommand]
        private async void ShowSnackbarIconExample()
        {
            var snack = new MaterialSnackbar()
            {
                LeadingIcon = "horus_logo.png",
                Text = "Lorem ipsum dolor sit amet",
                ActionText = "Action",
                TrailingIcon = "horus_logo.png"
            };
            var snackbarState = Snackbar.Make(snack, test, test, test);
            await snackbarState.Show();
        }
        
        [ICommand]
        private async void ShowSnackbarFullAPIExample()
        {
            var snack = new MaterialSnackbar()
            {
                LeadingIcon = "horus_logo.png",
                Text = "Lorem ipsum dolor sit amet",
                TextColor = Colors.Aqua,
                ActionText = "Action API",
                ActionTextColor = Colors.Fuchsia,
                ActionFontSize = 28,
                TrailingIcon = "horus_logo.png"
            };
            var snackbarState = Snackbar.Make(snack, test, test, test);
            await snackbarState.Show();
        }

        private async void test()
        {
            await DisplayAlert(Title, "Work!!!", "OK"); 
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

