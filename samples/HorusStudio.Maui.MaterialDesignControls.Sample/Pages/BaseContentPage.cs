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
            }
        }

        protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
        {
            base.OnNavigatingFrom(args);
            if (BindingContext is BaseViewModel vm)
            {
                vm.NavigatingFrom();
            }
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