using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class ChipsViewModel : BaseViewModel
    {
        #region Attributes & Properties

        private const string SuggestionText = "Suggestion";
        private const string FilterText = "Filter";

        public override string Title => "Chips";

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(TextChipSuggestion))]
        [AlsoNotifyChangeFor(nameof(TextChipFilter))]
        private bool _isEnabledState;

        public string TextChipSuggestion => (IsEnabledState) ? SuggestionText : $"{SuggestionText} disabled";
        
        public string TextChipFilter => (IsEnabledState) ? FilterText : $"{FilterText} disabled";

        #endregion

        public ChipsViewModel()
        {
            Subtitle = "Chips help people enter information, make selections, filter content, or trigger actions";
        }

        [ICommand]
        private void SetEnabled()
        {
            IsEnabledState = !IsEnabledState;
        }
    }
}

