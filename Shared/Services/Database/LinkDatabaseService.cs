using Shared.Requests;
using Shared.Responses;

namespace Shared.Services.Database
{
public class LinkDatabaseService : BaseDatabaseService<LinkRequest, LinkResponse>
{
    protected override string GetControllerName() => "Link";
}
}
