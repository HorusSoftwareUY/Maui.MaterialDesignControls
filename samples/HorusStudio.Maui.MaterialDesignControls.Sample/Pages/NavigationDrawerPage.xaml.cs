using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class NavigationDrawerPage : BaseContentPage<NavigationDrawerViewModel>
{
    public NavigationDrawerPage(NavigationDrawerViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}