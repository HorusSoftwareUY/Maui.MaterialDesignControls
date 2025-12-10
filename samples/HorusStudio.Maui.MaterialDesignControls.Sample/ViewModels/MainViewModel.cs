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
            //{ Models.Pages.BottomSheet, typeof(BottomSheetViewModel) },
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
            { Models.Pages.Label, typeof(LabelViewModel) },
            { Models.Pages.Appearance, typeof(AppearanceViewModel) }
        };
        
        public MainViewModel()
        {
            CreateMenu();
        }
        
        private void CreateMenu()
        {
            var menuItems = new List<MaterialNavigationDrawerItem>
            {
                new() { Headline = Sections.Settings, Text = Models.Pages.Appearance, AutomationId = $"menu_{nameof(Models.Pages.Appearance)}", LeadingIcon = "settings.png" },
                new() { Headline = Sections.Actions, Text = Models.Pages.Button, AutomationId = $"menu_{nameof(Models.Pages.Button)}", LeadingIcon = "ic_button.png" },
                new() { Headline = Sections.Actions, Text = Models.Pages.FloatingButton, AutomationId = $"menu_{nameof(Models.Pages.FloatingButton)}", LeadingIcon = "ic_floating.png" },
                new() { Headline = Sections.Actions, Text = Models.Pages.IconButton, AutomationId = $"menu_{nameof(Models.Pages.IconButton)}", LeadingIcon = "ic_icon_button.png" },
                new() { Headline = Sections.Actions, Text = Models.Pages.SegmentedButtons, AutomationId = $"menu_{nameof(Models.Pages.SegmentedButtons)}", LeadingIcon = "ic_segmented.png" },
                new() { Headline = Sections.Communications, Text = Models.Pages.Badge, AutomationId = $"menu_{nameof(Models.Pages.Badge)}", LeadingIcon = "ic_badge.png" },
                new() { Headline = Sections.Communications, Text = Models.Pages.ProgressIndicator, AutomationId = $"menu_{nameof(Models.Pages.ProgressIndicator)}", LeadingIcon = "ic_progress_indicator.png" },
                new() { Headline = Sections.Communications, Text = Models.Pages.Snackbar, AutomationId = $"menu_{nameof(Models.Pages.Snackbar)}", LeadingIcon = "ic_snackbar.png" },
                new() { Headline = Sections.Containment, Text = Models.Pages.BottomSheet, AutomationId = $"menu_{nameof(Models.Pages.BottomSheet)}", LeadingIcon = "ic_bottomsheet.png", TrailingIcon = "pending_actions.png", IsEnabled = false },
                new() { Headline = Sections.Containment, Text = Models.Pages.Card, AutomationId = $"menu_{nameof(Models.Pages.Card)}", LeadingIcon = "ic_card.png" },
                new() { Headline = Sections.Containment, Text = Models.Pages.Dialog, AutomationId = $"menu_{nameof(Models.Pages.Dialog)}", LeadingIcon = "ic_dialog.png", TrailingIcon = "pending_actions.png", IsEnabled = false },
                new() { Headline = Sections.Containment, Text = Models.Pages.Divider, AutomationId = $"menu_{nameof(Models.Pages.Divider)}", LeadingIcon = "ic_divider.png"},
                new() { Headline = Sections.Navigation, Text = Models.Pages.NavigationDrawer, AutomationId = $"menu_{nameof(Models.Pages.NavigationDrawer)}", LeadingIcon = "ic_navigation_drawer.png" },
                new() { Headline = Sections.Navigation, Text = Models.Pages.TopAppBar, AutomationId = $"menu_{nameof(Models.Pages.TopAppBar)}", LeadingIcon = "ic_top_app_bar.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Checkbox, AutomationId = $"menu_{nameof(Models.Pages.Checkbox)}", LeadingIcon = "ic_checkbox.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Chip, AutomationId = $"menu_{nameof(Models.Pages.Chip)}", LeadingIcon = "ic_chip.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.DatePicker, AutomationId = $"menu_{nameof(Models.Pages.DatePicker)}", LeadingIcon = "ic_date.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Picker, AutomationId = $"menu_{nameof(Models.Pages.Picker)}", LeadingIcon = "ic_picker.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.RadioButton, AutomationId = $"menu_{nameof(Models.Pages.RadioButton)}", LeadingIcon = "ic_radio.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Rating, AutomationId = $"menu_{nameof(Models.Pages.Rating)}", LeadingIcon = "ic_rating.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Selection, AutomationId = $"menu_{nameof(Models.Pages.Selection)}", LeadingIcon = "ic_selection.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Slider, AutomationId = $"menu_{nameof(Models.Pages.Slider)}", LeadingIcon = "ic_slider.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.Switch, AutomationId = $"menu_{nameof(Models.Pages.Switch)}", LeadingIcon = "ic_switch.png" },
                new() { Headline = Sections.Selection, Text = Models.Pages.TimePicker, AutomationId = $"menu_{nameof(Models.Pages.TimePicker)}", LeadingIcon = "ic_time.png" },
                new() { Headline = Sections.TextInputs, Text = Models.Pages.MultilineTextField, AutomationId = $"menu_{nameof(Models.Pages.MultilineTextField)}", LeadingIcon = "ic_editor.png" },
                new() { Headline = Sections.TextInputs, Text = Models.Pages.TextField, AutomationId = $"menu_{nameof(Models.Pages.TextField)}", LeadingIcon = "ic_entry.png" },
                new() { Headline = Sections.Typography, Text = Models.Pages.Label, AutomationId = $"menu_{nameof(Models.Pages.Label)}", LeadingIcon = "ic_label.png" }
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