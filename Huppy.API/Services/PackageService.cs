using Huppy.API.Controllers;
using Huppy.API.Models;

using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Shared.Models;

namespace Huppy.API.Services
{
public class PackageService
(ILogger<PackageController> logger, HuppyContext context)
{
    public async Task<bool> AreAppsValid(int[] apps)
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

    public async Task<bool> Add(PackageEntity entity)
    {
        await context.Packages.AddAsync(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<int> Update(PackageEntity entity)
    {
        context.Packages.Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> Count() => await context.Packages.CountAsync();

    public async Task < PackageEntity ? > FirstOrDefault(Expression<Func<PackageEntity, bool>> func) =>
                                            await context.Packages.FirstOrDefaultAsync(func);

    public async Task<string> FindUniquePackageName(string name)
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
