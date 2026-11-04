namespace HorusStudio.Maui.MaterialDesignControls.Sample.Benchmarks.XamlProbes;

/// <summary>
/// Binding probe with <c>x:DataType</c> set, so the source generator emits a
/// compiled <c>TypedBinding</c> for each of its 40 bindings.
/// </summary>
public partial class XamlBindCompiled : ContentView
{
    public XamlBindCompiled() => InitializeComponent();
}
