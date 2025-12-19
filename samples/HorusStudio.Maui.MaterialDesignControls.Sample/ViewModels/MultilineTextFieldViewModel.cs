using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class MultilineTextFieldViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.MultilineTextField;
        protected override string ControlReferenceUrl => "components/text-fields/overview";

        [ObservableProperty]
        private string _supportingTextValue = "Enter the value.";

        [ObservableProperty]
        private string _text = "";

        [ObservableProperty]
        private bool _hasAnError = false;

        [ObservableProperty]
        private string _observation = "";

        #endregion

        public MultilineTextFieldViewModel()
        {
            Subtitle = "Multiline text fields let people enter text into a UI. They typically appear in forms and dialogs.";
            Observation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur nisl tellus, elementum sit amet semper vel, fermentum vitae turpis. Integer vel auctor orci.";
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
    }
}

