using System.Collections.ObjectModel;
using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
#if ENABLE_NAV_TIMING
using NavigationTimer = HorusStudio.Maui.MaterialDesignControls.Sample.Utils.NavigationTimer;
#endif

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
        
        protected virtual string ControlReferenceUrl => string.Empty;

        [ObservableProperty]
        private string _subtitle = string.Empty;

        [ObservableProperty]
        private bool _isCustomize;

        [ObservableProperty]
        private ObservableCollection<MaterialSegmentedButtonItem> _tabItems;

        [ObservableProperty]
        private MaterialSegmentedButtonItem _selectedTabItem;

        [ObservableProperty]
        private bool _isEnabled = true;

        [ObservableProperty]
        private bool _isVisible = true;
        
        [ObservableProperty]
        private IEnumerable<TrailingIcon> _contextualActions;
        
        #endregion Attributes & Properties

        protected BaseViewModel()
        {
            if (!string.IsNullOrEmpty(ControlReferenceUrl))
            {
                ContextualActions = new[]
                {
                    new TrailingIcon
                    {
                        Icon = "ic_web.png",
                        Command = OpenControlReferenceCommand,
                        IsBusy = OpenControlReferenceCommand.IsRunning
                    }/*,
                    new TrailingIcon
                    {
                        Icon = "ic_preview.png",
                        Command = OpenControlReferenceCommand,
                        IsBusy = OpenControlReferenceCommand.IsRunning
                    }*/
                };
            }

            TabItems = new ObservableCollection<MaterialSegmentedButtonItem>
            {
                new MaterialSegmentedButtonItem("Overview"),
                new MaterialSegmentedButtonItem("Customize")
            };

            SelectedTabItem = TabItems.First();
        }
        
        public delegate Task DisplayAlertType(string title, string message, string cancel);
        public delegate Task<string> DisplayActionSheetType(string title, string cancel, string destruction, params string[] buttons);

        public DisplayAlertType? DisplayAlert { get; set; }
        public DisplayActionSheetType? DisplayActionSheet { get; set; }

        [ICommand]
        private async Task OpenControlReferenceAsync()
        {
            const string baseUrl = "https://m3.material.io/";
            var url = Path.Combine(baseUrl, ControlReferenceUrl);
            
            if (!string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
        }

        [ICommand]
        private async Task TabItemsSelection()
        {
            IsCustomize = !IsCustomize;
        }

        #region Navigation

        public virtual void NavigatedFrom() { }

        public virtual void NavigatedTo() { }

        public virtual void NavigatingFrom() { }

        public virtual bool BackButtonPressed() => false;

        public virtual void Initialize() { }

        /// <summary>
        /// When true the ViewModel takes ownership of calling <see cref="ReportPageReady"/>
        /// (e.g. after async data is bound). Set to true in any VM that loads data
        /// asynchronously so that BaseContentPage does not fire the timer too early.
        /// </summary>
        protected internal virtual bool ReportsPageReadyManually => false;

        /// <summary>
        /// Call this from any ViewModel when the page is fully loaded and ready to display.
        /// Simple pages get this called automatically from OnNavigatedTo via BaseContentPage.
        /// Pages with async data loading should override <see cref="ReportsPageReadyManually"/>
        /// and call this themselves after the data is bound on the main thread.
        /// </summary>
        public void ReportPageReady()
        {
#if ENABLE_NAV_TIMING
            NavigationTimer.Complete(Title);
#endif
        }

        public virtual void Appearing()
        {
            if (this is MainViewModel)
            {
                Shell.Current.BindingContext = this;
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            }
            else
            {
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            }

            if (!string.IsNullOrEmpty(Title))
            {
                var eventName = $"{Title.ToLower().Replace(" ", "_")}_page_appearing";
                Logger.LogEvent(eventName);
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
                Logger.LogException(ex);
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

#if ENABLE_NAV_TIMING
                NavigationTimer.Start(navigationUri);
#endif

                if (parameters != null)
                {
                    await Shell.Current.GoToAsync(finalNavigationUri, animate, parameters);
                }
                else
                {
                    await Shell.Current.GoToAsync(finalNavigationUri, animate);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
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
            Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
        }

        #endregion Navigation
    }
}

