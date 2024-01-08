using System.Threading.Tasks;

using Shared.Requests;
using Shared.Responses;

namespace Huppy.Services.Database
{
public class AppDatabaseService : BaseDatabaseService
{
    protected override string GetControllerName() => "App";

    public async Task<AppResponse?> Create(AppRequest request) => await AppEx(request, Action.Create);
    public async Task<AppResponse?> Update(AppRequest request) => await AppEx(request, Action.Update);
    public async Task<AppResponse?> Load(AppRequest request) => await AppEx(request, Action.Load);

    private async Task<AppResponse?> AppEx(AppRequest request,
                                           Action action) => await Request<AppResponse>(RequestType.Post,
                                                                                        action.ToString(), request);
}
}
