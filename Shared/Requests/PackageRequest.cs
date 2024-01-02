using Shared.Models;

namespace Shared.Requests
{
public class PackageRequest
{
    public int Id { get; set; } = -1;
    public int[] Apps { get; set; } = [];
    public string Name { get; set; } = "";

    public PackageRequest()
    {
    }

    public PackageRequest(PackageEntity packageEntity)
    {
        Id = packageEntity.Id;
        Apps = packageEntity.Apps ?? [];
        Name = packageEntity.Name ?? "";
    }
}
}
