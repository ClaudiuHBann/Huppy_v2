using Shared.Requests;

using System.Text.Json.Serialization;

namespace Shared.Models
{
public partial class LinkEntity
{
    public int Id { get; set; } = -1;

    public int App { get; set; } = -1;

    public string Url { get; set; } = null!;

    [JsonIgnore]
    public virtual AppEntity AppNavigation { get; set; } = null!;

    public LinkEntity()
    {
    }

    public LinkEntity(LinkRequest request)
    {
        Id = request.Id;
        App = request.App;
        Url = request.Url;
    }
}
}
