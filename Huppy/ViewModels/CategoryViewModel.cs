using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;
using Huppy.Utilities;

using Avalonia.Threading;

namespace Huppy.ViewModels
{
public partial class CategoryViewModel
(Database database, PackageViewModel packageViewModel) : ObservableObject
{
    // TODO: can we make this a map?
    public ObservableCollection<KeyValuePair<CategoryModel, AppViewModel>> CategoryToApps { get; set; } = [];

    public async Task Populate()
    {
        foreach (var pair in await database.GetCategoryToApps())
        {
            ObservableCollection<AppModel> collection = [];
            pair.Value.Select(app => new AppModel(app)).ToList().ForEach(collection.Add);

            await Dispatcher.UIThread.InvokeAsync(
                () => CategoryToApps.Add(new(new(pair.Key), new(collection, packageViewModel))));
        }
    }
}
}
