using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class BenchmarkUIRenderPage : BaseContentPage<BenchmarkUIRenderViewModel>
{
    public BenchmarkUIRenderPage(BenchmarkUIRenderViewModel vm) : base(vm)
    {
        InitializeComponent();
        vm.ControlsReady += OnControlsReady;
    }

    private void OnControlsReady(IList<View> controls, View measureTarget)
    {
        ControlsLayout.Children.Clear();

        foreach (var v in controls)
            ControlsLayout.Children.Add(v);

        // Give the measure phase a populated layout to work with
        if (measureTarget is VerticalStackLayout vsl)
        {
            foreach (var v in controls)
                vsl.Children.Add(new Label { Text = "measure" });
        }
    }

    private void OnRunClicked(object sender, EventArgs e) =>
        (BindingContext as BenchmarkUIRenderViewModel)?.RunCommand.Execute(null);

    private void OnCount50(object sender, EventArgs e)  => SetCount(50);
    private void OnCount100(object sender, EventArgs e) => SetCount(100);
    private void OnCount250(object sender, EventArgs e) => SetCount(250);
    private void OnCount500(object sender, EventArgs e) => SetCount(500);

    private void SetCount(int count) =>
        (BindingContext as BenchmarkUIRenderViewModel)?.SetCountCommand.Execute(count);
}
