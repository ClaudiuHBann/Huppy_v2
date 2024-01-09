using System.Linq;
using System.Collections.ObjectModel;

using Huppy.Models;
using Huppy.Services;

namespace Huppy.ViewModels
{
public class SearchViewModel
(SharedService shared) : ViewModelBase
{
    public ObservableCollection<CategoryModel> Categories { get; set; } = [SearchModel.CategoryAll];
    public SearchModel Search { get; set; } = new();

    public void Populate()
    {
        if (Categories.Count > 1)
        {
            return;
        }

        shared.GetCategoryModels().ForEach(Categories.Add);
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
        shared.CategoryViewModel?.CategoryToApps
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
            shared.CategoryViewModel?.CategoryToApps.ToList().ForEach(
                pair => pair.Key.IsVisible = pair.Key.Category.Id == Search.Category.Category.Id);
        }

        if (Search.Query.Length != 0)
        {
            shared.CategoryViewModel?.CategoryToApps.Where(pair => pair.Key.IsVisible)
                .SelectMany(pair => pair.Value.Apps)
                .ToList()
                .ForEach(appView => appView.IsVisible = CanBe(Search.Query, appView.App.Name));

            // hide the categories with 0 apps
            shared.CategoryViewModel?.CategoryToApps
                .Where(pair => pair.Key.IsVisible && !pair.Value.Apps.Any(appView => appView.IsVisible))
                .Select(pair => pair.Key)
                .ToList()
                .ForEach(category => category.IsVisible = false);
        }
    }
}
}
