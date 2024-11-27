using System;
using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class ButtonViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Buttons";

        [ObservableProperty]
        private TextDecorations _decorations = TextDecorations.None;

        [ObservableProperty]
        private bool _isCustomize;

        [ObservableProperty]
        private List<CustomizationItem> _chipsButtonsTypes;

        [ObservableProperty]
        private MaterialButtonType _selectedButtonType = MaterialButtonType.Filled;

        [ObservableProperty]
        private List<CustomizationItem> _chipsCornerRadius;

        [ObservableProperty]
        private int _selectedCornerRadius = 20;

        [ObservableProperty]
        private List<CustomizationItem> _chipsBackgroundColors;

        [ObservableProperty]
        private Color _selectedBackgroundColor = Color.FromArgb("#6750A4");

        [ObservableProperty]
        private List<CustomizationItem> _chipsTextColors;

        [ObservableProperty]
        private Color _selectedTextColor = Color.FromArgb("#FFFFFF");

        [ObservableProperty]
        private List<CustomizationItem> _chipsFontFamilies;

        [ObservableProperty]
        private string _selectedFontFamily = "FontMedium";

        [ObservableProperty]
        private List<CustomizationItem> _chipsFontSizes;

        [ObservableProperty]
        private int _selectedFontSize = 14;

        [ObservableProperty]
        private List<CustomizationItem> _chipsAnimations;

        [ObservableProperty]
        private AnimationTypes _selectedAnimation = AnimationTypes.None;

        [ObservableProperty]
        private bool _hasIcon;
        
        [ObservableProperty]
        private bool _isEnabled = true;

        [ObservableProperty]
        private bool _isVisible = true;

        #endregion

        public ButtonViewModel()
        {
            ChipsAnimations = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "None", Value = AnimationTypes.None, IsSelected = true },
                new CustomizationItem { Name = "Fade", Value = AnimationTypes.Fade },
                new CustomizationItem { Name = "Scale", Value = AnimationTypes.Scale },
            };

            ChipsFontSizes = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "12", Value = 12 },
                new CustomizationItem { Name = "14", Value = 14, IsSelected = true },
                new CustomizationItem { Name = "16", Value = 16 },
            };

            ChipsFontFamilies = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "Roboto", Value = "FontMedium",  IsSelected = true },
                new CustomizationItem { Name = "Roboto Slab", Value = "FontMediumSlab" }
            };

            ChipsButtonsTypes = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "Filled", Value = MaterialButtonType.Filled, IsSelected = true },
                new CustomizationItem { Name = "Elevated", Value = MaterialButtonType.Elevated },
                new CustomizationItem { Name = "Outlined", Value = MaterialButtonType.Outlined },
                new CustomizationItem { Name = "Tonal", Value = MaterialButtonType.Tonal },
                new CustomizationItem { Name = "Text", Value = MaterialButtonType.Text },
            };

            ChipsCornerRadius = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "20", Value = 20, IsSelected = true },
                new CustomizationItem { Name = "10", Value = 10 },
                new CustomizationItem { Name = "0", Value = 0 }
            };

            ChipsBackgroundColors = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "Purple", Value = Color.FromArgb("#6750A4"), IsSelected = true },
                new CustomizationItem { Name = "Green", Value = Color.FromArgb("#50A465") },
                new CustomizationItem { Name = "Blue", Value = Color.FromArgb("#1D4BEC") },
                new CustomizationItem { Name = "Red", Value = Color.FromArgb("#DD2953") },
            };

            ChipsTextColors = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "Primary", Value = Color.FromArgb("#FFFFFF"), IsSelected = true },
                new CustomizationItem { Name = "Secondary", Value = Color.FromArgb("#EAFF76") },
                new CustomizationItem { Name = "Tertiary", Value = Color.FromArgb("#76FFC4") }
            };

            Subtitle = "Buttons help people take action, such as sending an email, sharing a document, or liking a comment.";
        }

        [ICommand]
        private async Task MaterialButton1(string message)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await DisplayAlert(Title, message ?? "Clicked!", "OK");
        }

        [ICommand]
        private Task MaterialButton2(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton3(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton4(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton5(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton6(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton7(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton8(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton9(string message) => MaterialButton1(message);
        [ICommand]
        private Task MaterialButton10(string message) => MaterialButton1(message);

        [ICommand]
        private void MaterialButton11(string message)
        {
            Decorations = (TextDecorations)(((int)Decorations + 1) % 3);
        }

        [ICommand]
        private Task MaterialButton12(string message) => MaterialButton1(message);

        #region Customize commands

        [ICommand]
        private async Task TapButton() 
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
        }

        #endregion
    }
}

