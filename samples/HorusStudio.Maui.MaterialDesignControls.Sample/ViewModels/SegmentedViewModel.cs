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

        public override string Title => "Segmented button";

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
        private bool _fullApiIsEnabled = false;

        #endregion

        public SegmentedViewModel()
        {
            Subtitle = "Segmented buttons help people select options, switch views, or sort elements.";
        }
        
        public override void Appearing()
        {
            base.Appearing();
            
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
                    SelectedIcon = "horus_logo.png",
                    UnselectedIcon = "horus_studio_logo.png"
                },
                new MaterialSegmentedItem
                {
                    Text = "Opt2",
                    SelectedIcon = "ic_checkbox.png",
                },
                new MaterialSegmentedItem
                {
                    Text = "Opt3",
                    SelectedIcon = "logo.png"
                }
            };
            
            OnItemMultipleSelectedCommand.Execute(null);
        }

        [ICommand]
        private async Task OnItemSelectedOutlinedAsync()    
        {
            await DisplayAlert(Title, $"Button selected: {SelectedItem.Text}", "OK");
        }

        [ICommand]
        private void OnItemMultipleSelected()
        {
            TextItemsSelectedFilled = $"Selected: " + string.Join(", ", Items2.Where(w => w.IsSelected).Select(s => s.Text));
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

