using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class MainPage : BaseContentPage<MainViewModel>
    {
        public MainViewModel viewModel;
        public MainPage(MainViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            viewModel.FromAppearance = viewModel.FromAppearanceControl;

            if (viewModel.FromAppearance)
            {
                viewModel.AppearanceAsync();
            }

            base.OnAppearing();
        }
    }
}