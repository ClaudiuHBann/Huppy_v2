using Huppy.API.Models;
using Huppy.API.Controllers;

using Microsoft.EntityFrameworkCore;

using Shared.Models;

using System.Linq.Expressions;

namespace Huppy.API.Services
{
public class AppService
(ILogger<PackageController> logger, HuppyContext context)
{
    public async Task<bool> Add(AppEntity appEntity, LinkEntity linkEntity)
    {
        // TODO: make a separate link service
        linkEntity.Id = await context.Links.CountAsync() + 1;
        await context.Links.AddAsync(linkEntity);

        await context.Apps.AddAsync(appEntity);
        return await context.SaveChangesAsync() > 1;
    }

    public async Task<int> Count() => await context.Apps.CountAsync();

    public async Task < AppEntity ? > FirstOrDefault(Expression<Func<AppEntity, bool>> func) =>
                                        await context.Apps.FirstOrDefaultAsync(func);
}
}
