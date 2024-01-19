using Shared.Requests;
using Shared.Responses;

namespace Shared.Services.Database
{
public class CategoryDatabaseService : BaseDatabaseService<CategoryRequest, CategoryResponse>
{
    protected override string GetControllerName() => "Category";

    public async Task<CategoryResponse> CategoriesToAppsWithLinks() => await Request(EHTTPRequest.Get,
                                                                                     nameof(CategoriesToAppsWithLinks));
}
}
