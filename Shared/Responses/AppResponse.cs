using Shared.Models;

namespace Shared.Responses
{
public class AppResponse
{
    public int Id { get; set; } = -1;

    public int Category { get; set; } = -1;

    public string Name { get; set; } = null!;

    public bool Proposed { get; set; } = true;

    public byte[] Image { get; set; } = null!;

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
