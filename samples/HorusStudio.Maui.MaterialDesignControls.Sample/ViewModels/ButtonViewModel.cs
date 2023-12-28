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

        [ObservableProperty]
        private TextDecorations _decorations = TextDecorations.None;

        #endregion

        public ButtonViewModel()
        {
            Subtitle = "Buttons help people take action, such as sending an email, sharing a document, or liking a comment.";
        }

        [ICommand]
        private async Task MaterialButton1(string message)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await DisplayAlert(Title, message ?? "Clicked!", "OK");
        }

        [ICommand]
        private Task MaterialButton2(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton3(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton4(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton5(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton6(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton7(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton8(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton9(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton10(string message) => MaterialButton1(message);

        [ICommand]
        private void MaterialButton11(string message)
        {
            Decorations = (TextDecorations)(((int)Decorations + 1) % 3);
        }

        [ICommand]
        private Task MaterialButton12(string message) => MaterialButton1(message);
    }
}

