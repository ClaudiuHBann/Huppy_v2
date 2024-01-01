using System.Text.Json.Serialization;

namespace Shared.Models
{
public partial class AppEntity
{
    public int Id { get; set; }

    public int Category { get; set; }

    public string Name { get; set; } = null!;

    public bool Proposed { get; set; }

    public string Image { get; set; } = null!;

    [JsonIgnore]
    public virtual CategoryEntity CategoryNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<LinkEntity> Links { get; set; } = new List<LinkEntity>();
}
}
