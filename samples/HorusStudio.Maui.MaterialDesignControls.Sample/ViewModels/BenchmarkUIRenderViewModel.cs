using System.Diagnostics;
using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

public partial class BenchmarkUIRenderViewModel : BaseViewModel
{
    public static readonly int[] Levels = { 50, 100, 250, 500 };

    public override string Title => Models.Pages.BenchmarkUIRender;

    [ObservableProperty] private string _createText  = "1. Create: —";
    [ObservableProperty] private string _addText     = "2. Add: —";
    [ObservableProperty] private string _measureText = "3. Measure (10x avg): —";
    [ObservableProperty] private string _totalText   = "Select count and tap Run";
    [ObservableProperty] private bool   _isRunning;
    [ObservableProperty] private int    _selectedCount = 100;

    public override void Initialize() { }

    // Raised when the ViewModel has controls ready to be added to the layout
    public event Action<IList<View>, View>? ControlsReady;

    [ICommand]
    private void SetCount(int count) => SelectedCount = count;

    [ICommand]
    private void Run()
    {
        IsRunning   = true;
        CreateText  = "1. Create: …";
        AddText     = "2. Add: …";
        MeasureText = "3. Measure: …";
        TotalText   = "Running…";

        var count = SelectedCount;

        Task.Run(() =>
        {
            try
            {
                var total = Stopwatch.StartNew();

                // Phase 1 — create controls on background thread
                var sw = Stopwatch.StartNew();
                var controls = CreateControls(count);
                sw.Stop();
                var p1 = sw.ElapsedMilliseconds;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    CreateText = $"1. Create: {p1} ms  ({count / Math.Max(1.0, p1 / 1000.0):F0} controls/s)";

                    // Phase 2 — add to layout on UI thread
                    var addSw = Stopwatch.StartNew();
                    // Pass a placeholder layout for measure; actual layout injected via event
                    var measureLayout = new VerticalStackLayout();
                    ControlsReady?.Invoke(controls, measureLayout);
                    addSw.Stop();
                    var p2 = addSw.ElapsedMilliseconds;
                    AddText = $"2. Add: {p2} ms  ({count / Math.Max(1.0, p2 / 1000.0):F0} controls/s)";

                    // Phase 3 — measure pass (10 iterations, averaged)
                    var measureSw = Stopwatch.StartNew();
                    for (var i = 0; i < 10; i++)
                        measureLayout.Measure(double.PositiveInfinity, double.PositiveInfinity);
                    measureSw.Stop();
                    var p3avg = measureSw.ElapsedMilliseconds / 10.0;
                    MeasureText = $"3. Measure (10x avg): {p3avg:F1} ms";

                    total.Stop();
                    TotalText  = $"{count} controls — total: {total.ElapsedMilliseconds} ms";
                    IsRunning  = false;
                });
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    TotalText = $"Error: {ex.GetType().Name} — {ex.Message}";
                    IsRunning = false;
                });
            }
        });
    }

    private static List<View> CreateControls(int count)
    {
        var list = new List<View>(count);
        for (var i = 0; i < count; i++)
        {
            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto),
                },
                Padding   = new Thickness(12, 6),
                ColumnSpacing = 8,
            };

            var label = new Label
            {
                Text      = $"Item {i + 1}",
                FontSize  = 14,
                VerticalOptions = LayoutOptions.Center,
            };
            var entry = new Entry
            {
                Placeholder     = $"value {i + 1}",
                FontSize        = 13,
                VerticalOptions = LayoutOptions.Center,
            };
            var btn = new Button
            {
                Text      = "Tap",
                FontSize  = 12,
                Padding   = new Thickness(8, 4),
                HeightRequest = 36,
            };
            var sw = new Microsoft.Maui.Controls.Switch
            {
                IsToggled       = i % 2 == 0,
                VerticalOptions = LayoutOptions.Center,
            };

            grid.Add(label, 0);
            grid.Add(entry, 1);
            grid.Add(btn,   2);
            grid.Add(sw,    3);

            list.Add(grid);
        }
        return list;
    }
}
