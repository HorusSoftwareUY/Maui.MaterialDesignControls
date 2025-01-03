using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class FloatingButtonViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Floating Button";

        [ObservableProperty] 
        private string _buttonTextMove = string.Empty;
        
        [ObservableProperty] 
        private string _buttonTextType = string.Empty;
        
        [ObservableProperty]
        private MaterialFloatingButtonPosition _positionButton;
        
        [ObservableProperty]
        private MaterialFloatingButtonType _buttonType;
        

        #endregion

        public FloatingButtonViewModel()
        {
            Subtitle = "Use a FAB for the most important action on a screen. The FAB appears in front of all other content on screen, and is recognizable for its rounded shape and icon in the center.";
            PositionButton = MaterialFloatingButtonPosition.BottomRight;
            ButtonTextMove = "Move to bottom left";
            ButtonTextType = "Small";
        }

        [ICommand]
        private async Task FloatingButtonAction()
        {
            await DisplayAlert("Floating button", "Press floating button", "OK");
        }

        [ICommand]
        private void ChangeTypeFloatingButton()
        {
            if (ButtonType == MaterialFloatingButtonType.FAB)
            {
                ButtonType = MaterialFloatingButtonType.Small;
                ButtonTextType = "Large";
            }
            else if (ButtonType == MaterialFloatingButtonType.Small)
            {
                ButtonType = MaterialFloatingButtonType.Large;
                ButtonTextType = "FAB";
            }
            else if (ButtonType == MaterialFloatingButtonType.Large)
            {
                ButtonType = MaterialFloatingButtonType.FAB;
                ButtonTextType = "Small";
            }
        }

        [ICommand]
        private void MoveButton()
        {
            switch (PositionButton)
            {
                case MaterialFloatingButtonPosition.BottomRight:
                    PositionButton = MaterialFloatingButtonPosition.BottomLeft;
                    ButtonTextMove = "Move to top left";
                    break;
                case MaterialFloatingButtonPosition.BottomLeft:
                    PositionButton = MaterialFloatingButtonPosition.TopLeft;
                    ButtonTextMove = "Move to top right";
                    break;
                case MaterialFloatingButtonPosition.TopLeft:
                    PositionButton = MaterialFloatingButtonPosition.TopRight;
                    ButtonTextMove = "Move to bottom right";
                    break;
                default:
                    PositionButton = MaterialFloatingButtonPosition.BottomRight;
                    ButtonTextMove = "Move to bottom left";
                    break;
            }
        }
    }
}

