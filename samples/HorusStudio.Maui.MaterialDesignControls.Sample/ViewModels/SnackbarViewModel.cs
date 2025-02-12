using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class SnackbarViewModel : BaseViewModel
    {
        #region Attributes & Properties
        
        private readonly IMaterialSnackbar _snackbarUser;
        
        public override string Title => "Snackbar";

        #endregion

        public SnackbarViewModel(IMaterialSnackbar snackbarUser)
        {
            _snackbarUser = snackbarUser;
            Subtitle = "Snackbars show short updates about app processes at the bottom of the screen";
        }

        [ICommand]
        private async void ShowSnackbarExample()
        {
            _snackbarUser.ShowSnackbar(new SnackbarConfig("This is snackbar")
            {
                Action = new SnackbarConfig.ActionConfig("Action"){ Action = SnackbarAction }
            });
        }
        
        [ICommand]
        private async void ShowSnackbarIconExample()
        {
            _snackbarUser.ShowSnackbar(new SnackbarConfig("Lorem ipsum dolor sit amet")
            {
                LeadingIcon = new SnackbarConfig.IconConfig("horus_logo.png")
                {
                    Color = Colors.Red,
                    Action = SnackbarLeading
                },
                TrailingIcon = new SnackbarConfig.IconConfig("horus_logo.png")
                {
                    Action = SnackbarTrailing
                },
                Action = new SnackbarConfig.ActionConfig("Action")
                {
                    Action = SnackbarAction
                }
            });
        }
        
        [ICommand]
        private async void ShowSnackbarFullAPIExample()
        {
            _snackbarUser.ShowSnackbar(new SnackbarConfig("Lorem ipsum dolor sit amet")
            {
                LeadingIcon = new SnackbarConfig.IconConfig("horus_logo.png")
                {
                    Action = SnackbarLeading,
                    Color = Colors.Red,
                    Size = 35
                },
                TrailingIcon = new SnackbarConfig.IconConfig("horus_logo.png")
                {
                    Action = SnackbarTrailing,
                    Color = Colors.Coral,
                    Size = 35
                },
                Action = new SnackbarConfig.ActionConfig("Action API")
                {
                    Action = SnackbarAction,
                    Color = Colors.Fuchsia,
                    FontSize = 18
                },
                TextColor = Colors.Aqua,
                BackgroundColor = Colors.Green
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

