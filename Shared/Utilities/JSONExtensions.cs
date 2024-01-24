using System.Text.Json;

namespace Shared.Utilities
{
public static class JSONExtension
{
    public static string ToJSON(this object data, bool indented = false) =>
        JsonSerializer.Serialize(data, new JsonSerializerOptions() { WriteIndented = indented });

    public static Type FromJSON<Type>(this string json) => JsonSerializer.Deserialize<Type>(json);
}
}
