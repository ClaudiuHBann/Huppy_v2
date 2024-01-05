using System.Threading.Tasks;

using Shared.Requests;
using Shared.Responses;
using Shared.Utilities;

namespace Huppy.Services.Database
{
public class AppDatabaseService : BaseDatabaseService
{
    protected override string GetControllerName() => "App";

    public async Task<AppResponse?> Create(AppRequest appRequest) => await PackageEx(appRequest, PackageAction.Create);

    private enum PackageAction
    {
        Create,
    }

    private async Task<AppResponse?> PackageEx(AppRequest appRequest, PackageAction action)
    {
        var response = await Request(RequestType.Post, action.ToString(), appRequest);
        return response?.FromJSON<AppResponse>();
    }
}
}
