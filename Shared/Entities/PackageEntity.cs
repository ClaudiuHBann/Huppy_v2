using Shared.Responses;

namespace Shared.Models
{
public partial class PackageEntity
{
    public int Id { get; set; } = -1;

    public int[] Apps { get; set; } = null!;

    public string Name { get; set; } = null!;

    public PackageEntity()
    {
    }

    public PackageEntity(PackageResponse packageResponse)
    {
        Id = packageResponse.Id;
        Name = packageResponse.Name;
    }
}
}
