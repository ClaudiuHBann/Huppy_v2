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
public class PackageViewModel
(DatabaseService database, SharedService shared, NotificationService notification, ClipboardService clipboard)
    : ViewModelBase
{
    public ObservableCollection<AppModel> Apps { get; set; } = [];

    public static string PackageIDDefault { get; } = "0";
    public static string PackageNameDefault { get; } = "None";

        public async void ClipboardSaveText(string? text) => await clipboard.SetText(text);

        public async Task < PackageEntity ? > PackageCreate(PackageEntity packageEntity)
        {
            var response = await database.Packages.Create(new(packageEntity));
            if (response == null)
            {
                notification.NotifyE(database.Packages.LastError);
                return null;
            }

            return new(response);
        }

        public async Task < bool ? > PackageUpdate(PackageEntity packageEntity)
        {
            var response = await database.Packages.Update(new(packageEntity));
            if (response == null)
            {
                notification.NotifyE(database.Packages.LastError);
                return null;
            }

            return response.Updated;
        }

        public async Task < PackageEntity ? > PackageLoad(PackageEntity packageEntity)
        {
            var response = await database.Packages.Read(new(packageEntity));
            if (response == null)
            {
                notification.NotifyE(database.Packages.LastError);
                return null;
            }

            return new(response);
        }

        public void PackageLoad(Guid[] apps)
        {
            shared.CategoryViewModel?.CategoryToApps.SelectMany(pair => pair.Value.Apps)
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
