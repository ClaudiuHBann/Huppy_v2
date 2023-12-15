using System.Linq;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public class CategoryViewModel : ObservableObject
{
    // TODO: can we make this a map?
    public ObservableCollection<KeyValuePair<Category, AppViewModel>> CategoryToApps { get; set; } = [];

    public CategoryViewModel(HuppyContext context)
    {
        foreach (var app in context.Apps.GroupBy(app => app.CategoryNavigation))
        {
            CategoryToApps.Add(new(app.Key, new([..app])));
        }
    }
}
}
