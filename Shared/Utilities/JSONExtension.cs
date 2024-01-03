using System.Text.Json;

namespace Shared.Utilities
{
public static class JSONExtension
{
    private static readonly JsonSerializerOptions options = new() { WriteIndented = true };

    public static string ToJSON(this object data) => JsonSerializer.Serialize(data, options);
    public static Type? FromJSON<Type>(this string json) => JsonSerializer.Deserialize<Type>(json, options);
}
}
