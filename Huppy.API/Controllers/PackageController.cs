using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Huppy.API.Models;

using Shared.Models;
using Shared.Requests;
using Shared.Responses;
using Shared.Utilities;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class PackageController
(ILogger<PackageController> logger, HuppyContext context) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult> PackageCreate([FromBody] PackageRequest packageRequest)
    {
        if (!await AreAppsValid(packageRequest.Apps))
        {
            return MakeAndLogBadRequest("The package could not be created because it contains invalid apps!");
        }

        // this method will never fail on name not unqiue so:
        if (packageRequest.Name == null || packageRequest.Name == "")
        {
            packageRequest.Name = Guid.NewGuid().ToString();
            logger.LogInformation($"The package did not have any name so we generated one: {packageRequest.Name}");
        }

        var id = await context.Packages.CountAsync() + 1;
        var nameUnique = await FindUniquePackageName(packageRequest.Name);

        PackageEntity packageEntity = new() { Id = id, Apps = packageRequest.Apps, Name = nameUnique };

        await context.Packages.AddAsync(packageEntity);
        if (context.SaveChanges() > 0)
        {
            var packageResponse = new PackageResponse(packageEntity);
            return Ok(packageResponse.ToJSON());
        }
        else
        {
            return MakeAndLogBadRequest("The package could not be created.");
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> PackageUpdate([FromBody] PackageRequest packageRequest)
    {
        if (!await AreAppsValid(packageRequest.Apps))
        {
            return MakeAndLogBadRequest("The package could not be created because it contains invalid apps!");
        }

        var packageEntity = await context.Packages.FirstOrDefaultAsync(package => package.Id == packageRequest.Id);
        if (packageEntity == null)
        {
            return MakeAndLogBadRequest("The package could not be found!");
        }

        var packageEntityWithSameName =
            await context.Packages.FirstOrDefaultAsync(package => package.Name == packageRequest.Name);
        if (packageEntityWithSameName != null && packageRequest.Id != packageEntityWithSameName.Id)
        {
            return MakeAndLogBadRequest($"The package with the name \"{packageRequest.Name}\" already exists!");
        }

        packageEntity.Apps = packageRequest.Apps;
        packageEntity.Name = packageRequest.Name;

        // if the properties are the same SaveChanges will return 0 but it's fine so:
        var savedChanges = context.SaveChanges();
        var updated = savedChanges == 0 && Enumerable.SequenceEqual(packageEntity.Apps, packageRequest.Apps) &&
                      packageEntity.Name == packageRequest.Name;

        return Ok(savedChanges > 0 || updated);
    }

    private BadRequestObjectResult MakeAndLogBadRequest(string message)
    {
        logger.LogError(message);
        return BadRequest(message);
    }

    private async Task<bool> AreAppsValid(int[] apps)
    {
        foreach (var app in apps)
        {
            if (await context.Apps.FindAsync(app) == null)
            {
                return false;
            }
        }

        return true;
    }

    private async Task<string> FindUniquePackageName(string name)
    {
        if (await context.Packages.AnyAsync(package => package.Name == name))
        {
            var nameUnique = Guid.NewGuid().ToString();
            logger.LogInformation($"The package did not have a unique name so we generated one: {nameUnique}");
            return nameUnique;
        }
        else
        {
            return name;
        }
    }
}
}
