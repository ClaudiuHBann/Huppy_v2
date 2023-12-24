using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public class AppViewModel
(ObservableCollection<AppModel> apps, PackageViewModel packageViewModel) : ObservableObject
{
    public ObservableCollection<AppModel> Apps { get; set; } = apps;

    public void PackageAdd(AppModel app)
    {
        packageViewModel.Apps.Add(app);
    }

    public void PackageRemove(AppModel app)
    {
        packageViewModel.Apps.Remove(app);
    }
}
}
