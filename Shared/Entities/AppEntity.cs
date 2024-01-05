using System.Text.Json.Serialization;

using Shared.Requests;
using Shared.Responses;

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
        Category = appRequest.Category;
        Name = appRequest.Name;
        ImageRaw = appRequest.ImageRaw;
    }

    public AppEntity(AppResponse appResponse)
    {
        Id = appResponse.Id;
        Category = appResponse.Category;
        Name = appResponse.Name;
        Proposed = appResponse.Proposed;
        ImageRaw = appResponse.ImageRaw;
    }
}
}
