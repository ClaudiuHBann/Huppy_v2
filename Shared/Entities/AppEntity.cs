using MessagePack;

using Shared.Requests;
using Shared.Responses;
using Shared.Utilities;

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

    public AppEntity(AppEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Image = entity.Image;
        Proposed = entity.Proposed;
        Category = entity.Category;
    }

    public AppEntity(AppRequest request)
    {
        Id = request.Id;
        Name = request.Name;
        Image = request.Image;
        Category = request.Category;
    }

    public AppEntity(AppResponse response)
    {
        Id = response.Id;
        Name = response.Name;
        Image = response.Image;
        Proposed = response.Proposed;
        Category = response.Category;
    }

    public override string ToString()
    {
        return this.ToJSON(true);
    }
}
}
