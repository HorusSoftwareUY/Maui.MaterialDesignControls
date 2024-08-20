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
            _snackbarUser.ShowSnackbar(new SnackbarConfig()
            {
                LeadingIcon = "horus_logo.png",
                TrailingIcon = "horus_logo.png",
                Message = "This is snackbar",
                ActionText = "Cancel",
                Action = type =>
                {
                    test();
                }
            });
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
            //var snackbarState = Snackbar.Make(snack, test, test, test);
            //await snackbarState.Show();
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
            //var snackbarState = Snackbar.Make(snack, test, test, test);
            //await snackbarState.Show();
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

