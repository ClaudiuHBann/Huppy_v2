using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Huppy.Models;
using Huppy.Services;
using Huppy.Services.Database;

using Shared.Models;

namespace Huppy.ViewModels
{
public class PackageViewModel : ViewModelBase
{
    public ObservableCollection<AppModel> Apps { get; set; } = [];
    public ObservableCollection<KeyValuePair<CategoryModel, AppViewModel>>? CategoryToApps { get; set; } = null;

    public static string PackageIDDefault { get; } = "0";
    public static string PackageNameDefault { get; } = "None";

    private readonly DatabaseService _database;
    private readonly NotificationService _notification;
    private readonly ClipboardService _clipboard;

    public PackageViewModel(DatabaseService database, NotificationService notification, ClipboardService clipboard)
    {
        _database = database;
        _notification = notification;
        _clipboard = clipboard;
    }

    public async void ClipboardSaveText(string? text) => await _clipboard.SetText(text);

    public async Task<PackageEntity?> PackageCreate(PackageEntity packageEntity)
    {
        var response = await _database.Packages.Create(new(packageEntity));
        if (response == null)
        {
            _notification.NotifyE(_database.Packages.LastError);
            return null;
        }

        return new(response);
    }

    public async Task<bool?> PackageUpdate(PackageEntity packageEntity)
    {
        var response = await _database.Packages.Update(new(packageEntity));
        if (response == null)
        {
            _notification.NotifyE(_database.Packages.LastError);
            return null;
        }

        return response.Updated;
    }

    public async Task<PackageEntity?> PackageLoad(PackageEntity packageEntity)
    {
        var response = await _database.Packages.Read(new(packageEntity));
        if (response == null)
        {
            _notification.NotifyE(_database.Packages.LastError);
            return null;
        }

        return new(response);
    }

    public void PackageLoad(int[] apps)
    {
        CategoryToApps?.SelectMany(pair => pair.Value.Apps)
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
