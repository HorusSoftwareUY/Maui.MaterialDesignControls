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

        public override string Title => "Segmented";

        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedItem> _items;
        
        [ObservableProperty]
        private MaterialSegmentedItem _selectedItem;
        
        [ObservableProperty]
        private IEnumerable<MaterialSegmentedItem> _selectedItems;

        #endregion

        public SegmentedViewModel()
        {
            
        }
        
        public override void Appearing()
        {
            base.Appearing();
            Items = new ObservableCollection<MaterialSegmentedItem>
            {
                new MaterialSegmentedItem { Text = "test", SelectedIcon = "ic_segmented.png", SelectedIconColor = Colors.Blue},
                new MaterialSegmentedItem { Text = "test2", UnselectedIcon = "ic_button.png", UnselectedIconColor = Colors.Red},
                new MaterialSegmentedItem { Text = "test3"},
            };
        }

        [ICommand]
        private void OnItemSelectedAsync()
        {
            Console.WriteLine($"OnItemSelectedAsync item: {SelectedItem}");
            Console.WriteLine($"OnItemSelectedAsync list item: {SelectedItems}");
        }
    }
}

