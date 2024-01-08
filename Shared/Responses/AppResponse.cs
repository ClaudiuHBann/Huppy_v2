using MessagePack;

using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class AppResponse : BaseResponse
{
    public int Id { get; set; } = -1;

    public int Category { get; set; } = -1;

    public string Name { get; set; } = "";

    public bool Proposed { get; set; } = true;

    public byte[] Image { get; set; } = Array.Empty<byte>();

    public AppResponse()
    {
    }

    public AppResponse(AppEntity appEntity, bool updated = false) : base(updated)
    {
        Id = appEntity.Id;
        Category = appEntity.Category;
        Name = appEntity.Name;
        Proposed = appEntity.Proposed;
        Image = appEntity.Image;
    }
}
}
