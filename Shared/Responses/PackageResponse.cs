using Shared.Models;

namespace Shared.Responses
{
public class PackageResponse
{
    public int Id { get; set; } = -1;
    public int[] Apps { get; set; } = [];
    public string Name { get; set; } = "";
    public bool Updated { get; set; } = false;

    public PackageResponse()
    {
    }

    public PackageResponse(bool updated)
    {
        Updated = updated;
    }

    public PackageResponse(PackageEntity packageEntity)
    {
        Id = packageEntity.Id;
        Apps = packageEntity.Apps;
        Name = packageEntity.Name;
    }
}
}
