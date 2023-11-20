using System;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class LabelViewModel : BaseViewModel
	{
        #region Attributes & Properties

        public override string Title => "Labels";

        #endregion

        public LabelViewModel()
        {
            Subtitle = @"The scale is a range of contrasting styles that support the needs of various product contexts and content. No single product will use all the styles defined below. Instead, select styles from the scale that are most appropriate.";
        }
    }
}

