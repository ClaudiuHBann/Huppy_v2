using Shared.Requests;
using Shared.Responses;

namespace Huppy.Services.Database
{
public class PackageDatabaseService : BaseDatabaseService<PackageRequest, PackageResponse>
{
    protected override string GetControllerName() => "Package";
}
}
