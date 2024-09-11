using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class BottomSheetViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Bottom Sheets";

        [ObservableProperty]
        private TextDecorations _decorations = TextDecorations.None;

        #endregion

        public BottomSheetViewModel()
        {
            Subtitle = "Bottom sheets show secondary content anchored to the bottom of the screen.";
        }

        [ICommand]
        private async Task MaterialButton1(string message)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await DisplayAlert(Title, message ?? "Clicked!", "OK");
        }

        [ICommand]
        private Task MaterialButton2(string message) => MaterialButton1(message);
    }
}
