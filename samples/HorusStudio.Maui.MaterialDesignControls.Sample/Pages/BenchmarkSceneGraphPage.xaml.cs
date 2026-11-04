using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class BenchmarkSceneGraphPage : BaseContentPage<BenchmarkSceneGraphViewModel>
{
    public BenchmarkSceneGraphPage(BenchmarkSceneGraphViewModel vm) : base(vm)
    {
        InitializeComponent();
    }

    private BenchmarkSceneGraphViewModel Vm => BindingContext as BenchmarkSceneGraphViewModel;

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
        // Kill the GL render loop before BaseContentPage triggers vm.Disappearing.
        GlView.HasRenderLoop = false;
        base.OnDisappearing();
    }

    /// <summary>
    /// Shows the view matching the selected backend and kicks its frame loop:
    /// the GL view renders continuously via its native render loop, the CPU
    /// view is driven by the InvalidateSurface chain in OnPaintSurface.
    /// </summary>
    private void ApplyBackend(BenchmarkSceneGraphViewModel vm)
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
            gpu: true,
            e.Surface.Context as GRContext);
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

    private void OnShapeClicked(object sender, EventArgs e)
        => Vm?.ToggleShapeCommand.Execute(null);

    private void OnCount1k(object sender, EventArgs e) => SetCount(1_000);
    private void OnCount5k(object sender, EventArgs e) => SetCount(5_000);
    private void OnCount10k(object sender, EventArgs e) => SetCount(10_000);
    private void OnCount20k(object sender, EventArgs e) => SetCount(20_000);

    private void SetCount(int count)
    {
        if (Vm is not { } vm) return;
        vm.SetCountCommand.Execute(count);
        if (!vm.UseGpu && vm.IsRunning)
            CanvasView.InvalidateSurface();
    }
}
