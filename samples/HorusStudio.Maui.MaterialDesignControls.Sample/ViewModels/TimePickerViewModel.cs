using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class TimePickerViewModel : BaseViewModel
    {
        #region Attributes & Properties
        public override string Title => "Date picker";

        [ObservableProperty]
        private string _supportingTextValue = "Select a date.";

        [ObservableProperty]
        private DateTime? _date = null;

        [ObservableProperty]
        private bool _hasAnError = false;

        [ObservableProperty]
        private DateTime _minimumDate = DateTime.Today.AddYears(-1);

        [ObservableProperty]
        private DateTime _maximumDate = DateTime.Today.AddMonths(-1);

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

            if (!Date.HasValue)
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
        private void ClearDate()
        {
            Date = null;
        }
    }
}
