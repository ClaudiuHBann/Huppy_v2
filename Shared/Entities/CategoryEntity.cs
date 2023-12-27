using Newtonsoft.Json;

namespace Shared.Models
{
public partial class CategoryEntity
{
    public int Id { get; set; } = -1;

    public string Name { get; set; } = null!;

    public string? Description { get; set; } = "";

    public int? Count { get; set; } = 0;

    [JsonIgnore]
    public virtual ICollection<AppEntity> Apps { get; set; } = new List<AppEntity>();

    public override int GetHashCode()
    {
        return Id;
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as CategoryEntity);
    }

    public bool Equals(CategoryEntity? category)
    {
        return category != null && category.Id == this.Id;
    }
}
}
