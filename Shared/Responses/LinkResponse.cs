using MessagePack;
using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject]
public class LinkResponse
{
    [Key(0)]
    public int Id { get; set; } = -1;
    [Key(1)]
    public int App { get; set; } = -1;
    [Key(2)]
    public bool Updated { get; set; } = false;
    [Key(3)]
    public string Url { get; set; } = "";

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
