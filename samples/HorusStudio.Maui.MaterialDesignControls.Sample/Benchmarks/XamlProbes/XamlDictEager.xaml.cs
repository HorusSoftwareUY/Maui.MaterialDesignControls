namespace HorusStudio.Maui.MaterialDesignControls.Sample.Benchmarks.XamlProbes;

/// <summary>
/// Resource dictionary probe built with <c>LazyRD="false"</c> in the csproj, which is the
/// pre-.NET 11 behaviour: every entry is constructed up front.
/// The two dictionaries hold the same 120 entries, so the only variable is when
/// the work is paid.
/// </summary>
public partial class XamlDictEager : ResourceDictionary
{
    public XamlDictEager() => InitializeComponent();
}
