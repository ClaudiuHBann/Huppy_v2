using Shared.Models;

namespace Shared.Responses
{
public class PackageResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public PackageResponse()
    {
    }

    public PackageResponse(PackageEntity packageEntity)
    {
        Id = packageEntity.Id;
        Name = packageEntity.Name;
    }
}
}
