using Huppy.API.Models;
using Huppy.API.Controllers;

using Microsoft.EntityFrameworkCore;

using Shared.Models;

namespace Huppy.API.Services
{
public class PackageService
(ILogger<PackageController> logger, HuppyContext context) : BaseService<PackageEntity>(context)
{
    protected override async Task<bool> CreateValidate(PackageEntity? entity)
    {
        if (entity == null)
        {
            return false;
        }

        if (!await ValideApps(entity.Apps))
        {
            return false;
        }

        return true;
    }

    protected override Task <bool> ReadValidate(PackageEntity? entity) => Task.FromResult(true); // Read validates at the same time

    protected override async Task<bool> UpdateValidate(PackageEntity? entity)
    {
        ClearLastError();

        if (entity == null)
        {
            return false;
        }

        if (!await ValideApps(entity.Apps))
        {
            return false;
        }

        var entityReal = await ReadEx(entity.Id);
        if (entityReal == null)
        {
            SetLastError("The package could not be found!");
            return false;
        }

        var entityWithSameName = await context.Packages.FirstOrDefaultAsync(package => package.Name == entity.Name);
        if (entityWithSameName != null && entity.Id != entityWithSameName.Id)
        {
            SetLastError($"The package \"{entity.Name}\" already exists!");
            return false;
        }

        return true;
    }

    protected override Task<bool> DeleteValidate(PackageEntity? entity) => Task.FromResult(true); // Delete validates at the same time

    public override async Task < PackageEntity ? > Create(PackageEntity ? entity)
    {
        ClearLastError();

        if (entity == null)
        {
            return null;
        }

        if (!await CreateValidate(entity))
        {
            return null;
        }

        // this method will never fail on name not unqiue so:
        entity.Name = await FindUniquePackageName(entity.Name);

        var entityReal = await Create(entity);
        if (entityReal == null)
        {
            SetLastError("The package could not be created.");
        }

        return entityReal;
    }

    public override async Task < PackageEntity ? > Read(PackageEntity ? entity)
    {
        ClearLastError();

        if (entity == null)
        {
            return null;
        }

        var entityReal = await FindByIdOrName(entity.Id, entity.Name);
        if (entityReal == null)
        {
            SetLastError("The package could not be read.");
        }

        return entityReal;
    }

    public override async Task < PackageEntity ? > Delete(PackageEntity ? entity)
    {
        ClearLastError();

        if (entity == null)
        {
            return null;
        }

        var entityReal = await FindByIdOrName(entity.Id, entity.Name);
        if (entityReal == null)
        {
            SetLastError("The package could not be found!");
        }

        return await DeleteEx(entityReal);
    }

    private async Task<bool> ValideApps(Guid[] apps)
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

    private async Task < PackageEntity ? > FindByIdOrName(Guid id, string name)
    {
        ClearLastError();

        var entity = await context.Packages.FirstOrDefaultAsync(package => package.Name == name);
        if (entity != null)
        {
            return entity;
        }

        entity = await ReadEx(id);
        if (entity == null)
        {
            SetLastError("The package could not be found!");
        }

        return entity;
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
