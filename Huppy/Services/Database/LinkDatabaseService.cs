using Shared.Requests;
using Shared.Responses;

namespace Huppy.Services.Database
{
public class LinkDatabaseService : BaseDatabaseService<LinkRequest, LinkResponse>
{
    protected override string GetControllerName() => "Link";
}
}
