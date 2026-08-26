using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
using SkiaSharp.Views.Maui;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class BenchmarkFluidPage : BaseContentPage<BenchmarkFluidViewModel>
{
    public BenchmarkFluidPage(BenchmarkFluidViewModel vm) : base(vm)
    {
        InitializeComponent();
    }

    private BenchmarkFluidViewModel Vm => BindingContext as BenchmarkFluidViewModel;

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (Vm is { } vm)
        {
            vm.Start();
            ApplyBackend(vm);
        }
    }

    protected override void OnDisappearing()
    {
        GlView.HasRenderLoop = false;
        base.OnDisappearing();
    }

    private void ApplyBackend(BenchmarkFluidViewModel vm)
    {
        CanvasView.IsVisible = !vm.UseGpu;
        GlView.IsVisible = vm.UseGpu;

        if (vm.UseGpu)
        {
            GlView.HasRenderLoop = vm.IsRunning;
        }
        else
        {
            GlView.HasRenderLoop = false;
            if (vm.IsRunning)
                CanvasView.InvalidateSurface();
        }
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        if (Vm is not { } vm) return;

        var shouldContinue = vm.OnPaintSurface(
            e.Surface.Canvas,
            e.Info.Width,
            e.Info.Height,
            gpu: false);

        if (shouldContinue)
            CanvasView.InvalidateSurface();
    }

    private void OnPaintGlSurface(object sender, SKPaintGLSurfaceEventArgs e)
    {
        if (Vm is not { } vm) return;

        vm.OnPaintSurface(
            e.Surface.Canvas,
            e.BackendRenderTarget.Width,
            e.BackendRenderTarget.Height,
            gpu: true);
    }

    private void OnToggleClicked(object sender, EventArgs e)
    {
        if (Vm is not { } vm) return;
        vm.ToggleRunningCommand.Execute(null);
        ApplyBackend(vm);
    }

    private void OnBackendClicked(object sender, EventArgs e)
    {
        if (Vm is not { } vm) return;
        vm.ToggleBackendCommand.Execute(null);
        ApplyBackend(vm);
    }

    private void OnBenchClicked(object sender, EventArgs e)
    {
        if (Vm is not { } vm) return;
        vm.RunBenchmarkCommand.Execute(null);
        ApplyBackend(vm);
    }
}
