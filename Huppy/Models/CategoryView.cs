using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.Models
{
public partial class CategoryView
(Category category) : ObservableObject
{
    public Category Category { get; set; } = category;

    [ObservableProperty]
    public bool isVisible = false;
}
}
