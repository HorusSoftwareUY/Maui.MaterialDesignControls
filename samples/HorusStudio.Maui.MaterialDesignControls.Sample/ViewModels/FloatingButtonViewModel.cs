using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class FloatingButtonViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Floating Button";
        protected override string ControlReferenceUrl => "components/floating-action-button/overview";
        
        [ObservableProperty]
        private MaterialFloatingButtonPosition _positionButton = MaterialFloatingButtonPosition.BottomRight;
        
        [ObservableProperty]
        private MaterialFloatingButtonType _buttonType = MaterialFloatingButtonType.Large;

        [ObservableProperty]
        private bool _isVisible = true;

        [ObservableProperty]
        private bool _isEnabled = true;
        
        [ObservableProperty]
        private List<PositionModel> _positions;
        
        [ObservableProperty]
        private List<TypeModel> _types;
        
        #endregion

        public FloatingButtonViewModel()
        {
            Subtitle = "Use a FAB for the most important action on a screen. The FAB appears in front of all other content on screen, and is recognizable for its rounded shape and icon in the center.";

            Positions = new List<PositionModel>();
            foreach (var position in Enum.GetValues<MaterialFloatingButtonPosition>().ToList())
            {
                Positions.Add(new PositionModel() { Name = position.ToString(), Position = position });
            }
            
            Types = new List<TypeModel>();
            foreach (var type in Enum.GetValues<MaterialFloatingButtonType>().ToList())
            {
                Types.Add(new TypeModel() { Name = type.ToString(), Type = type });
            }
        }

        [ICommand]
        private async Task FloatingButtonAction()
        {
            await DisplayAlert("FAB", "FAB was tapped!", "OK");
        }

        [ICommand]
        private async Task NavigateAsync()
        {
            await GoToAsync<IconButtonViewModel>();
        }
    }
    
    public class PositionModel
    {
        public string Name { get; set; }
        public MaterialFloatingButtonPosition Position { get; set; }
    }

    public class TypeModel
    {
        public string Name { get; set; }
        public MaterialFloatingButtonType Type { get; set; }
    }
}
