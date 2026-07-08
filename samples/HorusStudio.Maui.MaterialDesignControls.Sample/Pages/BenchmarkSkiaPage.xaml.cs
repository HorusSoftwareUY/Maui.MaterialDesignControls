using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class BenchmarkSkiaPage : BaseContentPage<BenchmarkSkiaViewModel>
{
    public BenchmarkSkiaPage(BenchmarkSkiaViewModel vm) : base(vm)
    {
        InitializeComponent();
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var vm = BindingContext as BenchmarkSkiaViewModel;
        if (vm is null) return;

        var shouldContinue = vm.OnPaintSurface(
            e.Surface.Canvas,
            e.Info.Width,
            e.Info.Height);

        if (shouldContinue)
            CanvasView.InvalidateSurface();
    }

    private void OnToggleClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as BenchmarkSkiaViewModel;
        vm?.ToggleRunningCommand.Execute(null);
        if (vm?.IsRunning == true)
            CanvasView.InvalidateSurface();
    }

    private void OnCount50(object sender, EventArgs e)  => SetCount(50);
    private void OnCount100(object sender, EventArgs e) => SetCount(100);
    private void OnCount200(object sender, EventArgs e) => SetCount(200);
    private void OnCount500(object sender, EventArgs e) => SetCount(500);

    private void SetCount(int count)
    {
        var vm = BindingContext as BenchmarkSkiaViewModel;
        vm?.SetCountCommand.Execute(count);
    }
}
