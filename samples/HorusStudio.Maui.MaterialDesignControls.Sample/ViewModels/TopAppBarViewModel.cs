using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
	public partial class TopAppBarViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.TopAppBar;
        protected override string ControlReferenceUrl => "components/top-app-bar/overview";

        [ObservableProperty]
        private bool _iconButtonsEnabled = true;

        [ObservableProperty]
        private bool _iconButtonsVisible = true;

        [ObservableProperty]
        private List<TrailingIcon> _trailingIcons;
        
        #endregion

        public TopAppBarViewModel()
        {
            Subtitle = "Top app bars display navigation, actions, and text at the top of a screen.";

            ChangeTrailingIcons();
        }

        [ICommand]
        private void ChangeTrailingIcons()
        {
            if (TrailingIcons != null && TrailingIcons.Count == 2)
            {
                TrailingIcons = new List<TrailingIcon>
                {
                    new TrailingIcon
                    {
                        Icon="plus.png",
                        Command= ChangeTrailingIconsCommand
                    }
                };
            }
            else
            {
                TrailingIcons = new List<TrailingIcon>
                {
                    new TrailingIcon
                    {
                        Icon="ic_web.png",
                        Command= ChangeTrailingIconsCommand
                    },
                    new TrailingIcon
                    {
                        Icon="plus.png",
                        Command= ChangeTrailingIconsCommand
                    }
                };
            }
        }

        [ICommand]
        private void ChangeTrailingIconsState()
        {
            IconButtonsEnabled = !IconButtonsEnabled;
            IconButtonsVisible = !IconButtonsVisible;
        }

        [ICommand]
        private async Task LeadingIconTap()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await DisplayAlert(Title, "Leading icon clicked!", "OK");
        }

        [ICommand]
        private async Task TrailingIconTap()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            await DisplayAlert(Title, "Trailing icon clicked!", "OK");
        }

        [ICommand]
        private async Task TrailingIconTap2()
        {
            await TrailingIconTap();
        }

        [ICommand]
        private async Task TrailingIconTap3()
        {
            await TrailingIconTap();
        }
    }
}