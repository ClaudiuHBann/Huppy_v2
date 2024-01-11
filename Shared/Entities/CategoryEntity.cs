using MessagePack;

namespace Shared.Models
{
[MessagePackObject(true)]
public partial class CategoryEntity
{
    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; set; } = "";

    public string? Description { get; set; } = "";

    public int? Count { get; set; } = 0;

    [IgnoreMember]
    public virtual ICollection<AppEntity> Apps { get; set; } = null!;

    public CategoryEntity()
    {
    }
}
}
