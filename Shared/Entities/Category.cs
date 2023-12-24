using Newtonsoft.Json;

namespace Shared.Models
{
public partial class Category
{
    public int Id { get; set; } = -1;

    public string Name { get; set; } = null!;

    public string? Description { get; set; } = "";

    public int? Count { get; set; } = 0;

    [JsonIgnore]
    public virtual ICollection<App> Apps { get; set; } = new List<App>();

    public override int GetHashCode()
    {
        return Id;
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as Category);
    }

    public bool Equals(Category? category)
    {
        return category != null && category.Id == this.Id;
    }
}
}
