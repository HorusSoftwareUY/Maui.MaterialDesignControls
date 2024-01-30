using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class TopAppBarPage : BaseContentPage<TopAppBarViewModel>
{
    public TopAppBarPage(TopAppBarViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}