using System;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class DividerViewModel : BaseViewModel
	{
        #region Attributes & Properties

        public override string Title => "Dividers";

        #endregion

        public DividerViewModel()
        {
            Subtitle = "Dividers are one way to visually group components and create hierarchy. They can also be used to imply nested parent/child relationships.";
        }
    }
}

