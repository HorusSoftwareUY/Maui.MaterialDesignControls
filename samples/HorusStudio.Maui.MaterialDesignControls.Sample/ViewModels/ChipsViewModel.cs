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
        private ObservableCollection<string> _chips;

        [ObservableProperty]
        private List<string> _selectedChips = new();

        [ObservableProperty]
        private string _selectedChip;

        [ObservableProperty]
        private string _selectionWithCommandText = "SelectionChangedCommand";

        [ObservableProperty]
        private bool _hasAnError;
        
        [ObservableProperty]
        private string _supportingTextValue;
        
        #endregion

        public ChipsViewModel()
        {
            Chips = new ObservableCollection<string> { "Chip A", "Chip B", "Chip C", "Chip D", "Chip E" };
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

        [ICommand]
        private async Task SelectChipC()
        {
            SelectedChip = "Chip C";
        }

        [ICommand]
        private async Task ShowSelectedChip()
        {
            SupportingTextValue = null;
            HasAnError = false;

            if (string.IsNullOrWhiteSpace(SelectedChip))
            {
                SupportingTextValue = "You must select one option.";
                HasAnError = true;
            }
            else
            {
                await DisplayAlert(Title, $"SelectedChip: {SelectedChip}", "OK");
            }
        }

        [ICommand]
        private async Task SelectChipAAndB()
        {
            SelectedChips = new List<string> { "Chip A", "Chip B" };
        }

        [ICommand]
        private async Task ShowSelectedChips()
        {
            var result = SelectedChips != null && SelectedChips.Any() ? string.Join(", ", SelectedChips) : "-";
            await DisplayAlert(Title, $"SelectedChips: {result}", "OK");
        }

        [ICommand]
        private async Task SelectionChanged(List<string> selectedItems)
        {
            var result = selectedItems != null && selectedItems.Any() ? string.Join(", ", selectedItems) : "-";
            SelectionWithCommandText = $"Selection: {result}";
        }
    }
}