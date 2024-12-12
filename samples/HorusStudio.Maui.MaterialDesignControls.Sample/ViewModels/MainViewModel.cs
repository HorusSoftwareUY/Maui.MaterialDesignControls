using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        #region Attributes & Properties

        [ObservableProperty]
        List<MenuGroup> _menuGroups;

        public override string Title => string.Empty;

        #endregion

        public MainViewModel()
        {
            var groups = new List<MenuGroup>();

            var initialGroup = new MenuGroup() { GroupName = "" };
            initialGroup.Add(new MenuItemViewModel { Title = "Components overview", ViewModel = typeof(MainViewModel) });
            initialGroup.Add(new MenuItemViewModel { Title = "Change appearance", ViewModel = typeof(AppearanceViewModel) });

            var actionGroup = new MenuGroup() { GroupName = "Actions" };
            actionGroup.Add(new MenuItemViewModel { Title = "Buttons", ViewModel = typeof(ButtonViewModel) });
            actionGroup.Add(new MenuItemViewModel { Title = "Icon buttons", ViewModel = typeof(IconButtonViewModel) });

            var communicationGroup = new MenuGroup() { GroupName = "Communication" };
            communicationGroup.Add(new MenuItemViewModel { Title = "Badge", ViewModel = typeof(BadgeViewModel) });
            communicationGroup.Add(new MenuItemViewModel { Title = "Progress indicators", ViewModel = typeof(ProgressIndicatorViewModel) });

            var containmentGroup = new MenuGroup() { GroupName = "Containment" };
            containmentGroup.Add(new MenuItemViewModel { Title = "Cards", ViewModel = typeof(CardViewModel) });
            containmentGroup.Add(new MenuItemViewModel { Title = "Dividers", ViewModel = typeof(DividerViewModel) });

            var navigationGroup = new MenuGroup() { GroupName = "Navigation" };
            navigationGroup.Add(new MenuItemViewModel { Title = "Top app bars", ViewModel = typeof(TopAppBarViewModel) });

            var selectionGroup = new MenuGroup() { GroupName = "Selection" };
            selectionGroup.Add(new MenuItemViewModel { Title = "Chips", ViewModel = typeof(ChipsViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Checkbox", ViewModel = typeof(CheckboxViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Radio button", ViewModel = typeof(RadioButtonViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Rating", ViewModel = typeof(RatingViewModel) });

            var textGroup = new MenuGroup() { GroupName = "Text inputs" };
            textGroup.Add(new MenuItemViewModel { Title = "Labels", ViewModel = typeof(LabelViewModel) });

            groups.Add(initialGroup);
            groups.Add(actionGroup);
            groups.Add(communicationGroup);
            groups.Add(containmentGroup);
            groups.Add(navigationGroup);
            groups.Add(selectionGroup);
            groups.Add(textGroup);

            MenuGroups = groups;
        }

        #region Commands

        [ICommand]
        private async Task AboutUsAsync()
        {
            await GoToAsync<AboutViewModel>();
        }

        [ICommand]
        private async Task NavigateAsync(MenuItemViewModel item)
        {
            await GoToAsync(item.ViewModel.Name);
        }

        #endregion
    }

    public partial class MenuItemViewModel : ObservableObject
    {
        [ObservableProperty]
        string _title;

        [ObservableProperty]
        Type _viewModel;
    }
}