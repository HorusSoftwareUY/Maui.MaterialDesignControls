﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class DatePickerViewModel : BaseViewModel
    {
        #region Attributes & Properties
        public override string Title => Models.Pages.DatePicker;
        protected override string ControlReferenceUrl => "components/date-pickers/overview";

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

        public DatePickerViewModel()
        {
            Subtitle = "Date pickers let people select a date. They typically appear in forms and dialogs.";
        }

        [ICommand]
        private void CheckTextField()
        {
            SupportingTextValue = "Select a date.";
            HasAnError = false;

            if (!Date.HasValue)
            {
                SupportingTextValue = "You should select a valid date.";
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
