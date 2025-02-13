using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class SnackbarViewModel : BaseViewModel
    {
        #region Attributes & Properties
        
        private readonly IMaterialSnackbar _snackbar;
        private CancellationTokenSource? _tokenSource;
        
        public override string Title => Models.Pages.Snackbar;
        protected override string ControlReferenceUrl => "components/snackbar/overview";

        #endregion

        public SnackbarViewModel(IMaterialSnackbar snackbar)
        {
            _snackbar = snackbar;
            Subtitle = "Snackbars show short updates about app processes at the bottom of the screen";
        }

        [ICommand]
        private async void DefaultSnackbar()
        {
            _snackbar.Show(new SnackbarConfig("Default snackbar with custom action")
            {
                Action = new SnackbarConfig.ActionConfig("OK", SnackbarAction)
            });
        }
        
        [ICommand]
        private async void DismissedCallbackSnackbar()
        {
            _snackbar.Show(new SnackbarConfig("Snackbar without action")
            {
                OnDismissed = async() => await DisplayAlert(Title, "Snackbar dismissed", "OK")
            });
        }
        
        [ICommand]
        private async Task SnackbarWithIconsAsync()
        {
            _tokenSource = new CancellationTokenSource();
            
            await _snackbar.ShowAsync(new SnackbarConfig("Lorem ipsum dolor sit amet")
            {
                LeadingIcon = new SnackbarConfig.IconConfig("info.png", SnackbarLeading)
                {
                    Color = Colors.SteelBlue
                },
                TrailingIcon = new SnackbarConfig.IconConfig("ic_close.png", CloseSnackbar),
                Duration = TimeSpan.FromSeconds(10)
            }, _tokenSource.Token);
        }
        
        [ICommand]
        private async Task FullApiSnackbarAsync()
        {
            _tokenSource = new CancellationTokenSource();
            _tokenSource.CancelAfter(TimeSpan.FromSeconds(3));
            await _snackbar.ShowAsync(new SnackbarConfig("10 seconds duration cancelled at 3 seconds")
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
            }, _tokenSource.Token);
        }
        
        private void CloseSnackbar()
        {
            if (_tokenSource == null) return;
            _tokenSource.Cancel();
            _tokenSource = null;
        }
        
        private async void SnackbarLeading()
        {
            await DisplayAlert(Title, "Leading icon tapped!", "OK");
        }
        
        private async void SnackbarTrailing()
        {
            await DisplayAlert(Title, "Trailing icon tapped!", "OK");
        }
        
        private async void SnackbarAction()
        {
            await DisplayAlert(Title, "Action tapped!", "OK");
        }
    }
}

