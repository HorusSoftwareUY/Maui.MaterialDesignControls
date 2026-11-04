namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

/// <summary>
/// "Open a screen (.NET 11)": opens the same heavy client-style screen
/// (Northbeam Dispatch) inflated two ways — XamlC, what published apps ship
/// today, and SourceGen, the .NET 11 default.
///
/// The page swaps the screen in as its own content (see OpenScreenPage) rather
/// than navigating, so the number is inflate + attach + handlers + layout +
/// draw, without Shell navigation in it.
///
/// Step 1: open/close only. The tap → first-frame stopwatch (split into
/// "Build" = new view and "Show" = handlers + Android up to the first drawn
/// frame) comes next, once the screen itself is approved.
/// </summary>
public partial class OpenScreenViewModel : BaseViewModel
{
    public override string Title => Models.Pages.OpenScreen;

    public string RuntimeText
    {
        get
        {
            var desc = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
#if USE_MONO
            const string runtime = "Mono";
#else
            const string runtime = "CoreCLR";
#endif
            return $"{runtime} · {desc}";
        }
    }
}
