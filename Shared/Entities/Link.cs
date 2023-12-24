using Newtonsoft.Json;

namespace Shared.Models
{
public partial class Link
{
    public int Id { get; set; }

    public int App { get; set; }

    public string Url { get; set; } = null!;

    [JsonIgnore]
    public virtual App AppNavigation { get; set; } = null!;
}
}
