using System.Diagnostics;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Utils;

#if ENABLE_NAV_TIMING

/// <summary>
/// Measures the elapsed time between a navigation trigger (T0) and the moment
/// the destination page reports itself as fully ready (T1).
/// T0 = just before Shell.GoToAsync.
/// T1 = one dispatch cycle after OnNavigatedTo (so the initial render pass completes),
///      or whenever the ViewModel explicitly calls Complete() for pages with async data.
/// </summary>
public static class NavigationTimer
{
    private static readonly Stopwatch _sw = new();
    private static string _pendingPage = string.Empty;
    private static bool _completed;

    /// <summary>Fired on the main thread when a navigation timing is available.</summary>
    public static event Action<string, long>? NavTimingCompleted;

    /// <summary>Called just before Shell.GoToAsync — starts the stopwatch.</summary>
    public static void Start(string destinationRoute)
    {
        _pendingPage = destinationRoute;
        _completed = false;
        _sw.Restart();
    }

    /// <summary>
    /// Called by a page/VM when it considers itself fully rendered.
    /// Idempotent — only fires the event once per navigation.
    /// </summary>
    public static void Complete(string pageTitle)
    {
        if (_completed) return;
        _completed = true;
        _sw.Stop();
        var ms = _sw.ElapsedMilliseconds;

        MainThread.BeginInvokeOnMainThread(() =>
            NavTimingCompleted?.Invoke(string.IsNullOrEmpty(pageTitle) ? _pendingPage : pageTitle, ms));
    }
}

#endif
