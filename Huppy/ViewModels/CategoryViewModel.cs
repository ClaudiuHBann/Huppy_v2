using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;
using Huppy.Utilities;

using Avalonia.Threading;

namespace Huppy.ViewModels
{
public partial class CategoryViewModel : ObservableObject
{
    // TODO: can we make this a map?
    public ObservableCollection<KeyValuePair<CategoryModel, AppViewModel>> CategoryToApps { get; set; } = [];

    private readonly DatabaseService _database;
    private readonly PackageViewModel _packageViewModel;

    public CategoryViewModel(DatabaseService database, PackageViewModel packageViewModel)
    {
        _database = database;
        _packageViewModel = packageViewModel;

        Populate();
    }

    public async void Populate()
    {
        foreach (var pair in await _database.GetCategoryToApps())
        {
            ObservableCollection<AppModel> collection = [];
            pair.Value.Select(app => new AppModel(app)).ToList().ForEach(collection.Add);

            await Dispatcher.UIThread.InvokeAsync(
                () => CategoryToApps.Add(new(new(pair.Key), new(collection, _packageViewModel))));
        }
    }
}
}
