using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class MainPageViewModel : ObservableObject
    {
        private const string _title = "MaterialDesignControls";

        public delegate Task DisplayAlertType(string title, string message, string cancel);

        public DisplayAlertType DisplayAlert { get; set; }

        [ICommand]
        private async Task MaterialButton()
        {
            await Task.Delay(2000);
            await DisplayAlert(_title, $"MaterialButton command executed!", "Ok");
        }
    }
}