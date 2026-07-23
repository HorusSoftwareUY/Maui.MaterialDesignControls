using System.Diagnostics;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Utils;

#if ENABLE_STARTUP_PROFILING

/// <summary>
/// Records a startup timeline from T0 (first touch of this class, typically
/// <c>MainApplication.ctor</c>) through the first page <c>OnAppearing</c>.
///
/// Usage:
///   StartupProfiler.Mark("some phase done");   // anywhere in startup
///   StartupProfiler.Dump();                    // prints the full timeline
///
/// Filter output on device:
///   adb logcat -s STARTUP_PROFILE
///
/// Disable at build time:
///   dotnet build -p:EnableStartupProfiling=false
/// </summary>
public static class StartupProfiler
{
    // Stopwatch starts the moment this class is first referenced — that happens
    // when MainApplication.ctor calls Mark(), making it the effective T0.
    private static readonly Stopwatch _sw = Stopwatch.StartNew();
    private static readonly List<(string Label, long Ms)> _marks = new();
    private static bool _dumped;
    private static readonly object _lock = new();

    /// <summary>Records the elapsed time since T0 with the given label.</summary>
    public static void Mark(string label)
    {
        var ms = _sw.ElapsedMilliseconds;
        lock (_lock)
        {
            _marks.Add((label, ms));
        }
    }

    /// <summary>
    /// Logs the full timeline to logcat (Android) or Debug output (other platforms).
    /// Idempotent — subsequent calls are no-ops.
    /// </summary>
    public static void Dump()
    {
        lock (_lock)
        {
            if (_dumped) return;
            _dumped = true;
        }

        var lines = new List<string>(_marks.Count + 2);
        lines.Add("=== Startup timeline ===");
        foreach (var (label, ms) in _marks)
            lines.Add($"  {label,-50} +{ms} ms");

        var total = _marks.Count > 0 ? _marks[^1].Ms : 0;
        lines.Add($"  --- TOTAL STARTUP: {total} ms ---");

        foreach (var line in lines)
            Log(line);
    }

    private static void Log(string message)
    {
#if ANDROID
        global::Android.Util.Log.Info("STARTUP_PROFILE", message);
#else
        Debug.WriteLine($"[STARTUP_PROFILE] {message}");
#endif
    }
}

#endif
