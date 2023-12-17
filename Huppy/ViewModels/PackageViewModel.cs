using System.Linq;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public class PackageViewModel
(HuppyContext context) : ObservableObject
{
    public ObservableCollection<AppV> Apps { get; set; } = [];

    public void PackageCreate(int id, string? name = null)
    {
        var apps = Apps.Select(appView => appView.App.Id).ToArray();
        Package package = new() { Id = id, Apps = apps, Name = name ?? id.ToString() };

        context.Add(package);
    }

    public void PackageUpdate(int id, string? name = null)
    {
        var apps = Apps.Select(appView => appView.App.Id).ToArray();
        Package package = new() { Id = id, Apps = apps, Name = name ?? id.ToString() };

        context.Update(package);
    }

    public void PackageClear()
    {
        // update the real app is checked state
        Apps.ToList().ForEach(appView => appView.IsChecked = false);
        Apps.Clear();
    }
}
}
