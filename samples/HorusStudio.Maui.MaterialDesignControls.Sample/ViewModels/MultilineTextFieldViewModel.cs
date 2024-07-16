using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class MultilineTextFieldViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Multiline Text field";

        [ObservableProperty]
        private string _supportingTextValue = "Enter the value.";

        [ObservableProperty]
        private string _text = "";

        [ObservableProperty]
        private bool _hasAnError = false;

        #endregion

        public MultilineTextFieldViewModel()
        {
            Subtitle = "Multiline Text fields let users enter text into a UI. They typically appear in forms and dialogs.";
        }

        [ICommand]
        private void CheckTextField()
        {
            SupportingTextValue = "Enter the value.";
            HasAnError = false;

            if (string.IsNullOrWhiteSpace(Text))
            {
                SupportingTextValue = "You should enter a valid value.";
                HasAnError = true;
            }
        }

        [ICommand]
        private void LeadingAction()
        {
            DisplayAlert("Leading", "Command for leading icon.", "OK");
        }

        [ICommand]
        private void Return()
        {
            DisplayAlert("Return", "Command for return type.", "OK");
        }
    }
}

