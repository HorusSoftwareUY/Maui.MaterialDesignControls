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
                new MenuItemViewModel { Title = "Buttons", ViewModel = typeof(ButtonViewModel) },
                new MenuItemViewModel { Title = "Dividers", ViewModel = typeof(DividerViewModel) },
                new MenuItemViewModel { Title = "Labels", ViewModel = typeof(LabelViewModel) },
                new MenuItemViewModel { Title = "Progress indicators", ViewModel = typeof(ProgressIndicatorViewModel) },
                new MenuItemViewModel { Title = "Icon buttons", ViewModel = typeof(IconButtonViewModel) },
                new MenuItemViewModel { Title = "Cards", ViewModel = typeof(CardViewModel) },
                new MenuItemViewModel { Title = "Radio button", Icon = "ic_radio.png", ViewModel = typeof(RadioButtonViewModel) }
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