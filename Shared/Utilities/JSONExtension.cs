using Newtonsoft.Json;

namespace Shared.Utilities
{
public static class JSONExtension
{
    public static string ToJSON(this object data)
    {
        return JsonConvert.SerializeObject(data, Formatting.Indented);
    }

    public static T? FromJSON<T>(this string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
}
