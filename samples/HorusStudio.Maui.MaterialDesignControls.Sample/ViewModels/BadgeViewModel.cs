using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class BadgeViewModel : BaseViewModel
	{
        #region Attributes & Properties

        public override string Title => "MaterialBadge";

        #endregion

        public BadgeViewModel()
        {
            Subtitle = "Badges show notifications, counts, or status information on navigation items and icons";
        }
    }
}