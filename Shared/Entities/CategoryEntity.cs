using MessagePack;

namespace Shared.Models
{
[MessagePackObject(true)]
public partial class CategoryEntity
{
    public int Id { get; set; } = -1;

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
