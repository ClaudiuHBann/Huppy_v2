using MessagePack;

using Shared.Models;

namespace Shared.Requests
{
[MessagePackObject(true)]
public class AppRequest : BaseRequest
{
    public int Id { get; set; } = -1;

    public int Category { get; set; } = -1;

    public string Name { get; set; } = "";

    public byte[] Image { get; set; } = Array.Empty<byte>();

    public AppRequest()
    {
    }

    public AppRequest(AppEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Image = entity.Image;
        Category = entity.Category;
    }
}
}
