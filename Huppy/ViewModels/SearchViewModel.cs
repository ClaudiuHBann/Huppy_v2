using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Huppy.Models;
using Huppy.Services;

namespace Huppy.ViewModels
{
public class SearchViewModel : ViewModelBase
{
    public ObservableCollection<CategoryModel> Categories { get; set; } = [];
    public SearchModel Search { get; set; } = new();

    private readonly CategoryViewModel _categoryViewModel;
    private readonly SettingsService _settings;

    public SearchViewModel(CategoryViewModel categoryViewModel, SettingsService settings)
    {
        _categoryViewModel = categoryViewModel;
        _settings = settings;

        _categoryViewModel.CategoryToApps.CollectionChanged += OnCollectionChanged;
        Categories.Insert(0, SearchModel.CategoryAll);
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems == null)
        {
            return;
        }

        var pair = (KeyValuePair<CategoryModel, AppViewModel>?)e.NewItems[0];
        if (pair == null)
        {
            return;
        }

        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            Categories.Insert(0, pair.Value.Key);
        }
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
            .ForEach(appView => appView.SetVisibility(true, _settings));

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
                .ForEach(appView => appView.SetVisibility(CanBe(Search.Query, appView.App.Name), _settings));

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
