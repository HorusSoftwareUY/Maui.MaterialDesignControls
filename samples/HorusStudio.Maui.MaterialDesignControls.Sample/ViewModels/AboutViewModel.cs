using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class AboutViewModel : BaseViewModel
	{
        #region Attributes & Properties

        public override string Title => "About us";

        #endregion

        #region Commands
        
        [ICommand]
        private async Task LaunchBrowser(string url)
        {
            if (!string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
        }
        
        #endregion
    }
}

