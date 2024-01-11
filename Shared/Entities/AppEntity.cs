using MessagePack;

using Shared.Requests;
using Shared.Responses;

namespace Shared.Models
{
[MessagePackObject(true)]
public partial class AppEntity
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid Category { get; set; } = Guid.Empty;

    public string Name { get; set; } = "";

    public bool Proposed { get; set; } = true;

    public byte[] Image { get; set; } = Array.Empty<byte>();

    [IgnoreMember]
    public virtual CategoryEntity CategoryNavigation { get; set; } = null!;

    [IgnoreMember]
    public virtual ICollection<LinkEntity> Links { get; set; } = null!;

    public AppEntity()
    {
    }

    public AppEntity(AppRequest appRequest)
    {
        Id = appRequest.Id;
        Name = appRequest.Name;
        Image = appRequest.Image;
        Category = appRequest.Category;
    }

    public AppEntity(AppResponse appResponse)
    {
        Id = appResponse.Id;
        Name = appResponse.Name;
        Image = appResponse.Image;
        Proposed = appResponse.Proposed;
        Category = appResponse.Category;
    }
}
}
