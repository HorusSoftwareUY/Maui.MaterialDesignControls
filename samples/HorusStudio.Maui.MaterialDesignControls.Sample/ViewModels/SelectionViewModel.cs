using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class SelectionViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Selection";

        [ObservableProperty]
        private string _supportingTextValue = "Enter the value.";

        [ObservableProperty]
        public string _selectedText;

        [ObservableProperty]
        private bool _hasAnError = false;

        #endregion

        public SelectionViewModel()
        {
            Subtitle = "Selection controls allow the user to select options.";
        }

        [ICommand]
        private async Task Tap(object parameter)
        {
            string text = parameter.ToString();
            await DisplayAlert(Title, text, "OK");
            SelectedText = text;
        }

        [ICommand]
        private void CheckTextField()
        {
            SupportingTextValue = "Select user.";
            HasAnError = false;

            if (string.IsNullOrWhiteSpace(SelectedText))
            {
                SupportingTextValue = "You should select a valid value.";
                HasAnError = true;
            }
        }
    }
}
