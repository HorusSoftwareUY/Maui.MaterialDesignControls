using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class TextFieldViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Text fields";

        [ObservableProperty]
        private bool _isEnabled = true;

        #endregion

        public TextFieldViewModel()
        {
            Subtitle = "Text fields let users enter text into a UI. They typically appear in forms and dialogs.";
        }
    }
}

