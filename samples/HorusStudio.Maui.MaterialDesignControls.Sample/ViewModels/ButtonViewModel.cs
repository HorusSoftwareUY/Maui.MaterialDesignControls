using System;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class ButtonViewModel : BaseViewModel
	{
        #region Attributes & Properties

        public override string Title => "Buttons";

        #endregion

        public ButtonViewModel()
        {
            Subtitle = "Buttons help people take action, such as sending an email, sharing a document, or liking a comment.";
        }

        [ICommand]
        private async Task MaterialButton()
        {
            await Task.Delay(2000);
            await DisplayAlert($"Material Design Controls > {Title}", $"MaterialButton command executed!", "Ok");
        }
    }
}

