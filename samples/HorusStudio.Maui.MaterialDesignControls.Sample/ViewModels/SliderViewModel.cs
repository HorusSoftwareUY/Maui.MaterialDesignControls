using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class SliderViewModel : BaseViewModel
    {
        #region Attributes & Properties

        [ObservableProperty]
        private double _value = 0.5;


        [ObservableProperty]
        private string _valueFormat = "{0:0.0}º C";

        public override string Title => Models.Pages.Slider;

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
