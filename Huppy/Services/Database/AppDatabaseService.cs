using Shared.Requests;
using Shared.Responses;

namespace Huppy.Services.Database
{
public class AppDatabaseService : BaseDatabaseService<AppRequest, AppResponse>
{
    protected override string GetControllerName() => "App";
}
}
