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
                                                                                                 PackageAction.Create);

    public async Task<PackageResponse?> Update(PackageRequest packageRequest) => await PackageEx(packageRequest,
                                                                                                 PackageAction.Update);

    public async Task<PackageResponse?> Load(PackageRequest packageRequest) => await PackageEx(packageRequest,
                                                                                               PackageAction.Load);

    private enum PackageAction
    {
        Create,
        Update,
        Load
    }

    private async Task<PackageResponse?> PackageEx(PackageRequest packageRequest, PackageAction action) =>
        await Request<PackageResponse>(RequestType.Post, action.ToString(), packageRequest);
}
}
