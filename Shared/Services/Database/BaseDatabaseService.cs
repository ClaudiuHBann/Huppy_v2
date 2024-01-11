using System.Net.Http.Json;

using Shared.Responses;
using Shared.Utilities;

namespace Shared.Services.Database
{
public abstract class BaseDatabaseService<TypeRequest, TypeResponse>
    where TypeResponse : class
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

    protected enum EDBAction
    {
        Create,
        Read,
        Update,
        Delete
    }

    protected enum EHTTPRequest
    {
        Get,
        Post
    }

    public virtual async Task<TypeResponse?> Create(TypeRequest request) => await RequestCRUD(request,
                                                                                              EDBAction.Create);
    public virtual async Task<TypeResponse?> Read(TypeRequest request) => await RequestCRUD(request, EDBAction.Read);
    public virtual async Task<TypeResponse?> Update(TypeRequest request) => await RequestCRUD(request,
                                                                                              EDBAction.Update);
    public virtual async Task<TypeResponse?> Delete(TypeRequest request) => await RequestCRUD(request,
                                                                                              EDBAction.Delete);

    protected async Task<TypeResponse?> RequestCRUD(TypeRequest request,
                                                    EDBAction action) => await Request(EHTTPRequest.Post,
                                                                                       action.ToString(), request);

    protected async Task<TypeResponse?> Request(EHTTPRequest requestHTTP, string action, object? value = null)
    {
        ClearLastError();

        var uri = $"{_URLBase}{GetControllerName()}/{action}";

        HttpResponseMessage ? response;
        switch (requestHTTP)
        {
        case EHTTPRequest.Get:
            response = await _client.GetAsync(uri);
            break;

        case EHTTPRequest.Post:
            response = await _client.PostAsJsonAsync(uri, value);
            break;

        default:
            return null;
        }

        return await ProcessResponse(response);
    }

    private async Task<TypeResponse?> ProcessResponse(HttpResponseMessage response)
    {
        ClearLastError();

        var bytes = await response.Content.ReadFromJsonAsync<byte[]>();
        if (bytes == null)
        {
            return null;
        }

        if (response.IsSuccessStatusCode)
        {
            return bytes.FromMsgPack<TypeResponse>();
        }
        else
        {
            var error = bytes.FromMsgPack<ErrorResponse>();
            SetLastError(error.Message);

            return null;
        }
    }
}
}
