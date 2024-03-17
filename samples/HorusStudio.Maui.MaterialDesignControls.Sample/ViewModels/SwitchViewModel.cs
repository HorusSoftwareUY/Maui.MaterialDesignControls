using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class SwitchViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Switches";

        [ObservableProperty]
        private bool _isToggled = true;

        #endregion

        public SwitchViewModel()
        {
            Subtitle = "Switches toggle the selection of an item on or off.";
        }

        [ICommand]
        private async Task SwitchTapped()
        {
            await DisplayAlert(Title, "Switch selection changed (ToggledCommand)", "OK");
        }
    }
}