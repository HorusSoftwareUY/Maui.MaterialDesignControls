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
        private MaterialFloatingButtonPosition _positionButton;
        

        #endregion

        public FloatingButtonViewModel()
        {
            Subtitle = "Use a FAB for the most important action on a screen. The FAB appears in front of all other content on screen, and is recognizable for its rounded shape and icon in the center.";
            PositionButton = MaterialFloatingButtonPosition.BottomRight;
            ButtonTextMove = "Move to bottom left";
        }


        [ICommand]
        private void MoveButton()
        {
            if (PositionButton == MaterialFloatingButtonPosition.BottomRight)
            {
                PositionButton = MaterialFloatingButtonPosition.BottomLeft;
                ButtonTextMove = "Move to top left";
            }
            else if (PositionButton == MaterialFloatingButtonPosition.BottomLeft)
            {
                PositionButton = MaterialFloatingButtonPosition.TopLeft;
                ButtonTextMove = "Move to top right";
            }
            else if (PositionButton == MaterialFloatingButtonPosition.TopLeft)
            {
                PositionButton = MaterialFloatingButtonPosition.TopRight;
                ButtonTextMove = "Move to bottom right";
            }
            else
            {
                PositionButton = MaterialFloatingButtonPosition.BottomRight;
                ButtonTextMove = "Move to bottom left";
            }

        }
    }
}

