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

            var actionGroup = new MenuGroup() { GroupName = "Actions" };
            actionGroup.Add(new MenuItemViewModel { Title = "Buttons", Icon = "ic_button.png", ViewModel = typeof(ButtonViewModel) });
            actionGroup.Add(new MenuItemViewModel { Title = "Icon buttons", Icon = "ic_icon_button.png", ViewModel = typeof(IconButtonViewModel) });
            actionGroup.Add(new MenuItemViewModel { Title = "Segmented", Icon = "ic_segmented.png", ViewModel = typeof(SegmentedViewModel) });

            var communicationGroup = new MenuGroup() { GroupName = "Communication" };
            communicationGroup.Add(new MenuItemViewModel { Title = "Badge", Icon = "ic_badge.png", ViewModel = typeof(BadgeViewModel) });
            communicationGroup.Add(new MenuItemViewModel { Title = "Progress indicators", Icon = "ic_progress_indicator.png", ViewModel = typeof(ProgressIndicatorViewModel) });

            var containmentGroup = new MenuGroup() { GroupName = "Containment" };
            containmentGroup.Add(new MenuItemViewModel { Title = "Cards", Icon = "ic_card.png", ViewModel = typeof(CardViewModel) });
            containmentGroup.Add(new MenuItemViewModel { Title = "Dividers", Icon = "ic_divider.png", ViewModel = typeof(DividerViewModel) });

            var navigationGroup = new MenuGroup() { GroupName = "Navigation" };
            navigationGroup.Add(new MenuItemViewModel { Title = "Top app bars", ViewModel = typeof(TopAppBarViewModel) });

            var selectionGroup = new MenuGroup() { GroupName = "Selection" };
            selectionGroup.Add(new MenuItemViewModel { Title = "Chips", Icon = "ic_chip.png", ViewModel = typeof(ChipsViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Checkbox", Icon = "ic_checkbox.png", ViewModel = typeof(CheckboxViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Radio button", Icon = "ic_radio.png", ViewModel = typeof(RadioButtonViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Rating", Icon = "ic_rating.png", ViewModel = typeof(RatingViewModel) });

            var textGroup = new MenuGroup() { GroupName = "Text inputs" };
            textGroup.Add(new MenuItemViewModel { Title = "Labels", Icon = "ic_label.png", ViewModel = typeof(LabelViewModel) });

            groups.Add(actionGroup);
            groups.Add(communicationGroup);
            groups.Add(containmentGroup);
            groups.Add(navigationGroup);
            groups.Add(selectionGroup);
            groups.Add(textGroup);

            MenuGroups = groups;
        }

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