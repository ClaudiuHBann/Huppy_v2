using Shared.Models;

namespace Shared.Services.Database
{
public class DatabaseService : BaseService
{
    public PackageDatabaseService Packages { get; set; } = new();
    public CategoryDatabaseService Categories { get; set; } = new();
    public AppDatabaseService Apps { get; set; } = new();
    public LinkDatabaseService Links { get; set; } = new();

    public async Task<(AppEntity app, LinkEntity link)> AppCreate(AppEntity appEntity, LinkEntity linkEntity)
    {
        var appResponse = await Apps.Create(new(appEntity));
        var linkResponse = await Links.Create(new(linkEntity) { App = appResponse.Id });
        return new(new(appResponse), new(linkResponse));
    }
    public async Task<(AppEntity app, LinkEntity link)> AppUpdate(AppEntity appEntity, LinkEntity linkEntity)
    {
        var appResponse = await Apps.Update(new(appEntity));
        var linkResponse = await Links.Update(new(linkEntity) { App = appResponse.Id });
        return new(new(appResponse), new(linkResponse));
    }
}
}
