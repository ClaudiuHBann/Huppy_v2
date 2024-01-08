using System.Threading.Tasks;

using Shared.Responses;

namespace Huppy.Services.Database
{
public class CategoryDatabaseService : BaseDatabaseService
{
    protected override string GetControllerName() => "Category";

    public async Task<CategoryResponse?> GetCALs() => await Request<CategoryResponse>(RequestType.Get, nameof(GetCALs));
}
}
