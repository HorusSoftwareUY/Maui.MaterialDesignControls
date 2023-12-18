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
        private async Task MaterialButton1()
        {
            await Task.Delay(3000);
        }

        [ICommand]
        private Task MaterialButton2() => MaterialButton1();
        [ICommand]
        private Task MaterialButton3() => MaterialButton1();
        [ICommand]
        private Task MaterialButton4() => MaterialButton1();
        [ICommand]
        private Task MaterialButton5() => MaterialButton1();
        [ICommand]
        private Task MaterialButton6() => MaterialButton1();
        [ICommand]
        private Task MaterialButton7() => MaterialButton1();
        [ICommand]
        private Task MaterialButton8() => MaterialButton1();
        [ICommand]
        private Task MaterialButton9() => MaterialButton1();
        [ICommand]
        private Task MaterialButton10() => MaterialButton1();
        [ICommand]
        private Task MaterialButton11() => MaterialButton1();
        [ICommand]
        private Task MaterialButton12() => MaterialButton1();
    }
}

