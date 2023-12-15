using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Huppy.ViewModels
{
    public class AppViewModel
(ObservableCollection<AppV> apps, PackageViewModel packageViewModel) : ObservableObject
{
    public ObservableCollection<AppV> Apps { get; set; } = apps;

    public void PackageAdd(AppV appView)
    {
        packageViewModel.Apps.Add(appView);
    }

    public void PackageRemove(AppV appView)
    {
        packageViewModel.Apps.Remove(appView);
    }
}
}
