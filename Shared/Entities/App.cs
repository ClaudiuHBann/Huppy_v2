using Newtonsoft.Json;

namespace Shared.Models
{
public partial class App
{
    public int Id { get; set; }

    public int Category { get; set; }

    public string Name { get; set; } = null!;

    public bool Proposed { get; set; }

    public string Image { get; set; } = null!;

    [JsonIgnore]
    public virtual Category CategoryNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Link> Links { get; set; } = new List<Link>();
}
}
