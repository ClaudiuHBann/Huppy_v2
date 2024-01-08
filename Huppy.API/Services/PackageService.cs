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
    public async Task < PackageEntity ? > Create(PackageRequest request)
    {
        ClearLastError();

        if (!await ValideApps(request.Apps))
        {
            return null;
        }

        // this method will never fail on name not unqiue so:
        request.Name = await FindUniquePackageName(request.Name);

        var entity = await Create(new PackageEntity(request));
        if (entity == null)
        {
            SetLastError("The package could not be created.");
        }

        return entity;
    }

    public async Task < PackageEntity ? > Update(PackageRequest request)
    {
        ClearLastError();

        if (!await ValidateForUpdate(request))
        {
            return null;
        }

        var entity = await Update(new PackageEntity(request));
        if (entity == null)
        {
            SetLastError("The package could not be updated.");
        }

        return entity;
    }

    public async Task < PackageEntity ? > Read(PackageRequest request)
    {
        ClearLastError();

        var entity = await FindByIdOrName(request.Id, request.Name);
        if (entity == null)
        {
            SetLastError("The package could not be read.");
        }

        return entity;
    }

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

    private async Task<bool> ValidateForUpdate(PackageRequest request)
    {
        ClearLastError();

        if (!await ValideApps(request.Apps))
        {
            return false;
        }

        var entity = await FindByKeys<PackageEntity>(request.Id);
        if (entity == null)
        {
            SetLastError("The package could not be found!");
            return false;
        }

        var entityWithSameName = await context.Packages.FirstOrDefaultAsync(package => package.Name == request.Name);
        if (entityWithSameName != null && request.Id != entityWithSameName.Id)
        {
            SetLastError($"The package \"{request.Name}\" already exists!");
            return false;
        }

        return true;
    }

    private async Task < PackageEntity ? > FindByIdOrName(int id, string name)
    {
        ClearLastError();

        var entity = await context.Packages.FirstOrDefaultAsync(package => package.Name == name);
        if (entity != null)
        {
            return entity;
        }

        entity = await FindByKeys<PackageEntity>(id);
        if (entity != null)
        {
            return entity;
        }

        SetLastError("The package could not be found!");
        return null;
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
