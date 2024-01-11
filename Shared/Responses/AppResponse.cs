using MessagePack;

using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class AppResponse : BaseResponse
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid Category { get; set; } = Guid.Empty;

    public string Name { get; set; } = "";

    public bool Proposed { get; set; } = true;

    public byte[] Image { get; set; } = Array.Empty<byte>();

    public AppResponse()
    {
    }

    public AppResponse(AppEntity appEntity)
    {
        Id = appEntity.Id;
        Name = appEntity.Name;
        Image = appEntity.Image;
        Proposed = appEntity.Proposed;
        Category = appEntity.Category;
    }
}
}
