using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Huppy.ViewModels
{
public class AppViewModel
(ObservableCollection<Models.AppView> apps, PackageViewModel packageViewModel) : ObservableObject
{
    public ObservableCollection<Models.AppView> Apps { get; set; } = apps;

    public void PackageAdd(Models.AppView appView)
    {
        packageViewModel.Apps.Add(appView);
    }

    public void PackageRemove(Models.AppView appView)
    {
        packageViewModel.Apps.Remove(appView);
    }
}
}
