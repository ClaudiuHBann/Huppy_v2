using System;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Huppy.Models
{
public partial class SearchModel : ObservableObject
{
    [ObservableProperty]
    public string query = "";

    [ObservableProperty]
    public CategoryModel? category = null;

    public static readonly CategoryModel CategoryAll = new(new() { Id = Guid.Empty, Name = "All" });
}
}
