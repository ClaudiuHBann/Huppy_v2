using System.Threading.Tasks;

using Shared.Requests;
using Shared.Responses;
using Shared.Utilities;

namespace Huppy.Services.Database
{
public class PackageDatabaseService : BaseDatabaseService
{
    protected override string GetControllerName() => "Package";

    public async Task<PackageResponse?> Create(PackageRequest packageRequest) => await PackageEx(packageRequest,
                                                                                                 Action.Create);

    public async Task<PackageResponse?> Update(PackageRequest packageRequest) => await PackageEx(packageRequest,
                                                                                                 Action.Update);

    public async Task<PackageResponse?> Load(PackageRequest packageRequest) => await PackageEx(packageRequest,
                                                                                               Action.Load);

    private async Task<PackageResponse?> PackageEx(PackageRequest packageRequest, Action action) =>
        await Request<PackageResponse>(RequestType.Post, action.ToString(), packageRequest);
}
}
