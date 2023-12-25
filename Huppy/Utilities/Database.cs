using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Shared.Models;
using Shared.Utilities;

namespace Huppy.Utilities
{
public class Database
{
    private readonly HttpClient _client = new();
#if DEBUG
    private const string _URLBase = "https://localhost:7194/";
#else
    private const string _URLBase = "https://162.55.32.18:80/";
#endif
    private const string _URLPackage = _URLBase + "Package/";

    public async Task<List<KeyValuePair<Category, List<Shared.Models.App>>>> GetCategoryToApps()
    {
        var response = await _client.GetAsync(_URLPackage + nameof(GetCategoryToApps));
        var result = await response.Content.ReadAsStringAsync();
        if (result == null)
        {
            return [];
        }

        return result.FromJSON<List<KeyValuePair<Category, List<Shared.Models.App>>>>() ?? [];
    }
}
}
