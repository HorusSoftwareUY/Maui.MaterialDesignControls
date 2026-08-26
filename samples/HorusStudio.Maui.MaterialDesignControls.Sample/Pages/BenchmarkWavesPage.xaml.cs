using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
using SkiaSharp.Views.Maui;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class BenchmarkWavesPage : BaseContentPage<BenchmarkWavesViewModel>
{
    public BenchmarkWavesPage(BenchmarkWavesViewModel vm) : base(vm)
    {
        InitializeComponent();
    }

    private BenchmarkWavesViewModel Vm => BindingContext as BenchmarkWavesViewModel;

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Ambient effect: start automatically. The VM stops itself (and frees
        // its offscreen surface) from Disappearing.
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
    private void ApplyBackend(BenchmarkWavesViewModel vm)
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

    private void OnScaleQuarter(object sender, EventArgs e) => SetScale(0.25f);
    private void OnScaleThird(object sender, EventArgs e)   => SetScale(1f / 3f);
    private void OnScaleHalf(object sender, EventArgs e)    => SetScale(0.5f);
    private void OnScaleFull(object sender, EventArgs e)    => SetScale(1f);

    private void SetScale(float scale)
    {
        if (Vm is not { } vm) return;
        vm.SetScaleCommand.Execute(scale);
        if (!vm.UseGpu && vm.IsRunning)
            CanvasView.InvalidateSurface();
    }
}
