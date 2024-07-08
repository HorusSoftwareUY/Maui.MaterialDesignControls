using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class TextFieldViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Text fields";

        [ObservableProperty]
        private string _supportingTextValue = "";

        [ObservableProperty]
        private string _text = "";

        #endregion

        public TextFieldViewModel()
        {
            Subtitle = "Text fields let users enter text into a UI. They typically appear in forms and dialogs.";
        }

        [ICommand]
        private void CheckTextField()
        {
            SupportingTextValue = String.Empty;

            if (string.IsNullOrWhiteSpace(Text))
            {
                SupportingTextValue = "You should enter a value";
            }
        }
    }
}

