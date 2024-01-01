using System.Text.Json.Serialization;

namespace Shared.Models
{
public partial class LinkEntity
{
    public int Id { get; set; }

    public int App { get; set; }

    public string Url { get; set; } = null!;

    [JsonIgnore]
    public virtual AppEntity AppNavigation { get; set; } = null!;
}
}
