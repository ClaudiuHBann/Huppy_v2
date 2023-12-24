using CommunityToolkit.Mvvm.ComponentModel;

using Shared.Models;

namespace Huppy.Models
{
public partial class CategoryModel
(Category category) : ObservableObject
{
    public Category Category { get; set; } = category;

    [ObservableProperty]
    public bool isVisible = true;
}
}
