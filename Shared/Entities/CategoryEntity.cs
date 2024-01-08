using MessagePack;

namespace Shared.Models
{
[MessagePackObject]
public partial class CategoryEntity
{
    [Key(0)]
    public int Id { get; set; } = -1;

    [Key(1)]
    public string Name { get; set; } = "";

    [Key(2)]
    public string? Description { get; set; } = "";

    [Key(3)]
    public int? Count { get; set; } = 0;

    [IgnoreMember]
    public virtual ICollection<AppEntity> Apps { get; set; } = new List<AppEntity>();

    public CategoryEntity()
    {
    }
}
}
