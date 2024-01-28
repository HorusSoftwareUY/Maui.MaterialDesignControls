using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class TopAppBarViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Top app bars";

        [ObservableProperty]
        private bool _iconButtonsEnabled = true;

        #endregion

        public TopAppBarViewModel()
        {
            Subtitle = "Top app bars display navigation, actions, and text at the top of a screen.";
        }

        [ICommand]
        private async Task LeadingIconTap()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await DisplayAlert(Title, "Leading icon clicked!", "OK");
        }

        [ICommand]
        private async Task TrailingIconTap()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await DisplayAlert(Title, "Trailing icon clicked!", "OK");
        }
    }
}