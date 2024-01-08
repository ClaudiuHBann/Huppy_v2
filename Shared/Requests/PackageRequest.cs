using MessagePack;

using Shared.Models;

namespace Shared.Requests
{
[MessagePackObject]
public class PackageRequest
{
    [Key(0)]
    public int Id { get; set; } = -1;
    [Key(1)]
    public int[] Apps { get; set; } = Array.Empty<int>();
    [Key(2)]
    public string Name { get; set; } = "";

    public PackageRequest()
    {
    }

    public PackageRequest(PackageEntity packageEntity)
    {
        Id = packageEntity.Id;
        Apps = packageEntity.Apps ?? Array.Empty<int>();
        Name = packageEntity.Name ?? "";
    }
}
}
