using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class RatingViewModel : BaseViewModel
    {
        #region Attributes & Properties

        [ObservableProperty]
        private IEnumerable<ImageSource> _selectedIcons;

        [ObservableProperty]
        private IEnumerable<ImageSource> _unselectedIcons;

        [ObservableProperty]
        private int _value = 2;

        public override string Title => Models.Pages.Rating;

        #endregion

        public RatingViewModel()
        {
            Subtitle = "This control allow people to view and set rating values that reflect degrees of satisfaction.";

            SelectedIcons = new ObservableCollection<ImageSource>
            {
                "verybad.png",
                "bad.png",
                "middle.png",
                "good.png",
                "verygood.png",
            };

            UnselectedIcons = new ObservableCollection<ImageSource>
            {
                "verybad_unselected.png",
                "bad_unselected.png",
                "middle_unselected.png",
                "good_unselected.png",
                "verygood_unselected.png",
            };
        }


        [ICommand]
        private async Task CheckValue()
        {
            await DisplayAlert(Title, $"The value is {Value}", "OK");
        }
    }
}
