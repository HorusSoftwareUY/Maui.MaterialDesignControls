using System;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class IconButtonViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.IconButton;

        #endregion

        public IconButtonViewModel()
        {
            Subtitle = "Icon buttons help people take minor actions with one tap.";
        }

        [ICommand]
        private async Task MaterialIconButton1(string message)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await DisplayAlert(Title, message ?? "Clicked!", "OK");
        }

        [ICommand]
        private Task MaterialIconButton2(string message) => MaterialIconButton1(message);
        [ICommand]
        private Task MaterialIconButton3(string message) => MaterialIconButton1(message);
        [ICommand]
        private Task MaterialIconButton4(string message) => MaterialIconButton1(message);
        [ICommand]
        private Task MaterialIconButton5(string message) => MaterialIconButton1(message);
        [ICommand]
        private Task MaterialIconButton6(string message) => MaterialIconButton1(message);
    }
}

