using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class LabelViewModel : BaseViewModel
	{
        #region Attributes & Properties

        public override string Title => "Labels";

        [ObservableProperty]
        private bool _isCustomize;

        [ObservableProperty]
        private LabelTypes _labelType = LabelTypes.HeadlineSmall;

        [ObservableProperty]
        private Color _textColor = Colors.DarkGreen;

        [ObservableProperty]
        private string _fontFamily = "FontRegular";

        #endregion

        public LabelViewModel()
        {
            Subtitle = @"The scale is a range of contrasting styles that support the needs of various product contexts and content. No single product will use all the styles defined below. Instead, select styles from the scale that are most appropriate.";
        }

        [ICommand]
        private void ChangeAppearance()
        {
            TextColor = LabelType == LabelTypes.HeadlineSmall ? Colors.DarkRed : Colors.DarkGreen;
            LabelType = LabelType == LabelTypes.HeadlineSmall ? LabelTypes.LabelMedium : LabelTypes.HeadlineSmall;
            FontFamily = LabelType == LabelTypes.HeadlineSmall ? "FontBold" : "FontRegular";
        }
    }
}