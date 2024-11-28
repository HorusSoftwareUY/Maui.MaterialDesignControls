using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Models
{
    public partial class CustomizationItem : ObservableObject
    {
        public string Name { get; set; }
        public object Value { get; set; }

        [ObservableProperty]
        private bool _isSelected;
    }
}