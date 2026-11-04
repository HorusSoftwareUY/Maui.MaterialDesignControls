namespace HorusStudio.Maui.MaterialDesignControls.Sample.Benchmarks.XamlProbes;

/// <summary>
/// Inflation probe compiled with the <c>XamlC</c> inflator (see the
/// <c>MauiXaml</c> item metadata in the csproj). The markup of the three
/// probes is byte-identical except for the class name, so any difference
/// between them is the inflator and nothing else.
/// </summary>
public partial class XamlProbeXamlC : ContentView
{
    public XamlProbeXamlC() => InitializeComponent();
}
