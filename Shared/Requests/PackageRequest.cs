using Shared.Models;

namespace Shared.Requests
{
public class PackageRequest
{
    public int Id { get; set; }
    public int[]? Apps { get; set; } = null;
    public string? Name { get; set; } = null;

    public PackageRequest()
    {
    }

    public PackageRequest(PackageEntity packageEntity)
    {
        Id = packageEntity.Id;
        Apps = packageEntity.Apps;
        Name = packageEntity.Name;
    }
}
}
