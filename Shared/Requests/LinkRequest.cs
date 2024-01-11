using MessagePack;

using Shared.Models;

namespace Shared.Requests
{
[MessagePackObject(true)]
public class LinkRequest : BaseRequest
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid App { get; set; } = Guid.Empty;

    public string Url { get; set; } = "";

    public LinkRequest()
    {
    }

    public LinkRequest(LinkEntity entity)
    {
        Id = entity.Id;
        Url = entity.Url;
        App = entity.App;
    }
}
}
