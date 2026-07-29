using System.Text.Json.Serialization;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Models;

public class BenchmarkRawDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("department")]
    public string Department { get; set; } = string.Empty;

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("registered_at")]
    public string RegisteredAt { get; set; } = string.Empty;

    [JsonPropertyName("tags")]
    public string[] Tags { get; set; } = Array.Empty<string>();

    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }
}
