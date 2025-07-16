using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class BottomSheetViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Bottom Sheets";

        public Action<string> OpenBottomSheetControl { get; set; }
        public Action<string> CloseBottomSheetControl { get; set; }

        #endregion

        public BottomSheetViewModel()
        {
            Subtitle = "Bottom sheets show secondary content anchored to the bottom of the screen.";
        }

        [ICommand]
        private async Task OpenBottomSheet(string controlName)
        {
            OpenBottomSheetControl?.Invoke(controlName);
        }

        [ICommand]
        private async Task CloseBottomSheetAsync(string controlName)
        {
            CloseBottomSheetControl?.Invoke(controlName);
        }
    }
}
