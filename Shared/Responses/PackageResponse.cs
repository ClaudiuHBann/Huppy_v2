using MessagePack;
using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject]
public class PackageResponse
{
    [Key(0)]
    public int Id { get; set; } = -1;
    [Key(1)]
    public int[] Apps { get; set; } = Array.Empty<int>();
    [Key(2)]
    public string Name { get; set; } = "";
    [Key(3)]
    public bool Updated { get; set; } = false;

    public PackageResponse()
    {
    }

    public PackageResponse(PackageEntity packageEntity, bool updated = false)
    {
        Id = packageEntity.Id;
        Apps = packageEntity.Apps;
        Name = packageEntity.Name;
        Updated = updated;
    }
}
}
