using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public partial class CategoryV
(Category category) : ObservableObject
{
    public Category Category { get; set; } = category;

    [ObservableProperty]
    public bool isVisible = false;
}
}
