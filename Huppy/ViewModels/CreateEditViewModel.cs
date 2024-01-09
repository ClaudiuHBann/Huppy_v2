using Huppy.Services;

using Microsoft.Extensions.DependencyInjection;

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
        CategoryViewModel = DI.Create<CategoryViewModel>();
        SearchViewModel = DI.Create<SearchViewModel>();

        var serviceShared = DI.ServiceProvider.GetService<SharedService>();
        if (serviceShared == null)
        {
            return;
        }

        serviceShared.PackageViewModel = PackageViewModel;
        serviceShared.CategoryViewModel = CategoryViewModel;
    }
}
}
