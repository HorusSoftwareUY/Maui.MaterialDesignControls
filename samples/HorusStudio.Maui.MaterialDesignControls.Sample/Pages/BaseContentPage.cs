using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public abstract class BaseContentPage<TBaseViewModel> : ContentPage where TBaseViewModel : BaseViewModel
    {
        public BaseContentPage(TBaseViewModel viewModel)
        {
            BindingContext = viewModel;
            viewModel.DisplayAlert = DisplayAlert;
            viewModel.DisplayActionSheet = DisplayActionSheet;
            
            Shell.SetNavBarIsVisible(this, false);
        }

        protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
        {
            base.OnNavigatedFrom(args);
            if (BindingContext is BaseViewModel vm)
            {
                vm.NavigatedFrom();
            }
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            if (BindingContext is BaseViewModel vm)
            {
                vm.NavigatedTo();
#if ENABLE_NAV_TIMING
                // OnNavigatedTo fires after the Shell navigation animation completes,
                // which is already more accurate than OnAppearing. We defer one more
                // dispatch cycle so that MAUI's initial layout/render pass can finish
                // before we stop the timer. VMs with async data loading should override
                // ReportsPageReadyManually and call ReportPageReady() themselves.
                if (!vm.ReportsPageReadyManually)
                    Dispatcher.Dispatch(vm.ReportPageReady);
#endif
            }
        }

        protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
        {
            base.OnNavigatingFrom(args);
            if (BindingContext is BaseViewModel vm)
            {
                vm.NavigatingFrom();
            }
#if ENABLE_NAV_TIMING
            // Dismiss any visible timing badge when leaving this page
            if (Content is Grid grid)
            {
                foreach (var overlay in grid.Children.OfType<Views.NavTimingOverlay>())
                    overlay.Dismiss();
            }
#endif
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is BaseViewModel vm)
            {
                return vm.BackButtonPressed();
            }
            return base.OnBackButtonPressed();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is BaseViewModel vm)
            {
                vm.Initialize();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is BaseViewModel vm)
            {
                vm.Appearing();
#if ENABLE_NAV_TIMING
                // Attach the floating overlay to this page (idempotent).
                AppShell.EnsureNavTimingOverlay(this);
                // Timer is stopped in OnNavigatedTo (or by the VM itself if
                // ReportsPageReadyManually is true). Nothing to do here.
#endif
#if ENABLE_STARTUP_PROFILING
                // One-shot: record the first page that becomes visible and dump the timeline.
                HorusStudio.Maui.MaterialDesignControls.Sample.Utils.StartupProfiler.Mark($"First OnAppearing: {GetType().Name}");
                HorusStudio.Maui.MaterialDesignControls.Sample.Utils.StartupProfiler.Dump();
#endif
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is BaseViewModel vm)
            {
                vm.Disappearing();
            }
        }
    }
}