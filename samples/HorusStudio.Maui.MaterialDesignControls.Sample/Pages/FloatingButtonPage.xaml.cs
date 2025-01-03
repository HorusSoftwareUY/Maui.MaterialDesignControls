using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class FloatingButtonPage : BaseContentPage<FloatingButtonViewModel>
    {
        
        public FloatingButtonPage(FloatingButtonViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MaterialFloatingButton.ShowFloatingButton();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MaterialFloatingButton.HideFloatingButton();
        }
    }
}