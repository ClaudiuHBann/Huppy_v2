using System.Collections.ObjectModel;

using Huppy.Models;
using Huppy.Services;

namespace Huppy.ViewModels
{
public class AppViewModel : ViewModelBase
{
    public ObservableCollection<AppModel> Apps { get; set; }

    public SharedService Shared { get; set; }

    public AppViewModel(ObservableCollection<AppModel> apps, SharedService shared)
    {
        Apps = apps;
        Shared = shared;
    }

    public void PackageAdd(AppModel app)
    {
        Shared.PackageViewModel?.Apps.Add(app);
    }

    public void PackageRemove(AppModel app)
    {
        Shared.PackageViewModel?.Apps.Remove(app);
    }
}
}
