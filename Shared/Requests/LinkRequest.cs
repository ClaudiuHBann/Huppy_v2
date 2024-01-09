using MessagePack;

using Shared.Models;

namespace Shared.Requests
{
[MessagePackObject(true)]
public class LinkRequest : BaseRequest
{
    public int Id { get; set; } = -1;

    public int App { get; set; } = -1;

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
