using System.Net.Http;
using System.Threading.Tasks;

using Shared.Utilities;

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

    protected async Task<ResponseType?> Request<ResponseType>(RequestType requestType, string action,
                                                              object? value = null)
        where ResponseType : class
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
            response = await _client.PostAsync(uri, new ByteArrayContent(value!.ToMsgPack()));
            break;

        default:
            return null;
        }

        return await ProcessResponse<ResponseType>(response);
    }

    private async Task<ResponseType?> ProcessResponse<ResponseType>(HttpResponseMessage response)
        where ResponseType : class
    {
        ClearLastError();

        if (response.IsSuccessStatusCode)
        {
            return (await response.Content.ReadAsByteArrayAsync()).FromMsgPack<ResponseType>();
        }
        else
        {
            SetLastError(await response.Content.ReadAsStringAsync());
            return null;
        }
    }
}
}
