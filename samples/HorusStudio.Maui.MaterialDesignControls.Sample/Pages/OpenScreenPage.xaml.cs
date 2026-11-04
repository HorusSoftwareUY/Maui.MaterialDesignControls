using System.Diagnostics;
using HorusStudio.Maui.MaterialDesignControls.Sample.Benchmarks.OpenScreen;
using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

/// <summary>
/// Times opening the Northbeam Dispatch screen, from the tap to the first frame
/// Android draws with it, split in two:
///
///   Build = new DispatchHome…() + BindingContext   (inflate XAML, materialize the 20 bound rows)
///   Show  = attach to the page → handlers → layout → first native draw
///
/// T0 is taken when the button's Clicked fires, so MaterialButton's own touch
/// animation is not in the number. "cold" is per process: only the first open
/// after launching the app counts as cold, whichever button it was.
///
/// Every open also writes one line to logcat (tag DOTNET, prefix OPENSCREEN) so
/// runs can be harvested with adb.
/// </summary>
public partial class OpenScreenPage : BaseContentPage<OpenScreenViewModel>
{
    private static int _opensInProcess;

    private readonly OpenScreenViewModel _vm;

    public OpenScreenPage(OpenScreenViewModel vm) : base(vm)
    {
        _vm = vm;
        InitializeComponent();
        TopBar.LeadingIconCommand = new Command(() =>
        {
            if (!CloseScreen())
                vm.GoBackCommand.Execute(null);
        });
    }

    private void OnOpenXamlC(object? sender, EventArgs e) =>
        Open("XamlC", static () => new DispatchHomeXamlC());

    private void OnOpenSourceGen(object? sender, EventArgs e) =>
        Open("SourceGen", static () => new DispatchHomeSourceGen());

    private void Open(string inflator, Func<View> create)
    {
        if (ScreenHost.Content is not null)
            return;

        var openIndex = ++_opensInProcess;
        var t0 = Stopwatch.GetTimestamp();

        // ── Build ──
        var screen = create();
        screen.BindingContext = DispatchData.Shared;
        var tBuilt = Stopwatch.GetTimestamp();

        // ── Show ──
        FirstFrame.Await(screen, () =>
        {
            var tShown = Stopwatch.GetTimestamp();
            var build = Stopwatch.GetElapsedTime(t0, tBuilt).TotalMilliseconds;
            var show = Stopwatch.GetElapsedTime(tBuilt, tShown).TotalMilliseconds;
            var result = _vm.Record(inflator, build, show, openIndex == 1, openIndex);

            Console.WriteLine(
                $"OPENSCREEN result runtime={OpenScreenViewModel.RuntimeName} inflator={inflator} " +
                $"cold={(result.IsCold ? 1 : 0)} open={openIndex} build={build:F1} show={show:F1} total={build + show:F1}");
        });

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
        _vm.ScreenClosed();
        return true;
    }
}
