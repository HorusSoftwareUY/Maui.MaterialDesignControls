using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class SwitchViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Switchs";

        [ObservableProperty]
        private string _errorText;

        [ObservableProperty]
        private bool _termsAndConditionsAccepted = false;

        #endregion

        public SwitchViewModel()
        {
            Subtitle = "Switches toggle the selection of an item on or off.";
        }

        [ICommand]
        private async Task Submit()
        {
            if (TermsAndConditionsAccepted)
            {
                ErrorText = string.Empty;
            }
            else
            {
                ErrorText = "You must accept the terms and conditions";
            }
        }

        [ICommand]
        private async Task SwitchTapped()
        {
            await DisplayAlert(Title, "Switch selection changed (ToggledCommand)", "OK");
        }
    }
}