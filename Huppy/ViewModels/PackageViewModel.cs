using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Huppy.ViewModels
{
public class PackageViewModel : ObservableObject
{
    public ObservableCollection<App> Apps { get; set; } = [];
}
}
