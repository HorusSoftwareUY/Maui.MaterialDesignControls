using System.Text.Json.Serialization;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Models;

/// <summary>
/// Source-generated JsonSerializerContext for trimming-safe serialization.
/// Required when building with AndroidLinkMode=Full (Mono AOT).
/// </summary>
[JsonSerializable(typeof(List<BenchmarkRawDto>))]
[JsonSerializable(typeof(BenchmarkRawDto))]
internal sealed partial class SampleJsonContext : JsonSerializerContext { }
