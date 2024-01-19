using System.Net.Http.Json;

using Shared.Responses;
using Shared.Utilities;
using Shared.Exceptions;

namespace Shared.Services.Database
{
public abstract class BaseDatabaseService<TypeRequest, TypeResponse>
    where TypeResponse : class
{
    private readonly HttpClient _client = new();

#if DEBUG
    private const string _URLBase = "https://localhost:7194/";
#else
    private const string _URLBase = "https://162.55.32.18:80/";
#endif

    protected abstract string GetControllerName();

    protected enum EDBAction
    {
        Create,
        Read,
        Update,
        Delete
    }

    protected enum EHTTPRequest
    {
        Post,
        Get,
        Put,
        Delete
    }

    public virtual async Task<TypeResponse> Create(TypeRequest request) => await Request(EHTTPRequest.Post,
                                                                                         EDBAction.Create.ToString(),
                                                                                         request);
    public virtual async Task<TypeResponse> Read(TypeRequest request) => await Request(EHTTPRequest.Get,
                                                                                       EDBAction.Read.ToString(),
                                                                                       request);
    public virtual async Task<TypeResponse> Update(TypeRequest request) => await Request(EHTTPRequest.Put,
                                                                                         EDBAction.Update.ToString(),
                                                                                         request);
    public virtual async Task<TypeResponse> Delete(TypeRequest request) => await Request(EHTTPRequest.Delete,
                                                                                         EDBAction.Delete.ToString(),
                                                                                         request);

    protected async Task<TypeResponse> Request(EHTTPRequest requestHTTP, string action, object? value = null)
    {
        var uri = $"{_URLBase}{GetControllerName()}/{action}";

        HttpResponseMessage? response = requestHTTP switch {
            EHTTPRequest.Post => await _client.PostAsJsonAsync(uri, value),
            EHTTPRequest.Get => await _client.GetAsJsonAsync(uri, value),
            EHTTPRequest.Put => await _client.PutAsJsonAsync(uri, value),
            EHTTPRequest.Delete => await _client.DeleteAsJsonAsync(uri, value),
            _ => throw new ArgumentException($"The EHTTPRequest type '{requestHTTP}' is not allowed!"),
        };

        return await ProcessResponse(response);
    }

    private static async Task<TypeResponse> ProcessResponse(HttpResponseMessage response)
    {
        var bytes = await response.Content.ReadFromJsonAsync<byte[]>() ??
                    throw new ArgumentException("Could not ReadFromJsonAsync the response content!");
        if (response.IsSuccessStatusCode)
        {
            return bytes.FromMsgPack<TypeResponse>();
        }
        else
        {
            throw new DatabaseException(bytes.FromMsgPack<ErrorResponse>());
        }
    }
}
}
