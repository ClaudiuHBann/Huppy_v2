using MessagePack;

using Shared.Requests;
using Shared.Responses;

namespace Shared.Models
{
[MessagePackObject(true)]
public partial class LinkEntity
{
    public int Id { get; set; } = -1;

    public int App { get; set; } = -1;

    public string Url { get; set; } = "";

    [IgnoreMember]
    public virtual AppEntity AppNavigation { get; set; } = null!;

    public LinkEntity()
    {
    }

    public LinkEntity(LinkRequest request)
    {
        Id = request.Id;
        App = request.App;
        Url = request.Url;
    }

    public LinkEntity(LinkResponse response)
    {
        Id = response.Id;
        App = response.App;
        Url = response.Url;
    }
}
}
