namespace HorusStudio.Maui.MaterialDesignControls.Sample.Benchmarks.XamlProbes;

/// <summary>
/// Same markup as <see cref="XamlBindCompiled"/>, but with
/// <c>x:DataType="{x:Null}"</c>, which opts the subtree out of compiled
/// bindings and back onto the reflection-based binding path.
/// </summary>
public partial class XamlBindClassic : ContentView
{
    public XamlBindClassic() => InitializeComponent();
}
