using Microsoft.AspNetCore.Mvc;

using Huppy.API.Models;

using Shared.Models;
using Shared.Utilities;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class PackageController
(ILogger<PackageController> logger, HuppyContext context) : ControllerBase
{
    [HttpGet("[action]")]
    public ActionResult GetCategoryToApps()
    {
        List<KeyValuePair<Category, List<App>>> categoryToApps = [];
        foreach (var group in context.Apps.GroupBy(app => app.CategoryNavigation))
        {
            categoryToApps.Add(new(group.Key, [..group]));
        }

        return Ok(categoryToApps.ToJSON());
    }

    // public (int id, string name)? PackageCreate(string? name = null)
    //{
    //     // create a valid name
    //     if (name == null || name == "")
    //     {
    //         name = Guid.NewGuid().ToString();
    //     }
    //
    //     var apps = Apps.Select(appView => appView.App.Id).ToArray();
    //     Package package = new() { Id = context.Packages.Count() + 1, Apps = apps, Name = FindUniquePackageName(name)
    //     };
    //
    //     context.Packages.Add(package);
    //     return context.SaveChanges() > 0 ? (package.Id, package.Name) : null;
    // }
    //
    // public bool PackageUpdate(int id, string name)
    //{
    //     if (!context.Packages.Any(package => package.Id == id) || context.Packages.Any(package => package.Name ==
    //     name))
    //     {
    //         return false;
    //     }
    //
    //     var package = context.Packages.First(package => package.Id == id);
    //     package.Apps = Apps.Select(appView => appView.App.Id).ToArray();
    //     package.Name = name;
    //
    //     context.Packages.Update(package);
    //     return context.SaveChanges() > 0;
    // }

    private string FindUniquePackageName(string name) =>
        context.Packages.Any(package => package.Name == name) ? Guid.NewGuid().ToString() : name;
}
}
