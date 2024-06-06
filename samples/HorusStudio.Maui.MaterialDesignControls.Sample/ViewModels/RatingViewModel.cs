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


        public override string Title => "Rating";

        #endregion

        public RatingViewModel()
        {
            Subtitle = "This control allows users to view and set ratings that reflect degrees of satisfaction.";

            SelectedIcons = new ObservableCollection<ImageSource>
            {
                "square_checked.png",
                "square_checked.png",
                "star_selected.png",
            };

            UnselectedIcons = new ObservableCollection<ImageSource>
            {
                "square_unchecked.png",
                "square_unchecked.png",
                "star_unselected.png",
            };
        }


        [ICommand]
        private async Task CheckValue()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await DisplayAlert(Title, $"The value is {Value}", "OK");
        }
    }
}
