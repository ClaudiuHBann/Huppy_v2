using MessagePack;
using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class PackageResponse
{
    public int Id { get; set; } = -1;
    public int[] Apps { get; set; } = Array.Empty<int>();
    public string Name { get; set; } = "";
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
