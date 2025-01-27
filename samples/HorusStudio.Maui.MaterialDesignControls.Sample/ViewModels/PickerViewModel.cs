using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class PickerViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.Picker;

        [ObservableProperty]
        private string _supportingTextValue = "Select a color.";

        [ObservableProperty]
        private bool _hasAnError = false;

        [ObservableProperty]
        private ObservableCollection<int> _itemsSource;

        [ObservableProperty]
        private ObservableCollection<CustomColor> _colorsSource;

        [ObservableProperty]
        private CustomColor _selectedColor;

        [ObservableProperty]
        private CustomColor _selectedDisabledColor;

        #endregion

        public PickerViewModel()
        {
            Subtitle = "Pickers let people select an option from a list of values. They typically appear in forms and dialogs.";
            ItemsSource = new ObservableCollection<int>
            {
                30,31,32,33,34,35,36,37
            };

            ColorsSource = new ObservableCollection<CustomColor>
            {
                new CustomColor()
                {
                    Color = "Red",
                    Id = 1
                },
                new CustomColor()
                {
                    Color = "Blue",
                    Id = 2
                },
                new CustomColor()
                {
                    Color = "Green",
                    Id = 3
                }
            };

            SelectedDisabledColor = ColorsSource.First();
        }

        [ICommand]
        private void CheckTextField()
        {
            SupportingTextValue = "Select a color.";
            HasAnError = false;

            if (SelectedColor is null)
            {
                SupportingTextValue = "You should select a color.";
                HasAnError = true;
            }
        }

        [ICommand]
        private void LeadingAction()
        {
            DisplayAlert("Leading", "Command for leading icon.", "OK");
        }

        [ICommand]
        public async Task Show()
        {
            if (SelectedColor != null)
                await DisplayAlert("Color", SelectedColor.Color, "Ok");
            else
                await DisplayAlert("Color", "No color selected", "Ok");
        }

        [ICommand]
        public async Task ClearSelectedColor()
        {
            SelectedColor = null;
        }

        [ICommand]
        public void AddNewColor()
        {
            ColorsSource.Add(new CustomColor { Id = ColorsSource.Count + 1, Color = $"New Color {ColorsSource.Count + 1}" });
        }
    }
}
