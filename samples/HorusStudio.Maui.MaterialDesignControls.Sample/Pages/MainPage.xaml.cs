using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class MainPage
{
    public MainPage(MainViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();

#if USE_MONO
        RuntimeLabel.Text = "Running with Mono";
        RuntimeLabel.TextColor = Color.FromArgb("#29B6F6");
#endif
    }
}