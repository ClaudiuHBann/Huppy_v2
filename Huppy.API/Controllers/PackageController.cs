using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Huppy.API.Models;

using Shared.Models;
using Shared.Requests;
using Shared.Responses;
using Shared.Utilities;
using Huppy.API.Services;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class PackageController : BaseController<PackageController>
{
    private readonly PackageService _packageService;

    public PackageController(ILogger<PackageController> logger, PackageService packageService) : base(logger)
    {
        _packageService = packageService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> PackageCreate([FromBody] PackageRequest packageRequest)
    {
        if (packageRequest.Apps != null && !await _packageService.AreAppsValid(packageRequest.Apps))
        {
            return MakeAndLogBadRequest("The package could not be created because it contains invalid apps!");
        }

        // this method will never fail on name not unqiue so:
        if (packageRequest.Name == null || packageRequest.Name == "")
        {
            packageRequest.Name = Guid.NewGuid().ToString();
            Logger.LogInformation($"The package did not have any name so we generated one: {packageRequest.Name}");
        }

        var id = await _packageService.Count() + 1;
        var nameUnique = await _packageService.FindUniquePackageName(packageRequest.Name);

        PackageEntity packageEntity = new() { Id = id, Apps = packageRequest.Apps ?? [], Name = nameUnique };
        if (await _packageService.Add(packageEntity))
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
        if (packageRequest.Apps != null && !await _packageService.AreAppsValid(packageRequest.Apps))
        {
            return MakeAndLogBadRequest("The package could not be created because it contains invalid apps!");
        }

        var packageEntity = await _packageService.FirstOrDefault(package => package.Id == packageRequest.Id);
        if (packageEntity == null)
        {
            return MakeAndLogBadRequest("The package could not be found!");
        }

        var packageEntityWithSameName =
            await _packageService.FirstOrDefault(package => package.Name == packageRequest.Name);
        if (packageEntityWithSameName != null && packageRequest.Id != packageEntityWithSameName.Id)
        {
            return MakeAndLogBadRequest($"The package with the name \"{packageRequest.Name}\" already exists!");
        }

        packageEntity.Name = packageRequest.Name;
        if (packageRequest.Apps != null)
        {
            packageEntity.Apps = packageRequest.Apps;
        }

        // if the properties are the same SaveChanges will return 0 but it's fine so:
        var savedChanges = await _packageService.Update(packageEntity);
        var updatedApps =
            packageRequest.Apps == null || Enumerable.SequenceEqual(packageEntity.Apps, packageRequest.Apps);
        var updated = savedChanges == 0 && updatedApps && packageEntity.Name == packageRequest.Name;

        return Ok(savedChanges > 0 || updated);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> PackageLoad([FromBody] PackageRequest packageRequest)
    {
        PackageEntity? packageEntity = null;
        if (packageRequest.Id != -1)
        {
            packageEntity = await _packageService.FirstOrDefault(package => package.Id == packageRequest.Id);
        }
        else
        {
            packageEntity = await _packageService.FirstOrDefault(package => package.Name == packageRequest.Name);
        }

        if (packageEntity == null)
        {
            return MakeAndLogBadRequest("The package could not be found!");
        }

        var packageResponse = new PackageResponse(packageEntity);
        return Ok(packageResponse.ToJSON());
    }
}
}
