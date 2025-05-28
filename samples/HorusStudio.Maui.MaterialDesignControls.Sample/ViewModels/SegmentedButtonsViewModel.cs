using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class SegmentedButtonsViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.SegmentedButtons;
        protected override string ControlReferenceUrl => "components/segmented-buttons/overview";

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(TextButtonTypeSelected))]
        private MaterialSegmentedButtonsType _segmentedType;

        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButton> _items;
        
        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButton> _items2;
        
        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButton> _items3;
        
        [ObservableProperty]
        private MaterialSegmentedButton _selectedItem;
        
        [ObservableProperty]
        private IEnumerable<MaterialSegmentedButton> _selectedItems;

        [ObservableProperty]
        private string _textItemsSelectedFilled;
        
        public string TextButtonTypeSelected => SegmentedType == MaterialSegmentedButtonsType.Filled ? "Outlined" : "Filled";

        [ObservableProperty] 
        [AlsoNotifyChangeFor(nameof(ButtonDisableText))]
        private bool _fullApiIsEnabled = true;
        
        public string ButtonDisableText => (FullApiIsEnabled ? "Disable" : "Enable");

        #endregion

        public SegmentedButtonsViewModel()
        {
            Subtitle = "Segmented buttons help people select options, switch views, or sort elements.";
            
            SegmentedType = MaterialSegmentedButtonsType.Filled;
            
            Items = new ObservableCollection<MaterialSegmentedButton> 
            {
                new MaterialSegmentedButton
                {
                    Text = "Opt1",
                    IsSelected = true,
                },
                new MaterialSegmentedButton
                {
                    Text = "Opt2",
                },
                new MaterialSegmentedButton
                {
                    Text = "Opt3",
                }
            };
            
            Items2 = new ObservableCollection<MaterialSegmentedButton>
            {
                new MaterialSegmentedButton
                {
                    IsSelected = true,
                    Text = "Opt1",
                },
                new MaterialSegmentedButton
                {
                    Text = "Opt2",
                },
                new MaterialSegmentedButton
                {
                    Text = "Opt3",
                }
            };
            
            Items3 = new ObservableCollection<MaterialSegmentedButton>
            {
                new MaterialSegmentedButton
                {
                    Text = "Opt1",
                    SelectedIcon = "ic_checkbox.png",
                    UnselectedIcon = "logo.png"
                },
                new MaterialSegmentedButton
                {
                    Text = "Opt2",
                    SelectedIcon = "ic_checkbox.png",
                },
                new MaterialSegmentedButton
                {
                    Text = "Opt3",
                    SelectedIcon = "ic_checkbox.png"
                }
            };

            OnItemMultipleSelected();
        }
        
        [ICommand]
        private async Task OnItemSelectedOutlinedAsync()    
        {
            await DisplayAlert(Title, $"Button selected: {SelectedItem.Text}", "OK");
        }

        [ICommand]
        private void OnItemMultipleSelected()
        {
            var selectedText = "-";
            if (_items2.Any(w => w.IsSelected))
            {
                selectedText = string.Join(", ", Items2.Where(w => w.IsSelected).Select(s => s.Text));
            }
            TextItemsSelectedFilled = $"Selected: {selectedText}";
        }

        [ICommand]
        private void OnDisabledSegmented()
        {
            FullApiIsEnabled = !FullApiIsEnabled;
        }

        [ICommand]
        private void OnChangeTypeSegmented()
        {
            SegmentedType = SegmentedType == MaterialSegmentedButtonsType.Filled ? MaterialSegmentedButtonsType.Outlined : MaterialSegmentedButtonsType.Filled;
        }
    }
}