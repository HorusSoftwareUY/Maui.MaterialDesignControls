using System;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class TextFieldViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Text fields";

        #endregion

        public TextFieldViewModel()
        {
            Subtitle = "Text fields let users enter text into a UI. They typically appear in forms and dialogs.";
        }
    }
}

