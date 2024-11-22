using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class BadgeViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "MaterialBadge";

        [ObservableProperty]
        private bool _isCustomize;

        [ObservableProperty]
        private string _counter = string.Empty;

        #endregion

        public BadgeViewModel()
        {
            Subtitle = "Badges show notifications, counts, or status information on navigation items and icons";
            Counter = "1";
        }

        [ICommand]
        private void ChangeCounter()
        {
            Counter = Counter switch
            {
                "" => "1",
                "1" => "10",
                "10" => "100",
                "100" => "999+",
                "999+" => string.Empty,
                _ => Counter
            };
        }
    }
}