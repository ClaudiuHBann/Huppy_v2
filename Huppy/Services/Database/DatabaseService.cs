using Shared.Models;

using System.Threading.Tasks;

namespace Huppy.Services.Database
{
public class DatabaseService
(NotificationService notification) : BaseService
{
    public PackageDatabaseService Packages { get; set; } = new();
    public CategoryDatabaseService Categories { get; set; } = new();
    public AppDatabaseService Apps { get; set; } = new();
    public LinkDatabaseService Links { get; set; } = new();

    public async Task < (AppEntity app, LinkEntity link) ? > AppCreate(AppEntity appEntity, LinkEntity linkEntity)
    {
        var appResponse = await Apps.Create(new(appEntity));
        if (appResponse == null)
        {
            notification.NotifyE(Apps.LastError);
            return null;
        }

        var linkResponse = await Links.Create(new(linkEntity) { App = appResponse.Id });
        if (linkResponse == null)
        {
            notification.NotifyE(Links.LastError);
            return null;
        }

        return new(new(appResponse), new(linkResponse));
    }
    public async Task < (AppEntity app, LinkEntity link) ? > AppUpdate(AppEntity appEntity, LinkEntity linkEntity)
    {
        var appResponse = await Apps.Update(new(appEntity));
        if (appResponse == null)
        {
            notification.NotifyE(Apps.LastError);
            return null;
        }

        var linkResponse = await Links.Update(new(linkEntity) { App = appResponse.Id });
        if (linkResponse == null)
        {
            notification.NotifyE(Links.LastError);
            return null;
        }

        return new(new(appResponse), new(linkResponse));
    }
}
}
