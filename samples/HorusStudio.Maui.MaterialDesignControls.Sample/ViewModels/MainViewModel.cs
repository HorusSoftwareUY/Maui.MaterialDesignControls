using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class MainViewModel : BaseViewModel
    {
        #region Attributes & Properties


        [ObservableProperty]
        private ObservableCollection<string> _sizes;

        [ObservableProperty]
        private ObservableCollection<string> _selectedSizes;

        [ObservableProperty]
        private string _selectedSize;

        [ObservableProperty]
        private ObservableCollection<Person> _users;

        [ObservableProperty]
        private List<object> _selectedUsers;

        [ObservableProperty]
        private Person _selectedUser;

        [ObservableProperty]
        private ObservableCollection<string> _colors;

        [ObservableProperty]
        private List<string> _selectedColors;

        [ObservableProperty]
        IEnumerable<MenuItemViewModel> _menuItems;

        [ObservableProperty]
        private string _error;

        public override string Title => string.Empty;

        #endregion

        public MainViewModel()
        {
            Users = new ObservableCollection<Person> { new Person { Age = 60, Name = "Alfonso"}, new Person { Age = 24, Name = "Sofía" }, new Person { Age = 32, Name = "Pedro" }, new Person { Age = 18, Name = "Ana" }, };

            Sizes = new ObservableCollection<string> { "6", "6.5", "7", "7.5", "8" };

            Colors = new ObservableCollection<string> { "Red", "White", "Green", "Sky blue", "Black", "Gray", "Light Gray" };

            MenuItems = new List<MenuItemViewModel>
            {
                new MenuItemViewModel { Title = "Buttons", Icon = "ic_button.png", ViewModel = typeof(ButtonViewModel) },
                new MenuItemViewModel { Title = "Dividers", Icon = "ic_divider.png", ViewModel = typeof(DividerViewModel) },
                new MenuItemViewModel { Title = "Labels", Icon = "ic_label.png", ViewModel = typeof(LabelViewModel) },
                new MenuItemViewModel { Title = "Progress indicators", Icon = "ic_progress_indicator.png", ViewModel = typeof(ProgressIndicatorViewModel) },
                new MenuItemViewModel { Title = "Icon buttons", Icon = "ic_icon_button.png", ViewModel = typeof(IconButtonViewModel) },
                new MenuItemViewModel { Title = "Cards", Icon = "ic_card.png", ViewModel = typeof(CardViewModel) },
                new MenuItemViewModel { Title = "Radio button", Icon = "ic_radio.png", ViewModel = typeof(RadioButtonViewModel) },
                new MenuItemViewModel { Title = "Checkbox", Icon = "ic_checkbox.png", ViewModel = typeof(CheckboxViewModel) },
                new MenuItemViewModel { Title = "Badge", Icon = "ic_badge.png", ViewModel = typeof(BadgeViewModel) },
                new MenuItemViewModel { Title = "Rating", Icon = "ic_rating.png", ViewModel = typeof(RatingViewModel) },
                new MenuItemViewModel { Title = "Chips", Icon = "ic_chip.png", ViewModel = typeof(ChipsViewModel) }
            };
        }

        [ICommand]
        private async Task AboutUsAsync()
        {
            await GoToAsync<AboutViewModel>();
        }

        [ICommand]
        private void ValidateSelections()
        {
            var size = SelectedSize;
            var colors = SelectedColors;
            var user = SelectedUser;
            var users = SelectedUsers;
            var sizes = SelectedSizes;
            Error = "esosss";
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

    public partial class Person : ObservableObject
    {
        [ObservableProperty]
        private int _age;

        [ObservableProperty]
        private string _name;
    }
}