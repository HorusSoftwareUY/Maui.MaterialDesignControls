using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class MainViewModel : BaseViewModel
    {
        #region Attributes & Properties

        [ObservableProperty]
        IEnumerable<MenuItemViewModel> _menuItems;

        public override string Title => string.Empty;

        #endregion

        public MainViewModel()
        {
            MenuItems = new List<MenuItemViewModel>
            {
                new MenuItemViewModel { Title = "Buttons", Icon = "ic_button.png", ViewModel = typeof(ButtonViewModel) },
                new MenuItemViewModel { Title = "Dividers", Icon = "ic_divider.png", ViewModel = typeof(DividerViewModel) },
                new MenuItemViewModel { Title = "Labels", Icon = "ic_label.png", ViewModel = typeof(LabelViewModel) },
                new MenuItemViewModel { Title = "Progress indicators", Icon = "ic_progress_indicator.png", ViewModel = typeof(ProgressIndicatorViewModel) },
                new MenuItemViewModel { Title = "Icon buttons", Icon = "ic_icon_button.png", ViewModel = typeof(IconButtonViewModel) },
                new MenuItemViewModel { Title = "Radio button", Icon = "ic_radio.png", ViewModel = typeof(RadioButtonViewModel) },
                new MenuItemViewModel { Title = "Slider", Icon = "ic_slider.png", ViewModel = typeof(SliderViewModel) }
            };
        }

        [ICommand]
        private async Task AboutUsAsync()
        {
            await GoToAsync<AboutViewModel>();
        }
    }

    public partial class MenuItemViewModel : ObservableObject
    {
        [ObservableProperty]
        string _title;

        [ObservableProperty]
        ImageSource _icon;

        [ObservableProperty]
        Type _viewModel;
    }
}