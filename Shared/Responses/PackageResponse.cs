using MessagePack;

using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class PackageResponse : BaseResponse
{
    public int Id { get; set; } = -1;

    public int[] Apps { get; set; } = Array.Empty<int>();

    public string Name { get; set; } = "";

    public PackageResponse()
    {
    }

    public PackageResponse(PackageEntity packageEntity, bool updated = false) : base(updated)
    {
        Id = packageEntity.Id;
        Name = packageEntity.Name;
        Apps = packageEntity.Apps;
    }
}
}
