using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class BenchmarkListPage : BaseContentPage<BenchmarkListViewModel>
{
    public BenchmarkListPage(BenchmarkListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
