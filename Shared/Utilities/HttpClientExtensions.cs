using System.Net.Http.Json;

namespace Shared.Utilities
{
public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> GetAsJsonAsync<TValue>(this HttpClient httpClient, string requestUri,
                                                                         TValue value)
    {
        var request = new HttpRequestMessage { Content = JsonContent.Create(value), Method = HttpMethod.Get,
                                               RequestUri = new Uri(requestUri, UriKind.Relative) };
        return await httpClient.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> DeleteAsJsonAsync<TValue>(this HttpClient httpClient,
                                                                            string requestUri, TValue value)
    {
        var request = new HttpRequestMessage { Content = JsonContent.Create(value), Method = HttpMethod.Delete,
                                               RequestUri = new Uri(requestUri, UriKind.Relative) };
        return await httpClient.SendAsync(request);
    }
}
}
