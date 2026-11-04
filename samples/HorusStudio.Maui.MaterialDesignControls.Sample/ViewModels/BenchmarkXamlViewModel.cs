using System.Collections.ObjectModel;
using System.Diagnostics;
using HorusStudio.Maui.MaterialDesignControls.Sample.Benchmarks.XamlProbes;
using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

/// <summary>
/// Round 5 — the .NET 11 XAML pipeline, measured against both runtimes.
///
/// Three build-time knobs are compared, each between XAML files whose markup is
/// byte-identical (see the MauiXaml item metadata in the csproj):
///
///   1. Inflator:   Runtime (reflection) vs XamlC (IL) vs SourceGen (plain C#)
///   2. LazyRD:     resources as factories vs everything built in the ctor
///   3. x:DataType: compiled TypedBindings vs reflection-based classic bindings
///
/// The thesis under test is the one the Skia round left behind: CoreCLR's lead
/// over Mono grows with the amount of abstraction in the managed code. All
/// three knobs above *remove* abstraction, so the interesting question is not
/// which runtime wins, but whether the new XAML closes the gap between them —
/// and therefore which runtime gains more from adopting it.
/// </summary>
public partial class BenchmarkXamlViewModel : BaseViewModel
{
    #region Workload sizes

    // Inflation: 8 x 25 = 200 views per variant. Batched so the UI thread can
    // breathe in between; a single 200-view loop is long enough to trip an ANR
    // watchdog on the Runtime inflator.
    private const int InflateBatches = 8;
    private const int InflatePerBatch = 25;

    // Dictionaries: 5 x 8 = 40 dictionaries of 120 entries each.
    private const int DictBatches = 5;
    private const int DictPerBatch = 8;

    // The "realistic" slice: how many of the 120 keys a page actually touches.
    private const int DictHotKeys = 12;

    // Bindings: 5 x 20 = 100 views per variant, 40 bindings each.
    private const int BindBatches = 5;
    private const int BindPerBatch = 20;

    // Propagation: this many live instances receive this many rounds of
    // notifications. 20 x 40 bindings x 50 rounds = 40.000 property updates.
    private const int BindLiveInstances = 20;
    private const int BindBumpRounds = 50;

    #endregion

    // Group palette, shared by the bars and the headline chips.
    private static readonly Color InflateColor = Color.FromArgb("#7E57C2");
    private static readonly Color DictColor = Color.FromArgb("#26A69A");
    private static readonly Color BindColor = Color.FromArgb("#FFA726");
    private static readonly Color BaselineColor = Color.FromArgb("#4CAF50");
    private static readonly Color WorseColor = Color.FromArgb("#EF5350");

    /// <summary>
    /// Keeps the last constructed object reachable until the batch is timed and
    /// done, so nothing in the loop can be optimised away as dead code.
    /// </summary>
    private static object? _sink;

    public override string Title => Models.Pages.BenchmarkXaml;

    [ObservableProperty] private ObservableCollection<XamlBenchmarkRow> _inflateRows = new();
    [ObservableProperty] private ObservableCollection<XamlBenchmarkRow> _dictRows = new();
    [ObservableProperty] private ObservableCollection<XamlBenchmarkRow> _bindRows = new();

    // Headline: one ratio per axis, which is the whole story in three numbers.
    [ObservableProperty] private string _inflateHeadline = "—";
    [ObservableProperty] private string _dictHeadline = "—";
    [ObservableProperty] private string _bindHeadline = "—";

    [ObservableProperty] private string _runtimeText = string.Empty;
    [ObservableProperty] private string _statusText = "tap Run benchmark";
    [ObservableProperty] private string _runLabel = string.Empty;
    [ObservableProperty] private bool _hasResult;

    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(CanRun))]
    private bool _isRunning;

    public bool CanRun => !IsRunning;

    private int _runCount;

    public BenchmarkXamlViewModel()
    {
        RuntimeText = DescribeRuntime();
    }

    public override void Initialize() { }

    /// <summary>
    /// Reports which runtime the build is actually running on, so a screenshot
    /// of the page is self-describing. FrameworkDescription can't tell them
    /// apart on .NET 11 (Mono also reports ".NET 11.0..."), and Mono.Runtime is
    /// trimmed away under AndroidLinkMode=Full, so this uses the same USE_MONO
    /// build constant as the "Running with..." label on MainPage.
    /// </summary>
    private static string DescribeRuntime()
    {
        var desc = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
#if USE_MONO
        const string runtime = "Mono";
#else
        const string runtime = "CoreCLR";
#endif
        return $"{runtime} · {desc}";
    }

    #region Measurement harness

    private readonly record struct Sample(double Ms, long Bytes)
    {
        public static Sample operator +(Sample a, Sample b) => new(a.Ms + b.Ms, a.Bytes + b.Bytes);
    }

    /// <summary>
    /// Times <paramref name="batch"/> once, with a full collection before the
    /// clock starts so the measured window never pays for the previous batch's
    /// garbage. Allocation is read per-thread, which is why everything here
    /// runs on the UI thread and never hops.
    /// </summary>
    private static Sample MeasureBatch(Action batch)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var before = GC.GetAllocatedBytesForCurrentThread();
        var sw = Stopwatch.StartNew();
        batch();
        sw.Stop();
        var after = GC.GetAllocatedBytesForCurrentThread();

        return new Sample(sw.Elapsed.TotalMilliseconds, after - before);
    }

    /// <summary>Runs a batched phase, yielding to the UI thread between batches.</summary>
    private static async Task<Sample> RunPhaseAsync(int batches, Action batch)
    {
        var total = new Sample(0, 0);
        for (var b = 0; b < batches; b++)
        {
            total += MeasureBatch(batch);
            await Task.Yield();
        }
        return total;
    }

    #endregion

    #region Workloads

    private static void InflateBatch<TView>(Func<TView> factory) where TView : View
    {
        object? last = null;
        for (var i = 0; i < InflatePerBatch; i++)
            last = factory();
        _sink = last;
    }

    /// <summary>
    /// One dictionary batch, split into the three windows that tell the lazy
    /// story: building the dictionaries, resolving the handful of keys a real
    /// page touches, and then resolving everything that was left over.
    /// </summary>
    private static (Sample ctor, Sample hot, Sample rest) DictBatch(Func<ResourceDictionary> factory)
    {
        var dicts = new ResourceDictionary[DictPerBatch];
        var keys = XamlDictKeys.All;

        var ctor = MeasureBatch(() =>
        {
            for (var i = 0; i < DictPerBatch; i++)
                dicts[i] = factory();
        });

        object? sink = null;

        var hot = MeasureBatch(() =>
        {
            for (var i = 0; i < DictPerBatch; i++)
                for (var k = 0; k < DictHotKeys; k++)
                    sink = dicts[i][keys[k]];
        });

        var rest = MeasureBatch(() =>
        {
            for (var i = 0; i < DictPerBatch; i++)
                for (var k = DictHotKeys; k < keys.Length; k++)
                    sink = dicts[i][keys[k]];
        });

        _sink = sink;
        Array.Clear(dicts);
        return (ctor, hot, rest);
    }

    /// <summary>
    /// One binding batch: inflate the views, attach the source, then push
    /// notifications through a live subset. Inflation is reported separately
    /// because compiled bindings move work into it — folding it into the
    /// "apply" number would hide exactly the trade-off being measured.
    /// </summary>
    private static (Sample inflate, Sample apply, Sample update) BindBatch<TView>(Func<TView> factory)
        where TView : View
    {
        var views = new TView[BindPerBatch];
        var probe = new XamlBindProbe();

        var inflate = MeasureBatch(() =>
        {
            for (var i = 0; i < BindPerBatch; i++)
                views[i] = factory();
        });

        var apply = MeasureBatch(() =>
        {
            for (var i = 0; i < BindPerBatch; i++)
                views[i].BindingContext = probe;
        });

        // Only the live subset stays attached for the propagation window, so
        // the number does not silently scale with the batch size.
        for (var i = BindLiveInstances; i < BindPerBatch; i++)
            views[i].BindingContext = null;

        var update = MeasureBatch(() =>
        {
            for (var r = 0; r < BindBumpRounds; r++)
                probe.Bump();
        });

        _sink = views;
        return (inflate, apply, update);
    }

    #endregion

    /// <summary>
    /// One logcat line per measured row (tag DOTNET, prefix XAMLBENCH) so cold
    /// runs can be harvested with adb instead of read off screenshots.
    /// </summary>
    private void LogRows()
    {
        var runtime = RuntimeText.Split(' ')[0];
        void Log(string axis, IEnumerable<XamlBenchmarkRow> rows)
        {
            foreach (var r in rows)
                Console.WriteLine(FormattableString.Invariant(
                    $"XAMLBENCH row runtime={runtime} run={RunLabel.Replace(' ', '_')} axis={axis} variant={r.Variant} metric=\"{r.Metric}\" ms={r.Ms:F2} bytes={r.Bytes}"));
        }
        Log("inflator", InflateRows);
        Log("rd", DictRows);
        Log("bindings", BindRows);
    }

    [ICommand]
    private async Task Run()
    {
        if (IsRunning) return;

        IsRunning = true;
        StatusText = "warming up…";

        try
        {
            // Warm-up pass over every variant, discarded. Without it the first
            // variant measured pays the one-time cost of touching the XAML
            // parser, the binding machinery and the reflection caches, and
            // whichever variant happens to run first looks worst.
            try
            {
                await WarmUpAsync();
            }
            catch
            {
                // A probe that cannot even be constructed is reported by its own
                // axis below; the warm-up is not the place to surface it.
            }

            // Let the tiered JIT's background compilations land before the
            // measured pass. On CoreCLR the promoted tier-1 code is what we
            // want to compare against Mono's AOT code, not the tier-0 stubs.
            await Task.Delay(200);

            // Each axis is guarded on its own. The Runtime inflator and the
            // classic bindings are the two paths full trimming is allowed to
            // break, so a failure there has to be reported as a result — not
            // take the other two axes down with it.
            StatusText = "measuring inflation…";
            await GuardAsync(MeasureInflationAsync, m => InflateHeadline = m);

            StatusText = "measuring resource dictionaries…";
            await GuardAsync(MeasureDictionariesAsync, m => DictHeadline = m);

            StatusText = "measuring bindings…";
            await GuardAsync(MeasureBindingsAsync, m => BindHeadline = m);

            _runCount++;
            RunLabel = _runCount == 1 ? "cold" : $"warm #{_runCount}";
            StatusText = $"{DescribeWorkload()}";
            HasResult = true;
            LogRows();
        }
        catch (Exception ex)
        {
            StatusText = $"failed: {ex.Message}";
        }
        finally
        {
            _sink = null;
            IsRunning = false;
        }
    }

    /// <summary>
    /// Runs one axis, turning a failure into a visible headline instead of an
    /// aborted run. A trimmed-away reflection path throws here, and "this path
    /// no longer survives full trimming" is itself a finding worth showing.
    /// </summary>
    private static async Task GuardAsync(Func<Task> phase, Action<string> report)
    {
        try
        {
            await phase();
        }
        catch (Exception ex)
        {
            report($"failed · {ex.GetType().Name}: {ex.Message}");
        }
    }

    private static string DescribeWorkload() =>
        $"{InflateBatches * InflatePerBatch} views · " +
        $"{DictBatches * DictPerBatch} dictionaries × {XamlDictKeys.All.Length} keys · " +
        $"{BindBatches * BindPerBatch} views × {XamlBindProbe.BindingsPerInstance} bindings";

    private static async Task WarmUpAsync()
    {
        // One batch of everything, at the same shape as the real pass.
        InflateBatch(() => new XamlProbeRuntime());
        InflateBatch(() => new XamlProbeXamlC());
        InflateBatch(() => new XamlProbeSourceGen());
        await Task.Yield();

        DictBatch(() => new XamlDictLazy());
        DictBatch(() => new XamlDictEager());
        await Task.Yield();

        BindBatch(() => new XamlBindCompiled());
        BindBatch(() => new XamlBindClassic());
        await Task.Yield();

        _sink = null;
    }

    private async Task MeasureInflationAsync()
    {
        // Measured in reverse order of expected speed, so if anything is still
        // warming up it penalises the variant we expect to win, not the ones
        // we expect to lose.
        var runtime = await RunPhaseAsync(InflateBatches, () => InflateBatch(() => new XamlProbeRuntime()));
        var xamlc = await RunPhaseAsync(InflateBatches, () => InflateBatch(() => new XamlProbeXamlC()));
        var sourcegen = await RunPhaseAsync(InflateBatches, () => InflateBatch(() => new XamlProbeSourceGen()));

        var count = InflateBatches * InflatePerBatch;
        var rows = new List<XamlBenchmarkRow>
        {
            Row("Runtime", $"LoadFromXaml · {count} views", runtime, InflateColor),
            Row("XamlC", $"IL rewrite · {count} views", xamlc, InflateColor),
            Row("SourceGen", $"generated C# · {count} views", sourcegen, InflateColor, isBaseline: true),
        };

        Rank(rows, baseline: sourcegen);
        InflateRows = new ObservableCollection<XamlBenchmarkRow>(rows);
        InflateHeadline = Ratio(runtime.Ms, sourcegen.Ms, "SourceGen vs Runtime");
    }

    private async Task MeasureDictionariesAsync()
    {
        Sample lazyCtor = default, lazyHot = default, lazyRest = default;
        Sample eagerCtor = default, eagerHot = default, eagerRest = default;

        for (var b = 0; b < DictBatches; b++)
        {
            var l = DictBatch(() => new XamlDictLazy());
            lazyCtor += l.ctor; lazyHot += l.hot; lazyRest += l.rest;
            await Task.Yield();

            var e = DictBatch(() => new XamlDictEager());
            eagerCtor += e.ctor; eagerHot += e.hot; eagerRest += e.rest;
            await Task.Yield();
        }

        var n = DictBatches * DictPerBatch;
        var pct = DictHotKeys * 100 / XamlDictKeys.All.Length;

        // The three windows, accumulated: what you pay to build, what you pay
        // once a page has used its handful of resources, and what it costs if
        // every single entry ends up being touched.
        var lazyUsed = lazyCtor + lazyHot;
        var eagerUsed = eagerCtor + eagerHot;
        var lazyAll = lazyUsed + lazyRest;
        var eagerAll = eagerUsed + eagerRest;

        var rows = new List<XamlBenchmarkRow>
        {
            Row("Lazy", $"inflate {n} dictionaries", lazyCtor, DictColor, isBaseline: true),
            Row("Eager", $"inflate {n} dictionaries", eagerCtor, DictColor),
            Row("Lazy", $"inflate + {pct}% of keys used", lazyUsed, DictColor, isBaseline: true),
            Row("Eager", $"inflate + {pct}% of keys used", eagerUsed, DictColor),
            Row("Lazy", "inflate + 100% of keys used", lazyAll, DictColor, isBaseline: true),
            Row("Eager", "inflate + 100% of keys used", eagerAll, DictColor),
        };

        RankPairs(rows);
        DictRows = new ObservableCollection<XamlBenchmarkRow>(rows);
        DictHeadline = Ratio(eagerCtor.Ms, lazyCtor.Ms, "lazy vs eager inflate");
    }

    private async Task MeasureBindingsAsync()
    {
        Sample cInflate = default, cApply = default, cUpdate = default;
        Sample kInflate = default, kApply = default, kUpdate = default;

        for (var b = 0; b < BindBatches; b++)
        {
            var c = BindBatch(() => new XamlBindCompiled());
            cInflate += c.inflate; cApply += c.apply; cUpdate += c.update;
            await Task.Yield();

            var k = BindBatch(() => new XamlBindClassic());
            kInflate += k.inflate; kApply += k.apply; kUpdate += k.update;
            await Task.Yield();
        }

        var views = BindBatches * BindPerBatch;
        var updates = BindBatches * BindLiveInstances * BindBumpRounds * XamlBindProbe.BindingsPerInstance;

        var rows = new List<XamlBenchmarkRow>
        {
            Row("Compiled", $"inflate {views} views", cInflate, BindColor, isBaseline: true),
            Row("Classic", $"inflate {views} views", kInflate, BindColor),
            Row("Compiled", $"attach source to {views} views", cApply, BindColor, isBaseline: true),
            Row("Classic", $"attach source to {views} views", kApply, BindColor),
            Row("Compiled", $"propagate {updates:N0} updates", cUpdate, BindColor, isBaseline: true),
            Row("Classic", $"propagate {updates:N0} updates", kUpdate, BindColor),
        };

        RankPairs(rows);
        BindRows = new ObservableCollection<XamlBenchmarkRow>(rows);
        BindHeadline = Ratio(kUpdate.Ms, cUpdate.Ms, "compiled vs classic updates");
    }

    #region Presentation

    private static XamlBenchmarkRow Row(string variant, string metric, Sample s, Color color, bool isBaseline = false) =>
        new()
        {
            Variant = variant,
            Metric = metric,
            Ms = s.Ms,
            Bytes = s.Bytes,
            MsText = FormatMs(s.Ms),
            AllocText = FormatBytes(s.Bytes),
            BarColor = color,
            IsBaseline = isBaseline,
        };

    /// <summary>Ratios every row against one baseline, and scales the bars together.</summary>
    private static void Rank(List<XamlBenchmarkRow> rows, Sample baseline)
    {
        var max = rows.Max(r => r.Ms);
        foreach (var r in rows)
        {
            SetBar(r, max > 0 ? r.Ms / max : 0);
            ApplyRatio(r, baseline.Ms);
        }
    }

    /// <summary>
    /// Same, for groups laid out as consecutive (baseline, variant) pairs that
    /// each measure a different window — so bars are scaled inside the pair,
    /// not across windows of wildly different magnitude.
    /// </summary>
    private static void RankPairs(List<XamlBenchmarkRow> rows)
    {
        for (var i = 0; i + 1 < rows.Count; i += 2)
        {
            var a = rows[i];
            var b = rows[i + 1];
            var max = Math.Max(a.Ms, b.Ms);

            SetBar(a, max > 0 ? a.Ms / max : 0);
            SetBar(b, max > 0 ? b.Ms / max : 0);

            ApplyRatio(a, a.Ms);
            ApplyRatio(b, a.Ms);
        }
    }

    /// <summary>
    /// Stores the bar as two star columns. The filled part is floored at 1% so
    /// a row that rounds to zero still shows a sliver rather than vanishing.
    /// </summary>
    private static void SetBar(XamlBenchmarkRow row, double fraction)
    {
        var filled = Math.Clamp(fraction, 0.01, 1);
        row.BarFraction = filled;
        row.BarColumns = new ColumnDefinitionCollection(
            new ColumnDefinition(new GridLength(filled, GridUnitType.Star)),
            new ColumnDefinition(new GridLength(1 - filled, GridUnitType.Star)));
    }

    private static void ApplyRatio(XamlBenchmarkRow row, double baselineMs)
    {
        if (row.IsBaseline)
        {
            row.RatioText = "baseline";
            row.RatioColor = BaselineColor;
            return;
        }

        if (baselineMs <= 0)
        {
            row.RatioText = "—";
            return;
        }

        var factor = row.Ms / baselineMs;
        if (factor >= 1)
        {
            row.RatioText = $"{factor:0.0}× slower";
            row.RatioColor = factor >= 1.15 ? WorseColor : Colors.Gray;
        }
        else
        {
            row.RatioText = $"{1 / factor:0.0}× faster";
            row.RatioColor = BaselineColor;
        }
    }

    private static string Ratio(double slowMs, double fastMs, string caption) =>
        fastMs > 0 ? $"{slowMs / fastMs:0.0}× · {caption}" : "—";

    private static string FormatMs(double ms) =>
        ms >= 100 ? $"{ms:0} ms" : ms >= 10 ? $"{ms:0.0} ms" : $"{ms:0.00} ms";

    private static string FormatBytes(long bytes) =>
        bytes >= 1024 * 1024 ? $"{bytes / 1024d / 1024d:0.0} MB" : $"{bytes / 1024d:0} KB";

    #endregion
}
