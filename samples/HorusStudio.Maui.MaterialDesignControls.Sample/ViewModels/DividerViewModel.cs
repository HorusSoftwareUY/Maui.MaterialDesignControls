using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class DividerViewModel : BaseViewModel
	{
        #region Attributes & Properties

        public override string Title => "Dividers";

        [ObservableProperty]
        private bool _isCustomize;

        [ObservableProperty]
        private double _dividerHeight = 3;

        [ObservableProperty]
        private Color _dividerColor = Colors.Green;

        #endregion

        public DividerViewModel()
        {
            Subtitle = "Dividers are one way to visually group components and create hierarchy. They can also be used to imply nested parent/child relationships.";
        }

        [ICommand]
        private void ChangeAppearance()
        {
            DividerColor = DividerHeight == 3 ? Colors.DarkBlue : Colors.Green;
            DividerHeight = DividerHeight == 3 ? 6 : 3;
        }
    }
}