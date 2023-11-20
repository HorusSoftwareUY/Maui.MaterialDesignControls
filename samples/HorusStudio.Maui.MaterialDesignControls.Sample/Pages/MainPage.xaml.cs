using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class MainPage : BaseContentPage<MainViewModel>
    {
        public MainPage(MainViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}