using HorusStudio.Maui.MaterialDesignControls.Sample.Helpers;
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
        private List<CustomizationItem> _buttonsTypes;

        [ObservableProperty]
        private MaterialButtonType _selectedButtonType = MaterialButtonType.Filled;

        [ObservableProperty]
        private List<CustomizationItem> _cornersRadius;

        [ObservableProperty]
        private int _selectedCornerRadius = 20;

        [ObservableProperty]
        private List<CustomizationColor> _backgroundColors;

        [ObservableProperty]
        private Color _selectedBackgroundColor = ColorHelper.GetColorByKey("PurplePrimary");

        [ObservableProperty]
        private List<CustomizationColor> _textColors;

        [ObservableProperty]
        private Color _selectedTextColor = ColorHelper.GetColorByKey("PurpleBackground");

        [ObservableProperty]
        private List<CustomizationItem> _fontFamilies;

        [ObservableProperty]
        private string _selectedFontFamily = "FontMedium";

        [ObservableProperty]
        private List<CustomizationItem> _fontSizes;

        [ObservableProperty]
        private int _selectedFontSize = 14;

        [ObservableProperty]
        private List<CustomizationItem> _animations;

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
            Animations = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "None", Value = AnimationTypes.None, IsSelected = true },
                new CustomizationItem { Name = "Fade", Value = AnimationTypes.Fade },
                new CustomizationItem { Name = "Scale", Value = AnimationTypes.Scale },
            };

            FontSizes = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "12", Value = 12 },
                new CustomizationItem { Name = "14", Value = 14, IsSelected = true },
                new CustomizationItem { Name = "16", Value = 16 },
            };

            FontFamilies = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "Roboto", Value = "FontMedium",  IsSelected = true },
                new CustomizationItem { Name = "Roboto Slab", Value = "FontMediumSlab" }
            };

            ButtonsTypes = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "Filled", Value = MaterialButtonType.Filled, IsSelected = true },
                new CustomizationItem { Name = "Elevated", Value = MaterialButtonType.Elevated },
                new CustomizationItem { Name = "Outlined", Value = MaterialButtonType.Outlined },
                new CustomizationItem { Name = "Tonal", Value = MaterialButtonType.Tonal },
                new CustomizationItem { Name = "Text", Value = MaterialButtonType.Text },
            };

            CornersRadius = new List<CustomizationItem>
            {
                new CustomizationItem { Name = "20", Value = 20, IsSelected = true },
                new CustomizationItem { Name = "10", Value = 10 },
                new CustomizationItem { Name = "0", Value = 0 }
            };

            BackgroundColors = ColorHelper.GetCustomizationColorsBySuffix("Primary", false);
          
            TextColors = ColorHelper.GetCustomizationColorsBySuffix("Background", true);

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

