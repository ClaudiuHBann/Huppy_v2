using Huppy.API.Models;
using Huppy.API.Controllers;

using Microsoft.EntityFrameworkCore;

using Shared.Utilities;

namespace Huppy.API.Services
{
public class CategoryService
(ILogger<CategoryController> logger, HuppyContext context) : BaseService<CategoryController>(logger, context)
{
    public async Task<List<CAL>> GetCALs()
    {
        ClearLastError();

        List<CAL> categoryToApps = [];

        var groupsUnordered = await context.Apps.GroupBy(app => app.CategoryNavigation).ToListAsync();
        var groupsOrdered = groupsUnordered.OrderBy(group => group.Key.Name);
        foreach (var group in groupsOrdered)
        {
            var als = group.Select(app => new AL(app, [..context.Links.Where(link => link.App == app.Id)])).ToList();
            categoryToApps.Add(new(group.Key, als));
        }

        return categoryToApps;
    }
}
}
