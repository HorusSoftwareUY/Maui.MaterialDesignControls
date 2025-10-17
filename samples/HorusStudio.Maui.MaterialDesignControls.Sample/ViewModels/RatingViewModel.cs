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
                "mood_bad_c",
                "sentiment_dissatisfied_c",
                "sentiment_neutral_c",
                "sentiment_satisfied_c",
                "mood_c",
            };
            
            if (Application.Current?.RequestedTheme == AppTheme.Dark)
            {
                UnselectedIcons = new ObservableCollection<ImageSource>
                {
                    "mood_bad_d",
                    "sentiment_dissatisfied_d",
                    "sentiment_neutral_d",
                    "sentiment_satisfied_d",
                    "mood_d",
                };
            }
            else
            {
                UnselectedIcons = new ObservableCollection<ImageSource>
                {
                    "mood_bad_l",
                    "sentiment_dissatisfied_l",
                    "sentiment_neutral_l",
                    "sentiment_satisfied_l",
                    "mood_l",
                };
            }
        }


        [ICommand]
        private async Task CheckValue()
        {
            await DisplayAlert(Title, $"The value is {Value}", "OK");
        }

        [ICommand]
        private async Task ValueChanged(int newValue)
        {
            await DisplayAlert(Title, $"The new value is {newValue}", "OK");
        }

        [ICommand]
        private async Task ChangeValueTo3()
        {
            Value = 3;
        }
    }
}