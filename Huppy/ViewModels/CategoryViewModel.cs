using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public class CategoryViewModel : ObservableObject
{
    // TODO: can we make this a map?
    public ObservableCollection<KeyValuePair<CategoryView, AppViewModel>> CategoryToApps { get; set; } = [];

    public CategoryViewModel(HuppyContext context, PackageViewModel packageViewModel)
    {
        foreach (var group in context.Apps.GroupBy(app => app.CategoryNavigation))
        {
            ObservableCollection<AppView> appViews = [];
            foreach (var app in group)
            {
                appViews.Add(new AppView(app));
            }

            CategoryToApps.Add(new(new(group.Key), new(appViews, packageViewModel)));
        }
    }
}
}
