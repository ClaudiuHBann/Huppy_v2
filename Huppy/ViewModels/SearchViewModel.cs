using System.Linq;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public class SearchViewModel : ObservableObject
{
    public ObservableCollection<Category> Categories { get; set; }
    public Search Search { get; set; } = new();

    public SearchViewModel(CategoryViewModel categoryViewModel)
    {
        Categories = new(categoryViewModel.CategoryToApps.Select(pair => pair.Key));
        Categories.Insert(0, new Category() {
            Name = "All",
        });
    }
}
}
