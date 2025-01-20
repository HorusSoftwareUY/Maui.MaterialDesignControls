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

            var communicationGroup = new MenuGroup() { GroupName = "Communication" };
            communicationGroup.Add(new MenuItemViewModel { Title = "Badges", Icon = "ic_badge.png", ViewModel = typeof(BadgeViewModel) });
            communicationGroup.Add(new MenuItemViewModel { Title = "Progress indicators", Icon = "ic_progress_indicator.png", ViewModel = typeof(ProgressIndicatorViewModel) });

            var containmentGroup = new MenuGroup() { GroupName = "Containment" };
            containmentGroup.Add(new MenuItemViewModel { Title = "Cards", Icon = "ic_card.png", ViewModel = typeof(CardViewModel) });
            containmentGroup.Add(new MenuItemViewModel { Title = "Dividers", Icon = "ic_divider.png", ViewModel = typeof(DividerViewModel) });
            containmentGroup.Add(new MenuItemViewModel { Title = "Navigation drawer", Icon = "ic_navigation_drawer.png", ViewModel = typeof(NavigationDrawerViewModel) });

            var navigationGroup = new MenuGroup() { GroupName = "Navigation" };
            navigationGroup.Add(new MenuItemViewModel { Title = "Top app bars", ViewModel = typeof(TopAppBarViewModel) });

            var selectionGroup = new MenuGroup() { GroupName = "Selection" };
            selectionGroup.Add(new MenuItemViewModel { Title = "Chips", Icon = "ic_chip.png", ViewModel = typeof(ChipsViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Checkboxes", Icon = "ic_checkbox.png", ViewModel = typeof(CheckboxViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Radio buttons", Icon = "ic_radio.png", ViewModel = typeof(RadioButtonViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Rating", Icon = "ic_rating.png", ViewModel = typeof(RatingViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Switches", Icon = "ic_switch.png", ViewModel = typeof(SwitchViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Sliders", Icon = "ic_slider.png", ViewModel = typeof(SliderViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Picker", Icon = "ic_picker.png", ViewModel = typeof(PickerViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Date picker", Icon = "ic_date.png", ViewModel = typeof(DatePickerViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Time picker", Icon = "ic_time.png", ViewModel = typeof(TimePickerViewModel) });
            selectionGroup.Add(new MenuItemViewModel { Title = "Selection", Icon = "ic_selection.png", ViewModel = typeof(SelectionViewModel) });

            var textGroup = new MenuGroup() { GroupName = "Text inputs" };
            textGroup.Add(new MenuItemViewModel { Title = "Labels", Icon = "ic_label.png", ViewModel = typeof(LabelViewModel) });
            textGroup.Add(new MenuItemViewModel { Title = "Text fields", Icon = "ic_entry.png", ViewModel = typeof(TextFieldViewModel) });
            textGroup.Add(new MenuItemViewModel { Title = "Multiline text fields", Icon = "ic_editor.png", ViewModel = typeof(MultilineTextFieldViewModel) });

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