using System.Threading.Tasks;
using System.Collections.Generic;

using Shared.Models;
using Shared.Utilities;

namespace Huppy.Services.Database
{
public class CategoryDatabaseService : BaseDatabaseService
{
    protected override string GetControllerName() => "Category";

    public async Task<List<KeyValuePair<CategoryEntity, List<AppEntity>>>> GetCategoryToApps()
    {
        var response = await Request(RequestType.Get, nameof(GetCategoryToApps));
        return response != null ? response.FromJSON<List<KeyValuePair<CategoryEntity, List<AppEntity>>>>()! : [];
    }
}
}
