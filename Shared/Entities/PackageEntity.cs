using MessagePack;

using Shared.Requests;
using Shared.Responses;

namespace Shared.Models
{
[MessagePackObject(true)]
public partial class PackageEntity
{
    public int Id { get; set; } = -1;

    public int[] Apps { get; set; } = null!;

    public string Name { get; set; } = null!;

    public PackageEntity()
    {
    }

    public PackageEntity(PackageRequest packageRequest)
    {
        Id = packageRequest.Id;
        Apps = packageRequest.Apps ?? Array.Empty<int>();
        Name = packageRequest.Name;
    }

    public PackageEntity(PackageResponse packageResponse)
    {
        Id = packageResponse.Id;
        Apps = packageResponse.Apps ?? Array.Empty<int>();
        Name = packageResponse.Name;
    }
}
}
