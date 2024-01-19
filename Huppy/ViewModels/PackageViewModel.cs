using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Huppy.Models;
using Huppy.Services;

using Shared.Models;
using Shared.Services.Database;

namespace Huppy.ViewModels
{
public class PackageViewModel : ViewModelBase
{
    public static string PackageIDDefault { get; } = "0";
    public static string PackageNameDefault { get; } = "None";

    public ObservableCollection<AppModel> Apps { get; set; } = [];

    public readonly NotificationService Notification;
    private readonly DatabaseService _database;
    private readonly SharedService _shared;
    private readonly ClipboardService _clipboard;

    public PackageViewModel(DatabaseService database, SharedService shared, NotificationService notification,
                            ClipboardService clipboard)
    {
        Notification = notification;
        _database = database;
        _shared = shared;
        _clipboard = clipboard;
    }

    public async void ClipboardSaveText(string? text) => await _clipboard.SetText(text);

    public async Task<PackageEntity> PackageCreate(PackageEntity packageEntity) =>
        new(await _database.Packages.Create(new(packageEntity)));

    public async Task<bool> PackageUpdate(PackageEntity packageEntity) =>
        (await _database.Packages.Update(new(packageEntity))).Updated;

    public async Task<PackageEntity> PackageLoad(PackageEntity packageEntity) =>
        new(await _database.Packages.Read(new(packageEntity)));

    public void PackageLoad(Guid[] apps)
    {
        _shared.CategoryViewModel?.CategoryToApps.SelectMany(pair => pair.Value.Apps)
            .ToList()
            .ForEach(appView =>
                     {
                         // update the real app is checked state
                         appView.IsChecked = apps.Contains(appView.App.Id);
                         if (appView.IsChecked)
                         {
                             Apps.Add(appView);
                         }
                     });
    }

    public void PackageClear()
    {
        // update the real app is checked state
        Apps.ToList().ForEach(appView => appView.IsChecked = false);
        Apps.Clear();
    }
}
}
