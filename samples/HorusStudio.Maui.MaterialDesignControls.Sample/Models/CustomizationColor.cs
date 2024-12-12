using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Models
{
    public partial class CustomizationColor : ObservableObject
    {
        public Color Color { get; set; }
        public string TextColor { get; set; }

        [ObservableProperty]
        private bool _isSelected;
        public bool IsLight { get; set; }
        public Color IconTinColor => IsLight ? Colors.Black : Colors.White;
    }
}
