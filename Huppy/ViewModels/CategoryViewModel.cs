using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Huppy.Models;
using Huppy.Services;
using Huppy.Services.Database;

using Avalonia.Threading;

using Shared.Entities;
using Shared.Models;

namespace Huppy.ViewModels
{
public partial class CategoryViewModel : ViewModelBase
{
    // TODO: can we make this a map?
    public ObservableCollection<KeyValuePair<CategoryModel, AppViewModel>> CategoryToApps { get; set; } = [];

    private readonly DatabaseService _database;
    private readonly SharedService _shared;
    private readonly SettingsService _settings;
    private readonly NotificationService _notification;

    public SettingsEntity Settings => _settings.Settings;

    public CategoryViewModel(DatabaseService database, SharedService shared, SettingsService settings,
                             NotificationService notification)
    {
        _database = database;
        _shared = shared;
        _settings = settings;
        _notification = notification;

        Populate();
    }

    public void AppAdd(AppEntity app, LinkEntity link)
    {
        CategoryToApps.First(pair => pair.Key.Category.Id == app.Category).Value.Apps.Add(new(app, link));
    }

    public void AppUpdate(AppEntity app, LinkEntity link)
    {
        var apps = CategoryToApps.First(pair => pair.Key.Category.Id == app.Category).Value.Apps;
        var appModel = apps.First(appModel => appModel.App.Id == app.Id);
        apps[apps.IndexOf(appModel)] = new(app, link);
    }

    public async void Populate()
    {
        var response = await _database.Categories.GetCALs();
        if (response == null)
        {
            return;
        }

        foreach (var cal in response.CALs)
        {
            ObservableCollection<AppModel> collection = [];

            // order by name first and next by proposed because the proposed apps are at the end
            cal.ALs.OrderBy(al => al.App.Name)
                .OrderBy(al => al.App.Proposed)
                .Select(al =>
                        {
                            string url = AppModel.UrlDefault;
                            var link = al.Link.FirstOrDefault();
                            if (link != null)
                            {
                                url = link.Url;
                            }

                            var appModel = new AppModel(al.App, al.Link.FirstOrDefault());
                            appModel.Update(_settings.Settings);
                            return appModel;
                        })
                .ToList()
                .ForEach(collection.Add);

            await Dispatcher.UIThread.InvokeAsync(
                () => CategoryToApps.Add(new(new(cal.Category), new(collection, _shared, _database))));
        }
    }

    public void ShowProposedApps(bool show)
    {
        _settings.Settings.ShowProposedApps = show;
        // update the apps
        CategoryToApps.SelectMany(pair => pair.Value.Apps).ToList().ForEach(app => app.Update(_settings.Settings));
    }

    public async Task<(AppEntity app, LinkEntity link)?> AppCreate(AppEntity appEntity, LinkEntity linkEntity)
    {
        return await _database.AppCreate(appEntity, linkEntity);
    }
}
}
