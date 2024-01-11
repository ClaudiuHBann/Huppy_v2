using Shared.Requests;
using Shared.Responses;

namespace Shared.Services.Database
{
public class AppDatabaseService : BaseDatabaseService<AppRequest, AppResponse>
{
    protected override string GetControllerName() => "App";
}
}
