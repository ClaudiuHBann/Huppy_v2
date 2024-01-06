using Huppy.API.Models;
using Huppy.API.Controllers;

using Microsoft.EntityFrameworkCore;

using Shared.Models;
using Shared.Requests;

namespace Huppy.API.Services
{
public class PackageService
(ILogger<PackageController> logger, HuppyContext context) : BaseService<PackageController>(logger, context)
{
    private async Task<bool> ValideApps(int[] apps)
    {
        ClearLastError();

        // TODO: a better way of not doing 69 million requests?
        foreach (var app in apps)
        {
            if (await context.Apps.FindAsync(app) == null)
            {
                SetLastError("The package's apps contain invalid apps!");
                return false;
            }
        }

        return true;
    }

    public async Task < PackageEntity ? > Create(PackageRequest packageRequest)
    {
        ClearLastError();

        if (!await ValideApps(packageRequest.Apps))
        {
            return null;
        }

        // this method will never fail on name not unqiue so:
        packageRequest.Name = await FindUniquePackageName(packageRequest.Name);

        var packageEntity = await Add(new PackageEntity(packageRequest));
        if (packageEntity == null)
        {
            SetLastError("The package could not be created.");
        }

        return packageEntity;
    }

    public async Task < PackageEntity ? > Update(PackageRequest packageRequest)
    {
        ClearLastError();

        if (!await ValidateForUpdate(packageRequest))
        {
            return null;
        }

        var packageEntity = await Update(new PackageEntity(packageRequest));
        if (packageEntity == null)
        {
            SetLastError("The package could not be updated.");
        }

        return packageEntity;
    }

    public async Task < PackageEntity ? > Load(PackageRequest packageRequest)
    {
        ClearLastError();

        var packageEntity = await FindByIdOrName(packageRequest.Id, packageRequest.Name);
        if (packageEntity == null)
        {
            SetLastError("The package could not be loaded.");
        }

        return packageEntity;
    }

    private async Task<bool> ValidateForUpdate(PackageRequest packageRequest)
    {
        ClearLastError();

        if (!await ValideApps(packageRequest.Apps))
        {
            return false;
        }

        var packageEntity = await FindBy<PackageEntity>(packageRequest.Id);
        if (packageEntity == null)
        {
            SetLastError("The package could not be found!");
            return false;
        }

        var packageEntityWithSameName = await FindBy<PackageEntity>(packageRequest.Name);
        if (packageEntityWithSameName != null && packageRequest.Id != packageEntityWithSameName.Id)
        {
            SetLastError($"The package \"{packageRequest.Name}\" already exists!");
            return false;
        }

        return true;
    }

    private async Task < PackageEntity ? > FindByIdOrName(int id, string name)
    {
        ClearLastError();

        var packageEntity = await FindBy<PackageEntity>(id != -1 ? id : name);
        if (packageEntity == null)
        {
            SetLastError("The package could not be found!");
        }

        return packageEntity;
    }

    private async Task<string> FindUniquePackageName(string name)
    {
        ClearLastError();

        if (string.IsNullOrWhiteSpace(name) || await context.Packages.AnyAsync(package => package.Name == name))
        {
            name = Guid.NewGuid().ToString();
            logger.LogInformation($"The package's name was not unique so we generated one: {name}");
        }

        return name;
    }
}
}
