using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class TimePickerViewModel : BaseViewModel
    {
        #region Attributes & Properties
        public override string Title => "Time picker";

        [ObservableProperty]
        private string _supportingTextValue = "Select a time.";

        [ObservableProperty]
        private TimeSpan? _time = null;

        [ObservableProperty]
        private bool _hasAnError = false;

        #endregion

        public TimePickerViewModel()
        {
            Subtitle = "Time pickers let users select a time. They typically appear in forms and dialogs.";
        }

        [ICommand]
        private void CheckTextField()
        {
            SupportingTextValue = "Select a time.";
            HasAnError = false;

            if (!Time.HasValue)
            {
                SupportingTextValue = "You should select a valid time.";
                HasAnError = true;
            }
        }

        [ICommand]
        private void LeadingAction()
        {
            DisplayAlert("Leading", "Command for leading icon.", "OK");
        }

        [ICommand]
        private void ClearTime()
        {
            Time = null;
        }
    }
}
