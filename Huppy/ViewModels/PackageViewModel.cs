using System;
using System.Linq;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;
using Huppy.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Huppy.ViewModels
{
public class PackageViewModel
(HuppyContext context) : ObservableObject
{
    public ObservableCollection<AppV> Apps { get; set; } = [];

    public int ? PackageCreate(string? name = null)
    {
        // create a valid name
        if (name == null || name == "")
        {
            name = Guid.NewGuid().ToString();
        }

        var apps = Apps.Select(appView => appView.App.Id).ToArray();
        Package package = new() { Id = context.Packages.Count() + 1, Apps = apps, Name = FindUniquePackageName(name) };

        context.Packages.Add(package);
        return context.SaveChanges() > 0 ? package.Id : null;
    }

    public bool PackageUpdate(int id, string name)
    {
        if (!context.Packages.Any(package => package.Id == id) || context.Packages.Any(package => package.Name == name))
        {
            return false;
        }

        var package = context.Packages.First(package => package.Id == id);
        package.Apps = Apps.Select(appView => appView.App.Id).ToArray();
        package.Name = name;

        context.Packages.Update(package);
        return context.SaveChanges() > 0;
    }

    public void PackageClear()
    {
        // update the real app is checked state
        Apps.ToList().ForEach(appView => appView.IsChecked = false);
        Apps.Clear();
    }

    private string FindUniquePackageName(string name) =>
        context.Packages.Any(package => package.Name == name) ? Guid.NewGuid().ToString() : name;
}
}
