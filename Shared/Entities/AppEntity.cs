using Shared.Requests;

using System.Text.Json.Serialization;

namespace Shared.Models
{
public partial class AppEntity
{
    public int Id { get; set; } = -1;

    public int Category { get; set; } = -1;

    public string Name { get; set; } = null!;

    public bool Proposed { get; set; } = true;

    public byte[] ImageRaw { get; set; } = null!;

    [JsonIgnore]
    public virtual CategoryEntity CategoryNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<LinkEntity> Links { get; set; } = new List<LinkEntity>();

    public AppEntity()
    {
    }

    public AppEntity(AppRequest appRequest)
    {
        Id = appRequest.Id;
        Category = appRequest.Category;
        Name = appRequest.Name;
        Proposed = appRequest.Proposed;
        ImageRaw = appRequest.ImageRaw;
    }
}
}
