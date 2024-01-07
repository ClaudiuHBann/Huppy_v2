using Huppy.API.Models;
using Huppy.API.Controllers;

using Microsoft.EntityFrameworkCore;

using Shared.Models;

namespace Huppy.API.Services
{
public class CategoryService
(ILogger<CategoryController> logger, HuppyContext context) : BaseService<CategoryController>(logger, context)
{
    public async Task<List<KeyValuePair<CategoryEntity, List<AppEntity>>>> GetCategoryToApps()
    {
        ClearLastError();

        List<KeyValuePair<CategoryEntity, List<AppEntity>>> categoryToApps = [];

        var groupsUnordered = await context.Apps.GroupBy(app => app.CategoryNavigation).ToListAsync();
        var groupsOrdered = groupsUnordered.OrderBy(group => group.Key.Name);
        foreach (var group in groupsOrdered)
        {
            categoryToApps.Add(new(group.Key, [..group]));
        }

        return categoryToApps;
    }
}
}
