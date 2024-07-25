using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class BadgeViewModel : BaseViewModel
	{
        #region Attributes & Properties

        public override string Title => "MaterialBadge";

        [ObservableProperty]
        private string _counter = string.Empty;

        #endregion

        public BadgeViewModel()
        {
            Subtitle = "Badges show notifications, counts, or status information on navigation items and icons";
        }

        public override void Appearing()
        {
            base.Appearing();
            ChangeCounterCommand.Execute(null);
        }

        [ICommand]
        private void ChangeCounter()
        {
            int numberRandom = GenerateRamdomNumber();
            Counter = (numberRandom == 1000) ? "999+" : $"{numberRandom}";
        }

        private int GenerateRamdomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1, 1000);
        }
    }
}