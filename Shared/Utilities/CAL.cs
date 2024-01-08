using MessagePack;

using Shared.Models;

namespace Shared.Utilities
{
[MessagePackObject(true)]
public class AL
{
    public AppEntity App { get; set; } = new();
    public List<LinkEntity> Link { get; set; } = new();

    public AL()
    {
    }

    public AL(AppEntity app, List<LinkEntity> links)
    {
        App = app;
        Link = links;
    }
}

[MessagePackObject(true)]
public class CAL
{
    public CategoryEntity Category { get; set; } = new();
    public List<AL> ALs { get; set; } = new();

    public CAL()
    {
    }

    public CAL(CategoryEntity category, List<AL> als)
    {
        Category = category;
        ALs = als;
    }
}
}
