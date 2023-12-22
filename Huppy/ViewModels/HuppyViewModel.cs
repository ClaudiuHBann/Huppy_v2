using CommunityToolkit.Mvvm.ComponentModel;

using Huppy.Utilities;

namespace Huppy.ViewModels
{
public class HuppyViewModel : ObservableObject
{
    public PackageViewModel PackageViewModel { get; set; }
    public CategoryViewModel CategoryViewModel { get; set; }
    public SearchViewModel SearchViewModel { get; set; }

    public HuppyViewModel()
    {
        PackageViewModel = DI.Create<PackageViewModel>();
        CategoryViewModel = DI.Create<CategoryViewModel>(PackageViewModel);
        SearchViewModel = new(CategoryViewModel);
    }
}
}
