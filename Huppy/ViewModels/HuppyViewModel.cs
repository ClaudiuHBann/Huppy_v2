using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Models;

namespace Huppy.ViewModels
{
public class HuppyViewModel : ObservableObject
{
    public PackageViewModel PackageViewModel { get; set; }
    public CategoryViewModel CategoryViewModel { get; set; }
    public SearchViewModel SearchViewModel { get; set; }

    public HuppyViewModel(HuppyContext context)
    {
        PackageViewModel = new PackageViewModel(context);
        CategoryViewModel = new(context, PackageViewModel);
        SearchViewModel = new(CategoryViewModel);
    }
}
}
