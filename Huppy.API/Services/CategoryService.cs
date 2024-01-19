using Huppy.API.Models;
using Microsoft.EntityFrameworkCore;

using Shared.Models;
using Shared.Utilities;

namespace Huppy.API.Services
{
public class CategoryService
(HuppyContext context) : BaseService<CategoryEntity>(context)
{
    public async Task<List<CAL>> GetCALs()
    {
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
