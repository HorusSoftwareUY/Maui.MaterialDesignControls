using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class NavTimingOverlay : ContentView
{
    private CancellationTokenSource? _dismissCts;

    public NavTimingOverlay()
    {
        InitializeComponent();

#if ENABLE_NAV_TIMING
        NavigationTimer.NavTimingCompleted += OnNavTimingCompleted;
#endif

        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (_, _) => Dismiss();
        GestureRecognizers.Add(tapGesture);

        IsVisible = false;
    }

#if ENABLE_NAV_TIMING
    private async void OnNavTimingCompleted(string pageTitle, long ms)
    {
        _dismissCts?.Cancel();
        var cts = new CancellationTokenSource();
        _dismissCts = cts;

        TimingLabel.Text = $"⏱ {pageTitle}  —  {ms} ms";
        IsVisible = true;
        Opacity = 1;

        try
        {
            await Task.Delay(TimeSpan.FromSeconds(10), cts.Token);
            Dismiss();
        }
        catch (TaskCanceledException) { /* cancelled by new event or manual dismiss */ }
    }
#endif

    public void Dismiss()
    {
        _dismissCts?.Cancel();
        IsVisible = false;
    }

    [Obsolete("Use Dismiss() instead")]
    public void DismissAsync() => Dismiss();
}
