namespace HorusStudio.Maui.MaterialDesignControls.Sample.Benchmarks.XamlProbes;

/// <summary>
/// Resource dictionary probe the .NET 11 default: every entry is registered as a factory and
/// inflated the first time something asks for it.
/// The two dictionaries hold the same 120 entries, so the only variable is when
/// the work is paid.
/// </summary>
public partial class XamlDictLazy : ResourceDictionary
{
    public XamlDictLazy() => InitializeComponent();
}
