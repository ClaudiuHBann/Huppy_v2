using MessagePack;
using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class LinkResponse
{
    public int Id { get; set; } = -1;
    public int App { get; set; } = -1;
    public bool Updated { get; set; } = false;
    public string Url { get; set; } = null!;

    public LinkResponse(LinkEntity entity, bool updated = false)
    {
        Id = entity.Id;
        App = entity.App;
        Url = entity.Url;
        Updated = updated;
    }

    public LinkResponse()
    {
    }
}
}
