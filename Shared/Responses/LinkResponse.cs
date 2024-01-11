using MessagePack;

using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class LinkResponse : BaseResponse
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid App { get; set; } = Guid.Empty;

    public string Url { get; set; } = "";

    public LinkResponse(LinkEntity entity)
    {
        Id = entity.Id;
        Url = entity.Url;
        App = entity.App;
    }

    public LinkResponse()
    {
    }
}
}
