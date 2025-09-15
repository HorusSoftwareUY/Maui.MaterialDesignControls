using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

public partial class BottomSheetTestViewModel : BaseViewModel
{
        public override string Title => "Models.Pages.Card";
        
        [ICommand]
        public async Task ShowSnackBar()
        {
            _snackbar.Show(new MaterialSnackbarConfig("Default snackbar with custom action")
            {
                Action = new MaterialSnackbarConfig.ActionConfig("Close action", SnackbarAction),
            });
        }
        
        private readonly IMaterialSnackbar _snackbar;

        public BottomSheetTestViewModel(IMaterialSnackbar snackbar)
        {
            _snackbar = snackbar;
        }
        
        private async void SnackbarAction()
        {
            await DisplayAlert(Title, "Action tapped!", "OK");
        }
        
    
}