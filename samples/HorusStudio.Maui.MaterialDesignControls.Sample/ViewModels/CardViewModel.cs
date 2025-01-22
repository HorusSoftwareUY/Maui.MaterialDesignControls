using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class CardViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.Card;

        private int _index = 0;
        private List<Color> _backgroundColors = new List<Color> { Colors.LightCyan, Colors.LightCoral, Colors.LightGreen };
        private List<Color> _borderColors = new List<Color> { Colors.DarkGreen, Colors.LightYellow, Colors.DarkMagenta };
        private List<Color> _shadowColors = new List<Color> { Colors.DarkRed, Colors.Black, Colors.DarkGoldenrod };

        [ObservableProperty]
        private bool _isCustomize;

        [ObservableProperty]
        private Color _backgroundColor;

        [ObservableProperty]
        private Color _borderColor;

        [ObservableProperty]
        private Color _shadowColor;

        [ObservableProperty]
        private bool _cardEnabled = true;

        #endregion

        public CardViewModel()
        {
            Subtitle = "Cards display content and actions about a single subject";

            BackgroundColor = _backgroundColors[_index];
            BorderColor = _borderColors[_index];
            ShadowColor = _shadowColors[_index];
        }

        [ICommand]
        private async Task Tap()
        {
            await DisplayAlert(Title, "Card clicked!", "OK");
        }

        [ICommand]
        private void ChangeColors()
        {
            if (++_index == 3)
            {
                _index = 0;
            }
            BackgroundColor = _backgroundColors[_index];
            BorderColor = _borderColors[_index];
            ShadowColor = _shadowColors[_index];
        }
    }
}