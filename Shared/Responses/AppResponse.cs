using Shared.Models;

namespace Shared.Responses
{
public class AppResponse
{
    public int Id { get; set; } = -1;

    public int Category { get; set; } = -1;

    public string Name { get; set; } = null!;

    public bool Proposed { get; set; } = true;

    public byte[] ImageRaw { get; set; } = null!;

    public AppResponse()
    {
    }

    public AppResponse(AppEntity appEntity)
    {
        Id = appEntity.Id;
        Category = appEntity.Category;
        Name = appEntity.Name;
        Proposed = appEntity.Proposed;
        ImageRaw = appEntity.ImageRaw;
    }
}
}
