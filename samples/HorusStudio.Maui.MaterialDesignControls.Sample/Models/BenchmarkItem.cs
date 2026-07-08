namespace HorusStudio.Maui.MaterialDesignControls.Sample.Models;

public class BenchmarkItem
{
    public int Index { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int Score { get; set; }
    public string FormattedDate { get; set; } = string.Empty;
    public string TagsDisplay { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Color ScoreColor { get; set; } = Colors.Gray;
    public Color StatusColor { get; set; } = Colors.Gray;
    public string StatusLabel { get; set; } = string.Empty;
}
