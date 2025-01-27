using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class SelectionViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.Selection;

        [ObservableProperty]
        private string _supportingTextValue = "Enter the value.";

        [ObservableProperty]
        private string _selectedText;

        [ObservableProperty]
        private bool _hasAnError = false;

        #endregion

        public SelectionViewModel()
        {
            Subtitle = "Selection control allow people choose an option in a custom way with same look & feel than pickers and text fields. They typically appear in forms and dialogs.";
        }

        [ICommand]
        private async Task Tap(object parameter)
        {
            const string cancel = "Cancel";
            const string destruction = "Clear";
            var selected = await DisplayActionSheet(Title, cancel, destruction, "User 1", "User 2", "User 3");
            if (selected != null && selected != cancel)
            {
                SelectedText = selected == destruction ? null : selected;
            }
        }

        [ICommand]
        private void CheckTextField()
        {
            SupportingTextValue = "Select user";
            HasAnError = false;

            if (string.IsNullOrWhiteSpace(SelectedText))
            {
                SupportingTextValue = "Please select a valid value";
                HasAnError = true;
            }
        }
    }
}
