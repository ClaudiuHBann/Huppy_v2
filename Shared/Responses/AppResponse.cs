using MessagePack;

using Shared.Models;

namespace Shared.Responses
{
[MessagePackObject]
public class AppResponse
{
    [Key(0)]
    public int Id { get; set; } = -1;

    [Key(1)]
    public int Category { get; set; } = -1;

    [Key(2)]
    public string Name { get; set; } = "";

    [Key(3)]
    public bool Proposed { get; set; } = true;

    [Key(4)]
    public byte[] Image { get; set; } = Array.Empty<byte>();

    [Key(5)]
    public bool Updated { get; set; } = false;

    public AppResponse()
    {
    }

    public AppResponse(AppEntity appEntity, bool updated = false)
    {
        Id = appEntity.Id;
        Category = appEntity.Category;
        Name = appEntity.Name;
        Proposed = appEntity.Proposed;
        Image = appEntity.Image;
        Updated = updated;
    }
}
}
