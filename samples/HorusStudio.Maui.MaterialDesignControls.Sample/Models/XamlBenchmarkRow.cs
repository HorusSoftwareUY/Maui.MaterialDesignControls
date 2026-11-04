namespace HorusStudio.Maui.MaterialDesignControls.Sample.Models;

/// <summary>
/// One measured variant inside a group of the XAML benchmark. Rows come in
/// pairs or triplets that share a sub-metric; exactly one of them is the
/// baseline (the .NET 11 default) and the others carry a ratio against it.
/// </summary>
public class XamlBenchmarkRow
{
    /// <summary>Variant name, e.g. "SourceGen" or "Lazy".</summary>
    public string Variant { get; set; } = string.Empty;

    /// <summary>What was timed, e.g. "inflate 200 views".</summary>
    public string Metric { get; set; } = string.Empty;

    public double Ms { get; set; }
    public long Bytes { get; set; }

    public string MsText { get; set; } = string.Empty;
    public string AllocText { get; set; } = string.Empty;

    /// <summary>"baseline" on the .NET 11 default, "3.4x slower" elsewhere.</summary>
    public string RatioText { get; set; } = string.Empty;

    public Color RatioColor { get; set; } = Colors.Gray;

    /// <summary>0-1, relative to the slowest row of the same group.</summary>
    public double BarFraction { get; set; }

    /// <summary>
    /// <see cref="BarFraction"/> expressed as two star-sized columns, which is
    /// how a proportional bar is drawn in XAML without a converter: the first
    /// column is the filled part, the second is the remainder.
    /// </summary>
    public ColumnDefinitionCollection BarColumns { get; set; } =
        new(new ColumnDefinition(new GridLength(1, GridUnitType.Star)),
            new ColumnDefinition(new GridLength(1, GridUnitType.Star)));

    public Color BarColor { get; set; } = Colors.Gray;

    /// <summary>True on the .NET 11 default, which the row highlights.</summary>
    public bool IsBaseline { get; set; }
}
