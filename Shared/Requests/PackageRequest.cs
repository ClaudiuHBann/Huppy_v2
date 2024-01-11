using MessagePack;

using Shared.Models;

namespace Shared.Requests
{
[MessagePackObject(true)]
public class PackageRequest : BaseRequest
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid[] Apps { get; set; } = Array.Empty<Guid>();

    public string Name { get; set; } = "";

    public PackageRequest()
    {
    }

    public PackageRequest(PackageEntity packageEntity)
    {
        Id = packageEntity.Id;
        Name = packageEntity.Name ?? "";
        Apps = packageEntity.Apps ?? Array.Empty<Guid>();
    }
}
}
