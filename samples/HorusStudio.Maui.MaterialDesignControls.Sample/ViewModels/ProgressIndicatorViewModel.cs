using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class ProgressIndicatorViewModel : BaseViewModel
	{
        #region Attributes & Properties

        public override string Title => "Progress Indicators";

        #endregion

        public ProgressIndicatorViewModel()
        {
            Subtitle = "Progress indicators inform users about the status of ongoing processes, such as loading an app, submitting a form, or saving updates. They communicate an app’s state and indicate available actions, such as whether users can navigate away from the current screen.";
        }
    }
}

