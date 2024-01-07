using MessagePack;

using Shared.Requests;

namespace Shared.Models
{
[MessagePackObject(true)]
public partial class LinkEntity
{
    public int Id { get; set; } = -1;

    public int App { get; set; } = -1;

    public string Url { get; set; } = null!;

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
}
}
