using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Huppy.Models;
using Huppy.Services;
using Huppy.Services.Database;

using Shared.Models;

namespace Huppy.ViewModels
{
public class AppViewModel : ViewModelBase
{
    public ObservableCollection<AppModel> Apps { get; set; }

    private readonly SharedService _shared;
    private readonly DatabaseService _database;

    public AppViewModel(ObservableCollection<AppModel> apps, SharedService shared, DatabaseService database)
    {
        Apps = apps;
        _shared = shared;
        _database = database;
    }

    public void PackageAdd(AppModel app)
    {
        _shared.PackageViewModel?.Apps.Add(app);
    }

    public void PackageRemove(AppModel app)
    {
        _shared.PackageViewModel?.Apps.Remove(app);
    }

    public List<CategoryModel> GetCategoryModels()
    {
        return _shared.GetCategoryModels();
    }

    public void AppUpdateCTA(AppEntity appEntity, LinkEntity linkEntity)
    {
        _shared.CategoryViewModel?.AppUpdate(appEntity, linkEntity);
    }

    public async Task<(AppEntity app, LinkEntity link)?> AppUpdate(AppEntity appEntity, LinkEntity linkEntity)
    {
        return await _database.AppUpdate(appEntity, linkEntity);
    }
}
}
