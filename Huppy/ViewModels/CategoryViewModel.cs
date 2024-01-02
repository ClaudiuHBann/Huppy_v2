using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Huppy.Models;
using Huppy.Services.Database;

using Avalonia.Threading;

namespace Huppy.ViewModels
{
public partial class CategoryViewModel : ViewModelBase
{
    // TODO: can we make this a map?
    public ObservableCollection<KeyValuePair<CategoryModel, AppViewModel>> CategoryToApps { get; set; } = [];
    public ObservableCollection<AppModel>? Apps { get; set; } = null;

    private readonly DatabaseService _database;

    public CategoryViewModel(DatabaseService database)
    {
        _database = database;

        Populate();
    }

    public async void Populate()
    {
        foreach (var pair in await _database.Category.GetCategoryToApps())
        {
            ObservableCollection<AppModel> collection = [];
            pair.Value.Select(app => new AppModel(app)).ToList().ForEach(collection.Add);

            await Dispatcher.UIThread.InvokeAsync(() => CategoryToApps.Add(new(new(pair.Key), new(collection, Apps))));
        }
    }
}
}
