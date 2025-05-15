using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class SegmentedViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.SegmentedButton;
        protected override string ControlReferenceUrl => "components/segmented-buttons/overview";

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(TextButtonTypeSelected))]
        private MaterialSegmentedType _segmentedType;

        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedItem> _items;
        
        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedItem> _items2;
        
        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedItem> _items3;
        
        [ObservableProperty]
        private MaterialSegmentedItem _selectedItem;
        
        [ObservableProperty]
        private IEnumerable<MaterialSegmentedItem> _selectedItems;

        [ObservableProperty]
        private string _textItemsSelectedFilled;
        
        public string TextButtonTypeSelected => SegmentedType == MaterialSegmentedType.Filled ? "Outlined" : "Filled";

        [ObservableProperty] 
        [AlsoNotifyChangeFor(nameof(ButtonDisableText))]
        private bool _fullApiIsEnabled = true;
        
        public string ButtonDisableText => (FullApiIsEnabled ? "Disable" : "Enable");

        #endregion

        public SegmentedViewModel()
        {
            Subtitle = "Segmented buttons help people select options, switch views, or sort elements.";
            
            SegmentedType = MaterialSegmentedType.Filled;
            
            Items = new ObservableCollection<MaterialSegmentedItem> 
            {
                new MaterialSegmentedItem
                {
                    Text = "Opt1",
                    IsSelected = true,
                },
                new MaterialSegmentedItem
                {
                    Text = "Opt2",
                },
                new MaterialSegmentedItem
                {
                    Text = "Opt3",
                }
            };
            
            Items2 = new ObservableCollection<MaterialSegmentedItem>
            {
                new MaterialSegmentedItem
                {
                    IsSelected = true,
                    Text = "Opt1",
                },
                new MaterialSegmentedItem
                {
                    Text = "Opt2",
                },
                new MaterialSegmentedItem
                {
                    Text = "Opt3",
                }
            };
            
            Items3 = new ObservableCollection<MaterialSegmentedItem>
            {
                new MaterialSegmentedItem
                {
                    Text = "Opt1",
                    SelectedIcon = "ic_checkbox.png",
                    UnselectedIcon = "logo.png"
                },
                new MaterialSegmentedItem
                {
                    Text = "Opt2",
                    SelectedIcon = "ic_checkbox.png",
                },
                new MaterialSegmentedItem
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
            SegmentedType = SegmentedType == MaterialSegmentedType.Filled ? MaterialSegmentedType.Outlined : MaterialSegmentedType.Filled;
        }
    }
}

