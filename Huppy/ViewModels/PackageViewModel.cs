using System.Linq;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public class PackageViewModel : ObservableObject
{
    public ObservableCollection<AppModel> Apps { get; set; } = [];

    public (int id, string name)? PackageCreate(string? name = null)
    {
        return (0, "Ma-ta");
    }

    public bool PackageUpdate(int id, string name)
    {
        return false;
    }

    public void PackageClear()
    {
        // update the real app is checked state
        Apps.ToList().ForEach(appView => appView.IsChecked = false);
        Apps.Clear();
    }
}
}
