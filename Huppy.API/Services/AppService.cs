using System.Net;

using Huppy.API.Models;

using Microsoft.EntityFrameworkCore;

using Shared.Exceptions;
using Shared.Models;

namespace Huppy.API.Services
{
public class AppService
(HuppyContext context) : BaseService<AppEntity>(context)
{
    protected override async Task CreateValidate(AppEntity entity) => await Validate(entity);
    // Read validates at the same time
    protected override Task ReadValidate(AppEntity entity) => Task.CompletedTask;
    protected override async Task UpdateValidate(AppEntity entity) => await Validate(entity);
    // Delete validates at the same time
    protected override Task DeleteValidate(AppEntity entity) => Task.CompletedTask;

    private async Task Validate(AppEntity entity)
    {
        if (!await context.Categories.AnyAsync(category => category.Id == entity.Category))
        {
            throw new DatabaseException(new(HttpStatusCode.BadRequest, "The app's category is not valid!"));
        }

        // only for Update but works for Create too...
        var entityReal = await ReadEx(entity.Id);
        if (entityReal != null && entityReal.Proposed == false)
        {
            throw new DatabaseException(new(HttpStatusCode.Unauthorized, "A trusted app can not be edited!"));
        }

        var entityRealWithSameName = await context.Apps.FirstOrDefaultAsync(app => app.Name == entity.Name);
        if (entityRealWithSameName != null && entity.Id != entityRealWithSameName.Id)
        {
            throw new DatabaseException(new(HttpStatusCode.BadRequest, $"The app '{entity.Name}' already exists!"));
        }
    }

    public override async Task<AppEntity> Read(AppEntity entity) => await FindByIdOrName(entity.Id, entity.Name);

    public override async Task<AppEntity> Delete(AppEntity entity) => await DeleteEx(await Read(entity));

    private async Task<AppEntity> FindByIdOrName(Guid id, string name)
    {
        var entity = await context.Apps.FirstOrDefaultAsync(app => app.Name == name);
        if (entity != null)
        {
            return entity;
        }

        return await ReadEx(id);
    }
}
}
