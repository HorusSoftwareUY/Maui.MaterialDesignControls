using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class SliderViewModel : BaseViewModel
    {
        #region Attributes & Properties

        [ObservableProperty]
        private double _value = 0.5;

        public override string Title => "Slider";

        #endregion Attributes & Properties

        public SliderViewModel()
        {
            Subtitle = "Sliders let users make selections from a range of values";
        }

        [ICommand]
        private async Task CheckValue()
        {
            await DisplayAlert(Title, $"The value is {Value}", "OK");
        }
    }
}
