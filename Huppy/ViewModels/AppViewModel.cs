using System.Collections.ObjectModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public class AppViewModel
(ObservableCollection<AppModel> apps, ObservableCollection<AppModel>? appsPackage) : ViewModelBase
{
    public ObservableCollection<AppModel> Apps { get; set; } = apps;

    public void PackageAdd(AppModel app)
    {
        appsPackage?.Add(app);
    }

    public void PackageRemove(AppModel app)
    {
        appsPackage?.Remove(app);
    }
}
}
