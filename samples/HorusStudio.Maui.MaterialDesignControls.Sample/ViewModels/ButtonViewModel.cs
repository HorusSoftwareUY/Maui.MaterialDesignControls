using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class ButtonViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Buttons";

        [ObservableProperty]
        private bool _buttonEnabled = true;

        #endregion

        public ButtonViewModel()
        {
            Subtitle = "Buttons help people take action, such as sending an email, sharing a document, or liking a comment.";
        }

        [ICommand]
        private async Task MaterialButton()
        {
            IsBusy = true;
            await Task.Delay(3000);
            //await DisplayAlert($"{Title}", $"MaterialButton command executed!", "Ok");
            IsBusy = false;
        }
    }
}

