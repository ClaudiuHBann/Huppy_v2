using Newtonsoft.Json;

namespace Huppy.Utilities
{
public static class JSONExtension
{
    public static string ToJSON(this object data)
    {
        return JsonConvert.SerializeObject(data, Formatting.None);
    }

    public static T? FromJSON<T>(this string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
}
