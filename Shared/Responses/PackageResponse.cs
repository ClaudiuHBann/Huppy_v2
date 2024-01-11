using MessagePack;

using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class PackageResponse : BaseResponse
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid[] Apps { get; set; } = Array.Empty<Guid>();

    public string Name { get; set; } = "";

    public PackageResponse()
    {
    }

    public PackageResponse(PackageEntity packageEntity)
    {
        Id = packageEntity.Id;
        Name = packageEntity.Name;
        Apps = packageEntity.Apps;
    }
}
}
