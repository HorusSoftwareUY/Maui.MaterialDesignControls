using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

public partial class BenchmarkListViewModel : BaseViewModel
{
    private const int ItemCount = 10_000;

    private static readonly string[] Departments = { "Engineering", "Marketing", "Sales", "HR", "Finance", "Design" };
    private static readonly string[][] TagPool =
    {
        new[] { "senior", "backend" },
        new[] { "remote", "fulltime" },
        new[] { "junior", "frontend" },
        new[] { "lead", "mobile" },
        new[] { "contract", "data" },
        new[] { "intern", "devops" },
    };

    private static readonly Color ScoreHigh   = Color.FromArgb("#4CAF50");
    private static readonly Color ScoreMid    = Color.FromArgb("#FF9800");
    private static readonly Color ScoreLow    = Color.FromArgb("#B3261E");
    private static readonly Color ActiveColor  = Color.FromArgb("#6750A4");
    private static readonly Color InactiveColor = Color.FromArgb("#79747E");

    private string _cachedJson = string.Empty;

    public override string Title => Models.Pages.BenchmarkList;

    [ObservableProperty] private ObservableCollection<BenchmarkItem> _items = new();
    [ObservableProperty] private string _phase1Text = "—";
    [ObservableProperty] private string _phase2Text = "—";
    [ObservableProperty] private string _phase3Text = "—";
    [ObservableProperty] private string _phase4Text = "—";
    [ObservableProperty] private string _totalText  = "Tap Reload to run";
    [ObservableProperty] private bool _isLoading;

    public override void Initialize()
    {
        // Build JSON on background thread to avoid blocking the UI
        Task.Run(() =>
        {
            _cachedJson = BuildJson(ItemCount);
            MainThread.BeginInvokeOnMainThread(LoadItems);
        });
    }

    [ICommand]
    private void LoadItems()
    {
        if (string.IsNullOrEmpty(_cachedJson)) return;

        IsLoading = true;
        Phase1Text = Phase2Text = Phase3Text = Phase4Text = "…";
        TotalText = "Running…";

        Task.Run(() =>
        {
            var total = Stopwatch.StartNew();

            // Phase 1 — JSON deserialize
            var sw = Stopwatch.StartNew();
            var dtos = JsonSerializer.Deserialize<List<BenchmarkRawDto>>(_cachedJson)!;
            sw.Stop();
            var p1 = sw.ElapsedMilliseconds;

            // Phase 2 — LINQ: map + sort by score desc
            sw.Restart();
            var mapped = dtos
                .Select((dto, i) => MapItem(dto, i + 1))
                .OrderByDescending(x => x.Score)
                .ToList();
            sw.Stop();
            var p2 = sw.ElapsedMilliseconds;

            // Phase 3 — date parse + format on all items
            sw.Restart();
            foreach (var item in mapped)
                item.FormattedDate = DateTime.Parse(item.FormattedDate).ToString("MMM dd, yyyy");
            sw.Stop();
            var p3 = sw.ElapsedMilliseconds;

            total.Stop();
            var totalMs = total.ElapsedMilliseconds;

            // Phase 4 — bind on main thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var bindSw = Stopwatch.StartNew();
                Items = new ObservableCollection<BenchmarkItem>(mapped);
                bindSw.Stop();

                Phase1Text = $"{p1} ms";
                Phase2Text = $"{p2} ms";
                Phase3Text = $"{p3} ms";
                Phase4Text = $"{bindSw.ElapsedMilliseconds} ms";
                TotalText  = $"{ItemCount:N0} items — total: {totalMs + bindSw.ElapsedMilliseconds} ms";
                IsLoading  = false;
            });
        });
    }

    private static BenchmarkItem MapItem(BenchmarkRawDto dto, int index)
    {
        var score = dto.Score;
        return new BenchmarkItem
        {
            Index        = index,
            Title        = dto.Name,
            Email        = dto.Email,
            Department   = dto.Department,
            Score        = score,
            FormattedDate = dto.RegisteredAt, // formatted in phase 3
            TagsDisplay  = string.Join(" · ", dto.Tags),
            IsActive     = dto.IsActive,
            ScoreColor   = score >= 75 ? ScoreHigh : score >= 40 ? ScoreMid : ScoreLow,
            StatusColor  = dto.IsActive ? ActiveColor : InactiveColor,
            StatusLabel  = dto.IsActive ? "Active" : "Inactive",
        };
    }

    private static string BuildJson(int count)
    {
        var sb = new StringBuilder(count * 180);
        sb.Append('[');
        var baseDate = new DateTime(2020, 1, 1);
        for (var i = 0; i < count; i++)
        {
            var dept = Departments[i % Departments.Length];
            var tags = TagPool[i % TagPool.Length];
            var score = (i * 97 + 13) % 101;
            var date = baseDate.AddDays(i % 1825).ToString("yyyy-MM-dd");
            var active = i % 3 != 0 ? "true" : "false";
            sb.Append($"{{\"id\":{i},\"name\":\"User {i:D5}\",\"email\":\"user{i}@example.com\",\"department\":\"{dept}\",\"score\":{score},\"registered_at\":\"{date}\",\"tags\":[\"{tags[0]}\",\"{tags[1]}\"],\"is_active\":{active}}}");
            if (i < count - 1) sb.Append(',');
        }
        sb.Append(']');
        return sb.ToString();
    }
}
