namespace HorusStudio.Maui.MaterialDesignControls.Sample.Models;

public class BenchmarkItem
{
    public int Index { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int Score { get; set; }
    public string ScoreText { get; set; } = string.Empty;
    public string FormattedDate { get; set; } = string.Empty;
    public string TagsDisplay { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Color ScoreColor { get; set; } = Colors.Gray;
    public Color StatusColor { get; set; } = Colors.Gray;
    public string StatusLabel { get; set; } = string.Empty;

    // Rich row template
    public string Initials { get; set; } = string.Empty;
    public Color AvatarColor { get; set; } = Colors.Gray;
    public string Tag1 { get; set; } = string.Empty;
    public string Tag2 { get; set; } = string.Empty;
}
