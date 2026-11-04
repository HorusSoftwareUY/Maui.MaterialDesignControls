using HorusStudio.Maui.MaterialDesignControls.Sample.Benchmarks.OpenScreen;
using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class OpenScreenPage : BaseContentPage<OpenScreenViewModel>
{
    public OpenScreenPage(OpenScreenViewModel vm) : base(vm)
    {
        InitializeComponent();
        TopBar.LeadingIconCommand = new Command(() =>
        {
            if (!CloseScreen())
                vm.GoBackCommand.Execute(null);
        });
    }

    private void OnOpenXamlC(object? sender, EventArgs e) => Show(new DispatchHomeXamlC());

    private void OnOpenSourceGen(object? sender, EventArgs e) => Show(new DispatchHomeSourceGen());

    private void Show(View screen)
    {
        screen.BindingContext = DispatchData.Shared;
        ScreenHost.Content = screen;
        ScreenHost.IsVisible = true;
    }

    protected override bool OnBackButtonPressed() => CloseScreen() || base.OnBackButtonPressed();

    /// <summary>Closes the opened screen, if any. Returns false when there was none.</summary>
    private bool CloseScreen()
    {
        if (ScreenHost.Content is null)
            return false;

        // Drop the screen entirely so the next open pays the full cost again.
        ScreenHost.IsVisible = false;
        ScreenHost.Content = null;
        return true;
    }
}
