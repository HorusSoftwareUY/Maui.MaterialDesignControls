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
            _snackbarUser.Show(new SnackbarConfig("This is snackbar")
            {
                Action = new SnackbarConfig.ActionConfig("Action", SnackbarAction),
                OnDismissed = async() => await DisplayAlert(Title, "Snackbar dismissed", "OK")
            });
        }
        
        [ICommand]
        private async void ShowSnackbarIconExample()
        {
            _snackbarUser.Show(new SnackbarConfig("Lorem ipsum dolor sit amet")
            {
                LeadingIcon = new SnackbarConfig.IconConfig("horus_logo.png", SnackbarLeading)
                {
                    Color = Colors.Red
                },
                TrailingIcon = new SnackbarConfig.IconConfig("horus_logo.png", SnackbarTrailing),
                Action = new SnackbarConfig.ActionConfig("Action", SnackbarAction)
            });
        }
        
        [ICommand]
        private async Task ShowSnackbarFullApiExampleAsync()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeSpan.FromSeconds(3));
            await _snackbarUser.ShowAsync(new SnackbarConfig("10 seconds duration cancelled at 3 seconds")
            {
                LeadingIcon = new SnackbarConfig.IconConfig("horus_logo.png", SnackbarLeading)
                {
                    Color = Colors.Red,
                    Size = 35
                },
                TrailingIcon = new SnackbarConfig.IconConfig("horus_logo.png", SnackbarTrailing)
                {
                    Color = Colors.Coral,
                    Size = 35
                },
                Action = new SnackbarConfig.ActionConfig("Action API", SnackbarAction)
                {
                    Color = Colors.Fuchsia,
                    FontSize = 18
                },
                Duration = TimeSpan.FromSeconds(10),
                TextColor = Colors.Aqua,
                BackgroundColor = Colors.Green
            }, tokenSource.Token);
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

