using MessagePack;

using Shared.Requests;
using Shared.Responses;

namespace Shared.Models
{
[MessagePackObject]
public partial class PackageEntity
{
    [Key(0)]
    public int Id { get; set; } = -1;

    [Key(1)]
    public int[] Apps { get; set; } = Array.Empty<int>();

    [Key(2)]
    public string Name { get; set; } = "";

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
