using System;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class ChipsViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.Chip;
        protected override string ControlReferenceUrl => "components/chips/overview";

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

        [ObservableProperty]
        private string _selectedChipValue;
        
        [ObservableProperty]
        private string _selectedChipText;
        
        [ObservableProperty]
        private ObservableCollection<string> _chips;
        
        [ObservableProperty]
        private string _selectedChipItem;
        
        #endregion

        public ChipsViewModel()
        {
            Chips = new ObservableCollection<string> { "Chip A", "Chip B", "Chip C", "Chip D", "Chip E" };
            Subtitle = "Chips help people enter information, make selections, filter content, or trigger actions";
            
            SelectedChipText = "Selected value: -";
        }

        [ICommand]
        private async void TappedChips()
        {
            await DisplayAlert(Title, "Chips tapped!", "OK");
        }

        [ICommand]
        private async void ShowFilterSelection()
        {
            if (ChipsFilterA)
            {
                await DisplayAlert(Title, "Chip A selected", "OK");
            }
            else if (ChipsFilterB)
            {
                await DisplayAlert(Title, "Chip B selected", "OK");
            }
            else if (ChipsFilterC)
            {
                await DisplayAlert(Title, "Chip C selected", "OK");
            }
            else if (ChipsFilterD)
            {
                await DisplayAlert(Title, "Chip D selected", "OK");
            }
            else
            {
                await DisplayAlert(Title, "No chip selected", "OK");
            }
        }

        [ICommand]
        private async Task SelectChipC()
        {
            SelectedChipValue = "Option C";
        }
        
        [ICommand]
        private async Task SelectedValueChanged()
        {
            if (!string.IsNullOrEmpty(SelectedChipValue))
            {
                SelectedChipText = $"Selected value: {SelectedChipValue}";
            }
            else
            {
                SelectedChipText = "Selected value: -";
            }
        }
        
        [ICommand]
        private async Task ShowSelectedItem()
        {
            var message = !string.IsNullOrEmpty(SelectedChipItem) ? $"Selected item: {SelectedChipItem}" : "No item selected";
            await DisplayAlert(Title, message, "OK");
        }
    }
}