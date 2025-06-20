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
        private MaterialSegmentedButtonType _segmentedType;

        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButtonItem> _items;

        [ObservableProperty]
        private MaterialSegmentedButtonItem _selectedItem;

        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButtonItem> _items2;

        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButtonItem> _selectedItems2;

        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButtonItem> _items3;

        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButtonItem> _items4;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(SelectedItemText))]
        private MaterialSegmentedButtonItem _selectedItem4;

        [ObservableProperty]
        private IEnumerable<MaterialSegmentedButtonItem> _selectedItems;

        public string? SelectedItemText => SelectedItem4 != null ? $"SelectedItem: {SelectedItem4.Text}" : "-";

        public string TextButtonTypeSelected => SegmentedType == MaterialSegmentedButtonType.Filled ? "Outlined" : "Filled";

        [ObservableProperty] 
        [AlsoNotifyChangeFor(nameof(ButtonDisableText))]
        private bool _fullApiIsEnabled = true;
        
        public string ButtonDisableText => (FullApiIsEnabled ? "Disable" : "Enable");

        #endregion

        public SegmentedButtonsViewModel()
        {
            Subtitle = "Segmented buttons help people select options, switch views, or sort elements.";
            
            SegmentedType = MaterialSegmentedButtonType.Outlined;
            
            Items = new ObservableCollection<MaterialSegmentedButtonItem> 
            {
                new MaterialSegmentedButtonItem("Opt1")
                {
                    SelectedIcon = "star_selected",
                    UnselectedIcon = "star_unselected"
                },
                new MaterialSegmentedButtonItem("Opt2")
                {
                    SelectedIcon = "star_selected",
                    UnselectedIcon = "star_unselected"
                },
                new MaterialSegmentedButtonItem("Opt3")
                {
                    SelectedIcon = "star_selected",
                    UnselectedIcon = "star_unselected"
                }
            };

            SelectedItem = Items[0];
            
            Items2 = new ObservableCollection<MaterialSegmentedButtonItem>
            {
                new MaterialSegmentedButtonItem("Opt1")
                {
                    SelectedIcon = "ic_checkbox.png"
                },
                new MaterialSegmentedButtonItem("Opt2")
                {
                    SelectedIcon = "ic_checkbox.png"
                },
                new MaterialSegmentedButtonItem("Opt3")
                {
                    SelectedIcon = "ic_checkbox.png"
                }
            };

            SelectedItems2 = new ObservableCollection<MaterialSegmentedButtonItem> { Items2[0] };

            Items3 = new ObservableCollection<MaterialSegmentedButtonItem>
            {
                new MaterialSegmentedButtonItem("Opt1"),
                new MaterialSegmentedButtonItem("Opt2"),
                new MaterialSegmentedButtonItem("Opt3")
            };

            Items4 = new ObservableCollection<MaterialSegmentedButtonItem>
            {
                new MaterialSegmentedButtonItem("Opt1")
                {
                    SelectedIcon = "horus_logo",
                    ApplyIconTintColor = false
                },
                new MaterialSegmentedButtonItem("Opt2")
                {
                    SelectedIcon = "email"
                },
                new MaterialSegmentedButtonItem("Opt3")
                {
                    SelectedIcon = "star_selected",
                    UnselectedIcon = "star_unselected"
                }
            };

            SelectedItem4 = Items4[0];
        }

        [ICommand]
        private async Task OnSingleItemSelection(MaterialSegmentedButtonItem item)    
        {
            await DisplayAlert(Title, $"Segmented button selected: {item.Text}", "OK");
        }

        [ICommand]
        private async Task OnMultipleItemSelection(IEnumerable<MaterialSegmentedButtonItem> items)
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
            SelectedItem4 = Items4.Last();
        }

        [ICommand]
        private void OnDisabledSegmented()
        {
            FullApiIsEnabled = !FullApiIsEnabled;
        }

        [ICommand]
        private void OnChangeTypeSegmented()
        {
            SegmentedType = SegmentedType == MaterialSegmentedButtonType.Filled ? MaterialSegmentedButtonType.Outlined : MaterialSegmentedButtonType.Filled;
        }
    }
}