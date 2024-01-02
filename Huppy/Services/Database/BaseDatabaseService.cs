using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Huppy.Services.Database
{
public abstract class BaseDatabaseService
{
    private readonly HttpClient _client = new();
    public string LastError { get; private set; } = "";

#if DEBUG
    private const string _URLBase = "https://localhost:7194/";
#else
    private const string _URLBase = "https://162.55.32.18:80/";
#endif

    protected void ClearLastError() => LastError = "";
    protected void SetLastError(string error) => LastError = error;

    protected abstract string GetControllerName();

    protected enum RequestType
    {
        Get,
        Post
    }

    protected async Task<string?> Request(RequestType requestType, string action, object? value = null)
    {
        ClearLastError();

        var uri = $"{_URLBase}{GetControllerName()}/{action}";

        HttpResponseMessage ? response;
        switch (requestType)
        {
        case RequestType.Get:
            response = await _client.GetAsync(uri);
            break;

        case RequestType.Post:
            response = await _client.PostAsJsonAsync(uri, value);
            break;

        default:
            return null;
        }

        return await ProcessResponse(response);
    }

    private async Task<string?> ProcessResponse(HttpResponseMessage response)
    {
        ClearLastError();

        var result = await response.Content.ReadAsStringAsync();
        if (result == null)
        {
            return null;
        }

        if (response.IsSuccessStatusCode)
        {
            return result;
        }
        else
        {
            SetLastError(result);
            return null;
        }
    }
}
}
