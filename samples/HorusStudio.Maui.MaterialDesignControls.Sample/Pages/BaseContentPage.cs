using System;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public abstract class BaseContentPage<TBaseViewModel> : ContentPage where TBaseViewModel : BaseViewModel
    {
        #region Bindable Properties
        
        /// <summary>
        /// The backing store for the <see cref="Subtitle" /> bindable property.
        /// </summary>
        public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(BaseContentPage<>));
        
        /// <summary>
        /// The backing store for the <see cref="CustomizationMode" /> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomizationModeProperty = BindableProperty.Create(nameof(CustomizationMode), typeof(bool), typeof(BaseContentPage<>));
        
        /// <summary>
        /// The backing store for the <see cref="BackCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(BaseContentPage<>));
        
        #endregion Bindable Properties
        
        #region Properties
        
        /// <summary>
        /// Gets or sets a subtitle for page. This is a bindable property.
        /// </summary>
        public string Subtitle
        {
            get => (string)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }
        
        /// <summary>
        /// Gets or sets a flag to determine whether customization mode is active or not. This is a bindable property.
        /// </summary>
        public bool CustomizationMode
        {
            get => (bool)GetValue(CustomizationModeProperty);
            set => SetValue(CustomizationModeProperty, value);
        }
        
        /// <summary>
        /// Gets or sets a command to be fired when back button is pressed. This is a bindable property.
        /// </summary>
        public ICommand BackCommand
        {
            get => (ICommand)GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }
        
        #endregion Properties
        
        public BaseContentPage(TBaseViewModel viewModel)
        {
            BindingContext = viewModel;
            viewModel.DisplayAlert = DisplayAlert;
            viewModel.DisplayActionSheet = DisplayActionSheet;
            
            SetBinding(TitleProperty, new Binding(nameof(BaseViewModel.Title)));
            SetBinding(SubtitleProperty, new Binding(nameof(BaseViewModel.Subtitle)));
            SetBinding(CustomizationModeProperty, new Binding(nameof(BaseViewModel.IsCustomize), mode: BindingMode.TwoWay));
            SetBinding(BackCommandProperty, new Binding(nameof(BaseViewModel.GoBackCommand)));
            
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

