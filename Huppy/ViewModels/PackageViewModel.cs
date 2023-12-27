using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Huppy.Models;
using Huppy.Utilities;

using Shared.Models;

namespace Huppy.ViewModels
{
public class PackageViewModel : ViewModelBase
{
    public ObservableCollection<AppModel> Apps { get; set; } = [];

    public static string PackageIDDefault { get; } = "0";
    public static string PackageNameDefault { get; } = "None";

    private readonly DatabaseService _database;
    private readonly NotificationService _notification;

    public PackageViewModel(DatabaseService database, NotificationService notification)
    {
        _database = database;
        _notification = notification;
    }

    public async Task<PackageEntity?> PackageCreate(PackageEntity packageEntity)
    {
        var response = await _database.PackageCreate(new(packageEntity));
        if (response == null)
        {
            _notification.NotifyE(_database.LastError);
            return null;
        }

        return new(response);
    }

    public async Task<bool?> PackageUpdate(PackageEntity packageEntity)
    {
        var response = await _database.PackageUpdate(new(packageEntity));
        if (response == null)
        {
            _notification.NotifyE(_database.LastError);
            return null;
        }

        return response;
    }

    public void PackageClear()
    {
        // update the real app is checked state
        Apps.ToList().ForEach(appView => appView.IsChecked = false);
        Apps.Clear();
    }
}
}
