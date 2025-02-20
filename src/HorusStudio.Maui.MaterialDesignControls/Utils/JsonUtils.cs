using System.Text.Json;
using System.Text.Json.Serialization;

namespace HorusStudio.Maui.MaterialDesignControls.Utils;

class JsonSerializationUtils
{
    private static JsonSerializationUtils? _instance;
    public static JsonSerializationUtils Instance => _instance ??= new JsonSerializationUtils();

    public JsonSerializerOptions? SerializerOptions { get; }

    private JsonSerializationUtils()
    {
        SerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        SerializerOptions.Converters.Add(new ColorToHexJsonConverter());
        SerializerOptions.Converters.Add(new ImageSourceConverter());
    }
}

class ColorToHexJsonConverter : JsonConverter<Color?>
{
    public override Color? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (!value?.StartsWith("#") ?? false) return null;

        return Color.FromArgb(reader.GetString());
    }

    public override void Write(
        Utf8JsonWriter writer,
        Color? hexColorValue,
        JsonSerializerOptions options) => 
        writer.WriteStringValue(hexColorValue?.ToArgbHex(true) ?? "null");
}

class ImageSourceConverter : JsonConverter<ImageSource?>
{
    public override ImageSource? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        //TODO: Complete this in case it's needed
        return null;
    }

    public override void Write(
        Utf8JsonWriter writer,
        ImageSource? sourceValue,
        JsonSerializerOptions options)
    {
        switch (sourceValue)
        {
            case null:
                writer.WriteNullValue();
                return;
            case UriImageSource uriImageSource:
                writer.WriteStringValue(uriImageSource.Uri.ToString());
                break;
            case FileImageSource fileImageSource:
                writer.WriteStringValue(fileImageSource.File);
                break;
            case StreamImageSource:
                writer.WriteStringValue("Stream");
                break;
            default:
                writer.WriteStringValue("Unexpected ImageSource");
                break;
        }
    }
    
}