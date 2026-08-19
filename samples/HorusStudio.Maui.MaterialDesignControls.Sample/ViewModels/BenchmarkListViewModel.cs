using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;
using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

public partial class BenchmarkListViewModel : BaseViewModel
{
    private const int ItemCount = 10_000;

    // Seeded so every run — and every runtime — sees byte-identical input.
    private const int DataSeed = 42;

    // Synthetic identities: names are drawn from these pools, so no real person
    // is represented. English-language pool — the sample targets a global audience.
    private static readonly string[] FirstNames =
    {
        "James", "Olivia", "Ethan", "Emma", "Liam", "Ava", "Noah", "Sophia",
        "Mason", "Isabella", "Lucas", "Mia", "Henry", "Charlotte", "Owen", "Amelia",
        "Jack", "Harper", "Leo", "Evelyn", "Nathan", "Grace", "Oliver", "Chloe",
    };

    private static readonly string[] LastNames =
    {
        "Bennett", "Carter", "Mitchell", "Harrison", "Brooks", "Sullivan", "Parker", "Hayes",
        "Coleman", "Foster", "Reeves", "Wallace", "Hudson", "Barrett", "Fletcher", "Grayson",
        "Whitaker", "Sinclair", "Prescott", "Ashford", "Donovan", "Kingsley", "Marshall", "Rowland",
    };

    private static readonly string[] Domains = { "acme.io", "northwind.co", "globex.com", "initech.dev" };

    // Uneven on purpose: a real org is not a uniform distribution.
    private static readonly string[] DepartmentPool =
    {
        "Engineering", "Engineering", "Engineering", "Engineering",
        "Sales", "Sales", "Sales",
        "Marketing", "Marketing",
        "Design", "Design",
        "Finance", "HR",
    };

    private static readonly string[][] TagPool =
    {
        new[] { "senior", "backend" },
        new[] { "remote", "fulltime" },
        new[] { "junior", "frontend" },
        new[] { "lead", "mobile" },
        new[] { "contract", "data" },
        new[] { "intern", "devops" },
    };

    private static readonly Color ScoreHigh     = Color.FromArgb("#2E7D32");
    private static readonly Color ScoreMid      = Color.FromArgb("#EF6C00");
    private static readonly Color ScoreLow      = Color.FromArgb("#B3261E");
    private static readonly Color ActiveColor   = Color.FromArgb("#2E7D32");
    private static readonly Color InactiveColor = Color.FromArgb("#79747E");

    private static readonly Color[] AvatarPalette =
    {
        Color.FromArgb("#5E35B1"), Color.FromArgb("#00897B"), Color.FromArgb("#C2185B"),
        Color.FromArgb("#1E88E5"), Color.FromArgb("#F4511E"), Color.FromArgb("#43A047"),
        Color.FromArgb("#6D4C41"), Color.FromArgb("#3949AB"),
    };

    // Phase palette, shared by the stacked bar and its legend.
    public static Color Phase1Color { get; } = Color.FromArgb("#7E57C2");
    public static Color Phase2Color { get; } = Color.FromArgb("#26A69A");
    public static Color Phase3Color { get; } = Color.FromArgb("#FFA726");
    public static Color Phase4Color { get; } = Color.FromArgb("#EF5350");

    private string _cachedJson = string.Empty;
    private long[] _previousPhases;
    private long _previousTotal;

    public override string Title => Models.Pages.BenchmarkList;

    [ObservableProperty] private ObservableCollection<BenchmarkItem> _items = new();

    // Hero stats
    [ObservableProperty] private string _totalMsText = "—";
    [ObservableProperty] private string _throughputText = "tap Run benchmark";
    [ObservableProperty] private bool _hasResult;

    // Phase readouts: "128 ms" + "26%"
    [ObservableProperty] private string _phase1Text = "—";
    [ObservableProperty] private string _phase2Text = "—";
    [ObservableProperty] private string _phase3Text = "—";
    [ObservableProperty] private string _phase4Text = "—";
    [ObservableProperty] private string _phase1Percent = string.Empty;
    [ObservableProperty] private string _phase2Percent = string.Empty;
    [ObservableProperty] private string _phase3Percent = string.Empty;
    [ObservableProperty] private string _phase4Percent = string.Empty;

    // Proportional stacked bars (current run + ghost of the previous one)
    [ObservableProperty] private ColumnDefinitionCollection _phaseColumns = BuildColumns(1, 1, 1, 1);
    [ObservableProperty] private ColumnDefinitionCollection _previousColumns = BuildColumns(1, 1, 1, 1);
    [ObservableProperty] private bool _hasPrevious;
    [ObservableProperty] private string _deltaText = string.Empty;
    [ObservableProperty] private Color _deltaColor = Colors.Gray;

    [ObservableProperty] private string _errorText = string.Empty;
    [ObservableProperty] private bool _hasError;

    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(CanRun))]
    private bool _isLoading;

    /// <summary>
    /// True while the result is being revealed on screen. The run button stays
    /// disabled during it so a second tap cannot land its main-thread work
    /// (phase 4) in the middle of the reveal.
    /// </summary>
    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(CanRun))]
    private bool _isRevealing;

    public bool CanRun => !IsLoading && !IsRevealing;

    /// <summary>0→1 while revealing; drives the horizontal growth of the bars.</summary>
    [ObservableProperty] private double _revealFraction = 1;

    /// <summary>
    /// "cold" on the first run of the session, "warm #2", "#3"… afterwards.
    /// The first run pays JIT/tiering warm-up on the whole pipeline, so mixing
    /// it with later runs in a comparison is not valid. See the docs note.
    /// </summary>
    [ObservableProperty] private string _runLabel = string.Empty;

    private int _runCount;

    public override void Initialize() { }

    [ICommand]
    private async Task LoadItems()
    {
        if (IsLoading) return;

        IsLoading = true;
        ErrorText = string.Empty;
        HasError = false;
        Phase1Text = Phase2Text = Phase3Text = Phase4Text = "…";
        Phase1Percent = Phase2Percent = Phase3Percent = Phase4Percent = string.Empty;
        TotalMsText = "…";
        ThroughputText = "running";
        RevealFraction = 0;

        // Empty the list first so the reload reads as a reload: the list blinks
        // instead of being swapped underneath the user. Deliberately not padded
        // with an artificial delay — the flash is the whole feedback we want.
        Items = new ObservableCollection<BenchmarkItem>();

        try
        {
            var (p1, p2, p3, headlessMs, mapped) = await Task.Run(() =>
            {
                // Build JSON on first run
                if (string.IsNullOrEmpty(_cachedJson))
                    _cachedJson = BuildJson(ItemCount);

                var total = Stopwatch.StartNew();

                // Phase 1 — JSON deserialize
                var sw = Stopwatch.StartNew();
                var dtos = JsonSerializer.Deserialize(_cachedJson, SampleJsonContext.Default.ListBenchmarkRawDto)!;
                sw.Stop();
                var ms1 = sw.ElapsedMilliseconds;

                // Phase 2 — LINQ: map + sort by score desc
                sw.Restart();
                var list = dtos
                    .Select((dto, i) => MapItem(dto, i + 1))
                    .OrderByDescending(x => x.Score)
                    .ToList();
                sw.Stop();
                var ms2 = sw.ElapsedMilliseconds;

                // Phase 3 — date parse + format (InvariantCulture to avoid locale issues)
                sw.Restart();
                foreach (var item in list)
                {
                    if (DateTime.TryParseExact(item.FormattedDate, "yyyy-MM-dd",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                        item.FormattedDate = dt.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);
                }
                sw.Stop();
                var ms3 = sw.ElapsedMilliseconds;

                total.Stop();
                return (ms1, ms2, ms3, total.ElapsedMilliseconds, list);
            });

            // Phase 4 — bind on the main thread
            var bindSw = Stopwatch.StartNew();
            Items = new ObservableCollection<BenchmarkItem>(mapped);
            bindSw.Stop();
            var p4 = bindSw.ElapsedMilliseconds;

            var totalMs = headlessMs + p4;
            PublishResult(p1, p2, p3, p4, totalMs);

            // Everything above is measured; the reveal below is pure presentation
            // and runs after the numbers are already final.
            await RevealAsync(totalMs);
        }
        catch (Exception ex)
        {
            ErrorText = $"Error: {ex.GetType().Name} — {ex.Message}";
            HasError = true;
            TotalMsText = "failed";
            ThroughputText = string.Empty;
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void PublishResult(long p1, long p2, long p3, long p4, long totalMs)
    {
        // Ghost bar: the run before this one, for at-a-glance A/B.
        if (_previousPhases is { } prev)
        {
            PreviousColumns = BuildColumns(prev[0], prev[1], prev[2], prev[3]);
            HasPrevious = true;

            if (_previousTotal > 0)
            {
                var deltaPct = (totalMs - _previousTotal) * 100.0 / _previousTotal;
                var sign = deltaPct >= 0 ? "+" : "−";
                DeltaText = $"{sign}{Math.Abs(deltaPct):F0}% vs previous ({_previousTotal} ms)";
                DeltaColor = deltaPct <= 0 ? ScoreHigh : ScoreLow;
            }
        }

        var sum = Math.Max(1, p1 + p2 + p3 + p4);
        PhaseColumns = BuildColumns(p1, p2, p3, p4);

        Phase1Text = $"{p1} ms";
        Phase2Text = $"{p2} ms";
        Phase3Text = $"{p3} ms";
        Phase4Text = $"{p4} ms";
        Phase1Percent = $"{p1 * 100 / sum}%";
        Phase2Percent = $"{p2 * 100 / sum}%";
        Phase3Percent = $"{p3 * 100 / sum}%";
        Phase4Percent = $"{p4 * 100 / sum}%";

        TotalMsText = $"{totalMs} ms";
        var perSecond = totalMs > 0 ? ItemCount * 1000.0 / totalMs : 0;
        ThroughputText = $"{ItemCount:N0} items · {perSecond:N0} items/s";
        HasResult = true;

        _runCount++;
        RunLabel = _runCount == 1 ? "cold" : $"warm #{_runCount}";

        _previousPhases = new[] { p1, p2, p3, p4 };
        _previousTotal = totalMs;
    }

    /// <summary>
    /// Presentation only, and deliberately timer-driven rather than built on
    /// MAUI's animation API: with "animator duration scale" off in the device's
    /// developer options MAUI completes animations instantly, which would make
    /// this invisible on exactly the phones used for testing.
    /// </summary>
    private async Task RevealAsync(long totalMs)
    {
        const int durationMs = 420;

        IsRevealing = true;
        var sw = Stopwatch.StartNew();

        double t;
        while ((t = sw.Elapsed.TotalMilliseconds / durationMs) < 1)
        {
            var eased = 1 - Math.Pow(1 - t, 3);   // ease-out cubic
            RevealFraction = eased;
            TotalMsText = $"{(long)Math.Round(totalMs * eased)} ms";
            await Task.Delay(16);
        }

        RevealFraction = 1;
        TotalMsText = $"{totalMs} ms";
        IsRevealing = false;
    }

    private static ColumnDefinitionCollection BuildColumns(params long[] weights)
        => BuildColumns(weights.Select(w => (double)w).ToArray());

    private static ColumnDefinitionCollection BuildColumns(params double[] weights)
    {
        var columns = new ColumnDefinitionCollection();
        foreach (var w in weights)
        {
            // A zero-width star column would make the segment vanish entirely;
            // the floor keeps the bar's geometry stable across runs.
            var value = Math.Max(w, 0.0001);
            columns.Add(new ColumnDefinition { Width = new GridLength(value, GridUnitType.Star) });
        }
        return columns;
    }

    private static BenchmarkItem MapItem(BenchmarkRawDto dto, int index)
    {
        var score = dto.Score;
        var parts = dto.Name.Split(' ');
        var initials = parts.Length >= 2
            ? $"{parts[0][0]}{parts[1][0]}"
            : dto.Name.Length > 0 ? dto.Name[..1] : "?";

        return new BenchmarkItem
        {
            Index         = index,
            Title         = dto.Name,
            Email         = dto.Email,
            Department    = dto.Department,
            Score         = score,
            ScoreText     = score.ToString(),
            FormattedDate = dto.RegisteredAt,
            TagsDisplay   = string.Join(" · ", dto.Tags),
            Tag1          = dto.Tags.Length > 0 ? dto.Tags[0] : string.Empty,
            Tag2          = dto.Tags.Length > 1 ? dto.Tags[1] : string.Empty,
            IsActive      = dto.IsActive,
            ScoreColor    = score >= 75 ? ScoreHigh : score >= 40 ? ScoreMid : ScoreLow,
            StatusColor   = dto.IsActive ? ActiveColor : InactiveColor,
            StatusLabel   = dto.IsActive ? "Active" : "Inactive",
            Initials      = initials.ToUpperInvariant(),
            AvatarColor   = AvatarPalette[(uint)dto.Id % AvatarPalette.Length],
        };
    }

    private static string BuildJson(int count)
    {
        // Deterministic generator: same seed → same dataset on every device and
        // runtime, so load timings stay comparable between runs.
        var rng = new Random(DataSeed);
        var sb = new StringBuilder(count * 190);
        sb.Append('[');
        var baseDate = new DateTime(2020, 1, 1);

        for (var i = 0; i < count; i++)
        {
            var first = FirstNames[rng.Next(FirstNames.Length)];
            var last  = LastNames[rng.Next(LastNames.Length)];
            var dept  = DepartmentPool[rng.Next(DepartmentPool.Length)];
            var tags  = TagPool[rng.Next(TagPool.Length)];

            // Bell-ish distribution instead of a linear ramp: most people land
            // mid-table, few at the extremes.
            var score = (int)Math.Clamp((rng.NextDouble() + rng.NextDouble() + rng.NextDouble()) / 3 * 100, 0, 100);

            var date = baseDate.AddDays(rng.Next(0, 2190)).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var active = rng.NextDouble() > 0.22 ? "true" : "false";
            var domain = Domains[rng.Next(Domains.Length)];
            var email = $"{first}.{last}{i % 97}@{domain}".ToLowerInvariant();

            sb.Append($"{{\"id\":{i},\"name\":\"{first} {last}\",\"email\":\"{email}\",\"department\":\"{dept}\",\"score\":{score},\"registered_at\":\"{date}\",\"tags\":[\"{tags[0]}\",\"{tags[1]}\"],\"is_active\":{active}}}");
            if (i < count - 1) sb.Append(',');
        }

        sb.Append(']');
        return sb.ToString();
    }
}
