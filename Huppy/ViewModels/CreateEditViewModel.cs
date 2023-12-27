using Huppy.Utilities;

namespace Huppy.ViewModels
{
public class CreateEditViewModel : ViewModelBase
{
    public PackageViewModel PackageViewModel { get; set; }
    public CategoryViewModel CategoryViewModel { get; set; }
    public SearchViewModel SearchViewModel { get; set; }

    public CreateEditViewModel()
    {
        PackageViewModel = DI.Create<PackageViewModel>();
        CategoryViewModel = DI.Create<CategoryViewModel>(PackageViewModel);
        SearchViewModel = new(CategoryViewModel);
    }
}
}
