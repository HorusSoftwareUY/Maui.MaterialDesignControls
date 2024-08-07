using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class ChipsViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Chips";

        [ObservableProperty]
        private bool _isEnabledState;
        
        [ObservableProperty]
        private bool _chipsFilterA;
        
        [ObservableProperty]
        private bool _chipsFilterB;
        
        [ObservableProperty]
        private bool _chipsFilterC;
        
        [ObservableProperty]
        private bool _chipsFilterD;

        #endregion

        public ChipsViewModel()
        {
            Subtitle = "Chips help people enter information, make selections, filter content, or trigger actions";
        }

        [ICommand]
        private async void TappedChips()
        {
            await DisplayAlert(Title, "Chips tapped!", "OK");
        }
        
        [ICommand]
        private async void TappedChipsFilter(string chip)
        {
            switch (chip)
            {
                case "A":
                    ChipsFilterA = !ChipsFilterA;
                    await DisplayAlert(Title, $"Chips A {((ChipsFilterA) ? "selected" : "not selected")}", "OK");
                    break;
                case "B":
                    ChipsFilterB = !ChipsFilterB;
                    await DisplayAlert(Title, $"Chips B {((ChipsFilterB) ? "selected" : "not selected")}", "OK");
                    break;
                case "C":
                    ChipsFilterC = !ChipsFilterC;
                    await DisplayAlert(Title, $"Chips C {((ChipsFilterC) ? "selected" : "not selected")}", "OK");
                    break;
                case "D":
                    ChipsFilterD = !ChipsFilterD;
                    await DisplayAlert(Title, $"Chips D {((ChipsFilterD) ? "selected" : "not selected")}", "OK");
                    break;
            }
            
        }
    }
}

