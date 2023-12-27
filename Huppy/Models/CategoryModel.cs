using CommunityToolkit.Mvvm.ComponentModel;

using Shared.Models;

namespace Huppy.Models
{
public partial class CategoryModel
(CategoryEntity category) : ObservableObject
{
    public CategoryEntity Category { get; set; } = category;

    [ObservableProperty]
    public bool isVisible = true;
}
}
