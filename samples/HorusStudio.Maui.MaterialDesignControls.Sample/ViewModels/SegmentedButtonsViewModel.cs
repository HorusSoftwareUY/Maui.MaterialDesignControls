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
        private ObservableCollection<MaterialSegmentedButtonsItem> _items;
        
        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButtonsItem> _items2;
        
        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButtonsItem> _items3;

        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButtonsItem> _items4;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(SelectedItemText))]
        private MaterialSegmentedButtonsItem _selectedItem;

        [ObservableProperty]
        private IEnumerable<MaterialSegmentedButtonsItem> _selectedItems;

        public string? SelectedItemText => SelectedItem != null ? $"SelectedItem: {SelectedItem.Text}" : "-";

        public string TextButtonTypeSelected => SegmentedType == MaterialSegmentedButtonsType.Filled ? "Outlined" : "Filled";

        [ObservableProperty] 
        [AlsoNotifyChangeFor(nameof(ButtonDisableText))]
        private bool _fullApiIsEnabled = true;
        
        public string ButtonDisableText => (FullApiIsEnabled ? "Disable" : "Enable");

        #endregion

        public SegmentedButtonsViewModel()
        {
            Subtitle = "Segmented buttons help people select options, switch views, or sort elements.";
            
            SegmentedType = MaterialSegmentedButtonsType.Outlined;
            
            Items = new ObservableCollection<MaterialSegmentedButtonsItem> 
            {
                new MaterialSegmentedButtonsItem("Opt1")
                {
                    SelectedIcon = "star_selected",
                    UnselectedIcon = "star_unselected",
                    IsSelected = true,
                },
                new MaterialSegmentedButtonsItem("Opt2")
                {
                    SelectedIcon = "star_selected",
                    UnselectedIcon = "star_unselected"
                },
                new MaterialSegmentedButtonsItem("Opt3")
                {
                    SelectedIcon = "star_selected",
                    UnselectedIcon = "star_unselected"
                }
            };
            
            Items2 = new ObservableCollection<MaterialSegmentedButtonsItem>
            {
                new MaterialSegmentedButtonsItem("Opt1")
                {
                    SelectedIcon = "ic_checkbox.png",
                    IsSelected = true,
                },
                new MaterialSegmentedButtonsItem("Opt2")
                {
                    SelectedIcon = "ic_checkbox.png"
                },
                new MaterialSegmentedButtonsItem("Opt3")
                {
                    SelectedIcon = "ic_checkbox.png"
                }
            };
            
            Items3 = new ObservableCollection<MaterialSegmentedButtonsItem>
            {
                new MaterialSegmentedButtonsItem("Opt1"),
                new MaterialSegmentedButtonsItem("Opt2"),
                new MaterialSegmentedButtonsItem("Opt3")
            };

            Items4 = new ObservableCollection<MaterialSegmentedButtonsItem>
            {
                new MaterialSegmentedButtonsItem("Opt1")
                {
                    SelectedIcon = "horus_logo",
                    ApplyIconTintColor = false,
                    IsSelected = true,
                },
                new MaterialSegmentedButtonsItem("Opt2")
                {
                    SelectedIcon = "email"
                },
                new MaterialSegmentedButtonsItem("Opt3")
                {
                    SelectedIcon = "ic_date"
                }
            };

            SelectedItem = Items.First();
        }

        [ICommand]
        private async Task OnSingleItemSelection(MaterialSegmentedButtonsItem item)    
        {
            await DisplayAlert(Title, $"Segmented button selected: {item.Text}", "OK");
        }

        [ICommand]
        private async Task OnMultipleItemSelection(IEnumerable<MaterialSegmentedButtonsItem> items)
        {
            var selectedText = "-";
            if (items != null && items.Any())
            {
                selectedText = string.Join(", ", items.Select(x => x.Text));
            }

            await DisplayAlert(Title, $"Segmented buttons selected: {selectedText}", "OK");
        }

        [ICommand]
        private void SelectLastItem()
        {
            SelectedItem = Items.Last();
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