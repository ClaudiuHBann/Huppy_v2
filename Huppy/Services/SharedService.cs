using System.Linq;
using System.Collections.Generic;

using Huppy.Models;
using Huppy.ViewModels;

namespace Huppy.Services
{
public class SharedService : BaseService
{
    public CategoryViewModel? CategoryViewModel { get; set; } = null;
    public PackageViewModel? PackageViewModel { get; set; } = null;

    public List<CategoryModel> GetCategoryModels()
    {
        if (CategoryViewModel == null)
        {
            return [];
        }

        return CategoryViewModel.CategoryToApps.Select(pair => pair.Key).ToList();
    }
}
}
