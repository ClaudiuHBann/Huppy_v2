using System.Text.Json;

namespace Shared.Utilities
{
public static class JSONExtension
{
    public static string ToJSON(this object data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(data, options);
    }

    public static T? FromJSON<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}
}
