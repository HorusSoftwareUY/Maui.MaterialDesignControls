using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class ChipsViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Chips";

        [ObservableProperty]
        private bool _isEnabledState;

        #endregion

        public ChipsViewModel()
        {
            Subtitle = "";
        }

        [ICommand]
        private void SetEnabled()
        {
            IsEnabledState = !IsEnabledState;
        }
    }
}

