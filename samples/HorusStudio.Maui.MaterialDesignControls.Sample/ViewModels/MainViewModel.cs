using System;
using System.Collections.Generic;
using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        #region Attributes & Properties
        
        [ObservableProperty]
        private IEnumerable<MaterialNavigationDrawerItem> _menuItems;

        public override string Title => string.Empty;

        #endregion

        private readonly IDictionary<string, Type> _viewmodelTypeMap = new Dictionary<string, Type>()
        {
            { Models.Pages.Button, typeof(ButtonViewModel) },
            { Models.Pages.FloatingButton, typeof(FloatingButtonViewModel) },
            { Models.Pages.IconButton, typeof(IconButtonViewModel) },
            { Models.Pages.SegmentedButtons, typeof(SegmentedButtonsViewModel) },
            { Models.Pages.Badge, typeof(BadgeViewModel) },
            { Models.Pages.ProgressIndicator, typeof(ProgressIndicatorViewModel) },
            { Models.Pages.Snackbar, typeof(SnackbarViewModel) },
            { Models.Pages.BottomSheet, typeof(BottomSheetViewModel) },
            { Models.Pages.Card, typeof(CardViewModel) },
            //{ Models.Pages.Dialog, typeof(DialogViewModel) },
            { Models.Pages.Divider, typeof(DividerViewModel) },
            { Models.Pages.NavigationDrawer, typeof(NavigationDrawerViewModel) },
            { Models.Pages.TopAppBar, typeof(TopAppBarViewModel) },
            { Models.Pages.Checkbox, typeof(CheckboxViewModel) },
            { Models.Pages.Chip, typeof(ChipsViewModel) },
            { Models.Pages.DatePicker, typeof(DatePickerViewModel) },
            { Models.Pages.Picker, typeof(PickerViewModel) },
            { Models.Pages.RadioButton, typeof(RadioButtonViewModel) },
            { Models.Pages.Rating, typeof(RatingViewModel) },
            { Models.Pages.Selection, typeof(SelectionViewModel) },
            { Models.Pages.Slider, typeof(SliderViewModel) },
            { Models.Pages.Switch, typeof(SwitchViewModel) },
            { Models.Pages.TimePicker, typeof(TimePickerViewModel) },
            { Models.Pages.TextField, typeof(TextFieldViewModel) },
            { Models.Pages.MultilineTextField, typeof(MultilineTextFieldViewModel) },
            { Models.Pages.Label, typeof(LabelViewModel) }
        };
        
        public MainViewModel()
        {
            CreateMenu();
        }
        
        private void CreateMenu()
        {
            var menuItems = new List<MaterialNavigationDrawerItem>
            {
                new() { Headline = Sections.Actions, Text = Models.Pages.Button, LeadingIcon = "ic_button.png" },
                new() { Headline = Sections.Actions, Text = Models.Pages.FloatingButton, LeadingIcon = "ic_floating.png" },
                new() { Headline = Sections.Actions, Text = Models.Pages.IconButton, LeadingIcon = "ic_icon_button.png" },
                new() { Headline = Sections.Actions, Text = Models.Pages.SegmentedButtons, LeadingIcon = "ic_segmented.png", BadgeText = "NEW" },
                new() { Headline = Sections.Communications, Text = Models.Pages.Badge, LeadingIcon = "ic_badge.png" },
                new() { Headline = Sections.Communications, Text = Models.Pages.ProgressIndicator, LeadingIcon = "ic_progress_indicator.png" },
                new() { Headline = Sections.Communications, Text = Models.Pages.Snackbar, LeadingIcon = "ic_snackbar.png" },
                new() { Headline = Sections.Containment, Text = Models.Pages.BottomSheet, LeadingIcon = "ic_bottomsheet.png", BadgeText = "NEW" },
                new() { Headline = Sections.Containment, Text = Models.Pages.Card, LeadingIcon = "ic_card.png" },
                new() { Headline = Sections.Containment, Text = Models.Pages.Dialog, LeadingIcon = "ic_dialog.png", TrailingIcon = "pending_actions.png", IsEnabled = false },
                new() { Headline = Sections.Containment, Text = Models.Pages.Divider, LeadingIcon = "ic_divider.png"},
                new() { Headline = Sections.Navigation, Text = Models.Pages.NavigationDrawer, LeadingIcon = "ic_navigation_drawer.png" },
                new() { Headline = Sections.Navigation, Text = Models.Pages.TopAppBar, LeadingIcon = "ic_top_app_bar.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Checkbox, LeadingIcon = "ic_checkbox.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Chip, LeadingIcon = "ic_chip.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.DatePicker, LeadingIcon = "ic_date.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Picker, LeadingIcon = "ic_picker.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.RadioButton, LeadingIcon = "ic_radio.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Rating, LeadingIcon = "ic_rating.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Selection, LeadingIcon = "ic_selection.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Slider, LeadingIcon = "ic_slider.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Switch, LeadingIcon = "ic_switch.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.TimePicker, LeadingIcon = "ic_time.png" },
                new() { Headline = Sections.TextInputs, Text = Models.Pages.MultilineTextField, LeadingIcon = "ic_editor.png" },
                new() { Headline = Sections.TextInputs, Text = Models.Pages.TextField, LeadingIcon = "ic_entry.png" },
                new() { Headline = Sections.Typography, Text = Models.Pages.Label, LeadingIcon = "ic_label.png" }
            };

            MenuItems = menuItems;
        }

        [ICommand]
        private async Task MenuItemClickAsync(MaterialNavigationDrawerItem menuItem)
        {
            if (menuItem != null && _viewmodelTypeMap.TryGetValue(menuItem.Text, out Type viewModelType))
            {
                await GoToAsync(viewModelType.Name);
            }
        }
         
        [ICommand]
        private async Task AboutUsAsync()
        {
            await GoToAsync<AboutViewModel>();
        }
    }
}