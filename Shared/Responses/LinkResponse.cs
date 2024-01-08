using MessagePack;

using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class LinkResponse : BaseResponse
{
    public int Id { get; set; } = -1;

    public int App { get; set; } = -1;

    public string Url { get; set; } = "";

    public LinkResponse(LinkEntity entity, bool updated = false) : base(updated)
    {
        Id = entity.Id;
        App = entity.App;
        Url = entity.Url;
    }

    public LinkResponse()
    {
    }
}
}
