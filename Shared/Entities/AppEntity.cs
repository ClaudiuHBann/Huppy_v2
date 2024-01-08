using Shared.Requests;
using Shared.Responses;

using MessagePack;

namespace Shared.Models
{
[MessagePackObject]
public partial class AppEntity
{
    [Key(0)]
    public int Id { get; set; } = -1;

    [Key(1)]
    public int Category { get; set; } = -1;

    [Key(2)]
    public string Name { get; set; } = "";

    [Key(3)]
    public bool Proposed { get; set; } = true;

    [Key(4)]
    public byte[] Image { get; set; } = Array.Empty<byte>();

    [IgnoreMember]
    public virtual CategoryEntity CategoryNavigation { get; set; } = new();

    [IgnoreMember]
    public virtual ICollection<LinkEntity> Links { get; set; } = new List<LinkEntity>();

    public AppEntity()
    {
    }

    public AppEntity(AppRequest appRequest)
    {
        Id = appRequest.Id;
        Category = appRequest.Category;
        Name = appRequest.Name;
        Image = appRequest.Image;
    }

    public AppEntity(AppResponse appResponse)
    {
        Id = appResponse.Id;
        Category = appResponse.Category;
        Name = appResponse.Name;
        Proposed = appResponse.Proposed;
        Image = appResponse.Image;
    }
}
}
