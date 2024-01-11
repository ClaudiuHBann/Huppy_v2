using Shared.Requests;
using Shared.Responses;

namespace Shared.Services.Database
{
public class PackageDatabaseService : BaseDatabaseService<PackageRequest, PackageResponse>
{
    protected override string GetControllerName() => "Package";
}
}
