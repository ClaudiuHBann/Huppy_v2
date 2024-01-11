using MessagePack;

using Shared.Requests;
using Shared.Responses;

namespace Shared.Models
{
[MessagePackObject(true)]
public partial class PackageEntity
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid[] Apps { get; set; } = Array.Empty<Guid>();

    public string Name { get; set; } = "";

    public PackageEntity()
    {
    }

    public PackageEntity(PackageRequest packageRequest)
    {
        Id = packageRequest.Id;
        Apps = packageRequest.Apps ?? Array.Empty<Guid>();
        Name = packageRequest.Name;
    }

    public PackageEntity(PackageResponse packageResponse)
    {
        Id = packageResponse.Id;
        Apps = packageResponse.Apps ?? Array.Empty<Guid>();
        Name = packageResponse.Name;
    }
}
}
