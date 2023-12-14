using Huppy.Models;

using System.Linq;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
namespace Huppy.ViewModels;

public class AppViewModel
(ObservableCollection<Models.App> apps) : ObservableObject
{
    public ObservableCollection<Models.App> Apps { get; set; } = apps;
}
