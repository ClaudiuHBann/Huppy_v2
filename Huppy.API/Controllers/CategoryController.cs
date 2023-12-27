using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Huppy.API.Models;

using Shared.Models;
using Shared.Utilities;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class CategoryController
(ILogger<CategoryController> logger, HuppyContext context) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<ActionResult> GetCategoryToApps()
    {
        List<KeyValuePair<CategoryEntity, List<AppEntity>>> categoryToApps = [];

        var groupsUnordered = await context.Apps.GroupBy(app => app.CategoryNavigation).ToListAsync();
        var groupsOrdered = groupsUnordered.OrderBy(group => group.Key.Name);
        foreach (var group in groupsOrdered)
        {
            categoryToApps.Add(new(group.Key, [..group]));
        }

        return Ok(categoryToApps.ToJSON());
    }
}
}
