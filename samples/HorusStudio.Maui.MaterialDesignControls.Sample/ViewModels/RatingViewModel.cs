using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class RatingViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => "Rating";

        #endregion

        public RatingViewModel()
        {
            Subtitle = "This control allow to rate.";
        }
    }
}
