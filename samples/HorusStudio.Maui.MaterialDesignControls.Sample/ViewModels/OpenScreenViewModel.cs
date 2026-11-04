using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

/// <summary>
/// "Open a screen (.NET 11)": opens the same heavy client-style screen
/// (Northbeam Dispatch) inflated two ways — XamlC, what published apps ship
/// today, and SourceGen, the .NET 11 default — and shows how long it took from
/// the tap to the first drawn frame, split into Build and Show.
///
/// Protocol (see the plan doc): cold only. One open per app launch; the chip
/// says "cold ✓" only for the first open in the process.
///
/// The timing itself lives in OpenScreenPage (it needs the native view); this
/// class only holds and formats the results.
/// </summary>
public partial class OpenScreenViewModel : BaseViewModel
{
    public override string Title => Models.Pages.OpenScreen;

#if USE_MONO
    public const string RuntimeName = "Mono";
#else
    public const string RuntimeName = "CoreCLR";
#endif

    public string RuntimeText =>
        $"{RuntimeName} · {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}";

    // ── Last open ──
    [ObservableProperty] private bool _hasResult;
    [ObservableProperty] private bool _overlayVisible;
    [ObservableProperty] private string _totalText = "—";
    [ObservableProperty] private string _overlayText = "";
    [ObservableProperty] private string _resultTitle = "";
    [ObservableProperty] private string _buildText = "";
    [ObservableProperty] private string _showText = "";
    [ObservableProperty] private GridLength _buildStar = new(1, GridUnitType.Star);
    [ObservableProperty] private GridLength _showStar = new(1, GridUnitType.Star);
    [ObservableProperty] private string _chipText = "";
    [ObservableProperty] private Color _chipColor = Colors.Gray;

    // ── Last value per button, this process ──
    [ObservableProperty] private string _lastXamlC = "—";
    [ObservableProperty] private string _lastSourceGen = "—";

    public readonly record struct OpenResult(bool IsCold);

    public OpenResult Record(string inflator, double buildMs, double showMs, bool isCold, int openIndex)
    {
        var total = buildMs + showMs;

        TotalText = $"{total:F0} ms";
        ResultTitle = $"{RuntimeName} · {inflator}";
        BuildText = $"Build (XAML) {buildMs:F1} ms";
        ShowText = $"Show (handlers + Android) {showMs:F1} ms";
        BuildStar = new GridLength(Math.Max(buildMs, 0.1), GridUnitType.Star);
        ShowStar = new GridLength(Math.Max(showMs, 0.1), GridUnitType.Star);
        ChipText = isCold ? "cold ✓" : $"warm #{openIndex} · not counted";
        ChipColor = isCold ? Color.FromArgb("#2DD4BF") : Color.FromArgb("#F2545B");
        OverlayText = $"{total:F0} ms · {(isCold ? "cold" : "warm")}";
        OverlayVisible = true;
        HasResult = true;

        var summary = $"{total:F0} ms ({(isCold ? "cold" : "warm")})";
        if (inflator == "XamlC") LastXamlC = summary; else LastSourceGen = summary;

        return new OpenResult(isCold);
    }

    public void ScreenClosed() => OverlayVisible = false;
}
