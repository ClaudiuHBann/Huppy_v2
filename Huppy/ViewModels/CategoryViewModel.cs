using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Huppy.Models;
using Huppy.Services;
using Huppy.Services.Database;

using Avalonia.Threading;

using Shared.Models;
using Shared.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Huppy.ViewModels
{
public partial class CategoryViewModel : ViewModelBase
{
    // TODO: can we make this a map?
    public ObservableCollection<KeyValuePair<CategoryModel, AppViewModel>> CategoryToApps { get; set; } = [];
    public ObservableCollection<AppModel>? Apps { get; set; } = null;

    private readonly DatabaseService _database;
    private readonly SettingsService _settings;
    private readonly NotificationService _notification;

    public SettingsEntity Settings => _settings.Settings;

    public CategoryViewModel(DatabaseService database, SettingsService settings,
                             NotificationService notificationService)
    {
        _database = database;
        _settings = settings;
        _notification = notificationService;

        Populate();
    }

    public async void Populate()
    {
        foreach (var pair in await _database.Category.GetCategoryToApps())
        {
            ObservableCollection<AppModel> collection = [];

            // order by name first and next by proposed because the proposed apps are at the end
            pair.Value.OrderBy(app => app.Name)
                .OrderBy(app => app.Proposed)
                .Select(app =>
                        {
                            var appModel = new AppModel(app);
                            appModel.Update(_settings.Settings);
                            return appModel;
                        })
                .ToList()
                .ForEach(collection.Add);

            await Dispatcher.UIThread.InvokeAsync(() => CategoryToApps.Add(new(new(pair.Key), new(collection, Apps))));
        }
    }

    public void ShowProposedApps(bool show)
    {
        _settings.Settings.ShowProposedApps = show;
        // update the apps
        CategoryToApps.SelectMany(pair => pair.Value.Apps).ToList().ForEach(app => app.Update(_settings.Settings));
    }

    public async Task<AppEntity?> AppCreate(AppRequest appRequest)
    {
        var response = await _database.App.Create(appRequest);
        if (response == null)
        {
            _notification.NotifyE(_database.Package.LastError);
            return null;
        }

        return new(response);
    }
}
}
