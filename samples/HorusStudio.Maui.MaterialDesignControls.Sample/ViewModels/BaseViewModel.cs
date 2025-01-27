using System.Diagnostics;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        #region Attributes & Properties

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotBusy))]
        private bool _isBusy;

        public bool IsNotBusy => !IsBusy;
        public abstract string Title { get; }

        [ObservableProperty]
        private string _subtitle = string.Empty;

        [ObservableProperty]
        private bool _isCustomize;
        
        [ObservableProperty]
        private bool _isEnabled = true;

        [ObservableProperty]
        private bool _isVisible = true;
        
#if ANDROID
        bool _alreadyOpenFlyout = false;
#endif
        
        #endregion Attributes & Properties

        public delegate Task DisplayAlertType(string title, string message, string cancel);
        public delegate Task<string> DisplayActionSheetType(string title, string cancel, string destruction, params string[] buttons);

        public DisplayAlertType? DisplayAlert { get; set; }
        public DisplayActionSheetType? DisplayActionSheet { get; set; }

        #region Navigation

        public virtual void NavigatedFrom() { }

        public virtual void NavigatedTo() { }

        public virtual void NavigatingFrom() { }

        public virtual bool BackButtonPressed() => false;

        public virtual void Initialize() { }

        public virtual void Appearing()
        {
            if (this is MainViewModel)
            {
                Shell.Current.BindingContext = this;
            }
            else
            {
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            }
        }

        public virtual void Disappearing() { }

        protected Task GoBackAsync(IDictionary<string, object> parameters = null) => GoToAsync("..", parameters);

        protected Task GoToAsync<TViewModel>(IDictionary<string, object> parameters = null, bool animate = true, bool isRoot = false)
            where TViewModel : BaseViewModel => GoToAsync(typeof(TViewModel).Name, parameters, animate, isRoot);

        protected async Task GoToAsync(IEnumerable<Type> fullRoute, IDictionary<string, object> parameters = null, bool animate = true, bool isRoot = false)
        {
            try
            {
                IsBusy = true;
                if (fullRoute == null || !fullRoute.Any()) return;
                if (fullRoute.Any(partialUri => !partialUri.IsAssignableFrom(typeof(BaseViewModel)))) return;

                var navigationUri = string.Join("/", fullRoute.Select(partialUri => partialUri.Name));
                await GoToAsync(navigationUri, parameters, animate, isRoot);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString(), nameof(GoToAsync));
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async Task GoToAsync(string navigationUri, IDictionary<string, object> parameters = null, bool animate = true, bool isRoot = false)
        {
            try
            {
                IsBusy = true;
                var finalNavigationUri = $"{(isRoot ? "//" : string.Empty)}{navigationUri}";

                if (parameters != null)
                {
                    await Shell.Current.GoToAsync(navigationUri, animate, parameters);
                }
                else
                {
                    await Shell.Current.GoToAsync(navigationUri, animate);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString(), nameof(GoToAsync));
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        private Task GoBack() => GoBackAsync();

        [ICommand]
        private async Task GoToAsync(Type type)
        {
            if (Shell.Current.FlyoutIsPresented) Shell.Current.FlyoutIsPresented = false;
            await GoToAsync(type.Name);
        }

        [ICommand]
        private void ToggleMenu()
        {
            // Workaround to open the flyout on Android the first time
            // https://github.com/dotnet/maui/issues/8226
#if ANDROID
            if (!_alreadyOpenFlyout)
            {
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                _alreadyOpenFlyout = true;
            }
#endif

            Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
        }

        #endregion Navigation
    }
}

