using System.Linq;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public class SearchViewModel : ObservableObject
{
    public ObservableCollection<CategoryModel> Categories { get; set; }
    public SearchModel Search { get; set; } = new();

    private readonly CategoryViewModel _categoryViewModel;

    public SearchViewModel(CategoryViewModel categoryViewModel)
    {
        _categoryViewModel = categoryViewModel;

        Categories = new(_categoryViewModel.CategoryToApps.Select(pair => pair.Key));
        Categories.Insert(0, SearchModel.CategoryAll);
    }

    private static bool CanBe(string query, string app)
    {
        query = query.ToLower();
        app = app.ToLower();

        return app.Contains(query);
    }

    public void Filter()
    {
        // clear the visible flag for categories
        _categoryViewModel.CategoryToApps
            .SelectMany(pair =>
                        {
                            pair.Key.IsVisible = true;
                            return pair.Value.Apps;
                        })
            // clear the visible flag for apps
            .ToList()
            .ForEach(appView => appView.IsVisible = true);

        // first hide whole categories to speed up the app hiding
        if (Search.Category != null && Search.Category.Category.Id != SearchModel.CategoryAll.Category.Id)
        {
            _categoryViewModel.CategoryToApps.ToList().ForEach(pair => pair.Key.IsVisible =
                                                                   pair.Key.Category.Id == Search.Category.Category.Id);
        }

        if (Search.Query.Length != 0)
        {
            _categoryViewModel.CategoryToApps.Where(pair => pair.Key.IsVisible)
                .SelectMany(pair => pair.Value.Apps)
                .ToList()
                .ForEach(appView => appView.IsVisible = CanBe(Search.Query, appView.App.Name));

            // hide the categories with 0 apps
            _categoryViewModel.CategoryToApps
                .Where(pair => pair.Key.IsVisible && !pair.Value.Apps.Any(appView => appView.IsVisible))
                .Select(pair => pair.Key)
                .ToList()
                .ForEach(category => category.IsVisible = false);
        }
    }
}
}
