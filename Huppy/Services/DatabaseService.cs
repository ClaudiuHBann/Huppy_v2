using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Shared.Models;
using Shared.Requests;
using Shared.Responses;
using Shared.Utilities;

namespace Huppy.Utilities
{
public class DatabaseService
{
    private readonly HttpClient _client = new();
    public string LastError { get; private set; } = "";

#if DEBUG
    private const string _URLBase = "https://localhost:7194/";
#else
    private const string _URLBase = "https://162.55.32.18:80/";
#endif

    private const string _URLCategory = _URLBase + "Category/";
    private const string _URLPackage = _URLBase + "Package/";

    public async Task<List<KeyValuePair<CategoryEntity, List<AppEntity>>>> GetCategoryToApps()
    {
        ClearLastError();

        var response = await _client.GetAsync(_URLCategory + nameof(GetCategoryToApps));
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode || result == null)
        {
            return [];
        }

        return result.FromJSON<List<KeyValuePair<CategoryEntity, List<AppEntity>>>>() ?? [];
    }

    public async Task<PackageResponse?> PackageCreate(PackageRequest packageRequest)
    {
        ClearLastError();

        var response = await _client.PostAsJsonAsync(_URLPackage + nameof(PackageCreate), packageRequest);
        var result = await response.Content.ReadAsStringAsync();
        if (result == null)
        {
            return null;
        }

        if (response.IsSuccessStatusCode)
        {
            return result.FromJSON<PackageResponse>();
        }
        else
        {
            LastError = result;
            return null;
        }
    }

    public async Task<bool?> PackageUpdate(PackageRequest packageRequest)
    {
        ClearLastError();

        var response = await _client.PostAsJsonAsync(_URLPackage + nameof(PackageUpdate), packageRequest);
        var result = await response.Content.ReadAsStringAsync();
        if (result == null)
        {
            return null;
        }

        if (response.IsSuccessStatusCode)
        {
            return bool.Parse(result);
        }
        else
        {
            LastError = result;
            return null;
        }
    }

    public async Task<PackageEntity?> PackageLoad(PackageRequest packageRequest)
    {
        ClearLastError();

        var response = await _client.PostAsJsonAsync(_URLPackage + nameof(PackageLoad), packageRequest);
        var result = await response.Content.ReadAsStringAsync();
        if (result == null)
        {
            return null;
        }

        if (response.IsSuccessStatusCode)
        {
            var packageResponse = result.FromJSON<PackageResponse>();
            return packageResponse == null ? null : new(packageResponse);
        }
        else
        {
            LastError = result;
            return null;
        }
    }

    private void ClearLastError() => LastError = "";
}
}
