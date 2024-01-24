using MessagePack;

using Shared.Models;

namespace Shared.Requests
{
[MessagePackObject(true)]
public class AppRequest : BaseRequest
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid Category { get; set; } = Guid.Empty;

    public string Name { get; set; } = "";

    public byte[] Image { get; set; } = Array.Empty<byte>();

    public AppRequest()
    {
    }

    public AppRequest(AppRequest request)
    {
        Id = request.Id;
        Category = request.Category;
        Name = request.Name;
        Image = request.Image;
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
