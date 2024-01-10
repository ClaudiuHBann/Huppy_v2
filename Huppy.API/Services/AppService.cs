using Huppy.API.Models;

using Microsoft.EntityFrameworkCore;

using Shared.Models;

namespace Huppy.API.Services
{
public class AppService
(HuppyContext context) : BaseService<AppEntity>(context)
{
    protected override async Task <bool> CreateValidate(AppEntity ? entity) => await Validate(entity);
    protected override Task <bool> ReadValidate(AppEntity ? entity) => Task.FromResult(true); // Read validates at the same time
    protected override async Task <bool> UpdateValidate(AppEntity ? entity) => await Validate(entity);
    protected override Task<bool> DeleteValidate(AppEntity? entity) => Task.FromResult(true); // Delete validates at the same time

    private async Task<bool> Validate(AppEntity? entity)
    {
        ClearLastError();

        if (entity == null)
        {
            return false;
        }

        if (!await context.Categories.AnyAsync(category => category.Id == entity.Category))
        {
            SetLastError($"The app's category is not valid!");
            return false;
        }

        // only for Update but works for Create too...
        var entityReal = await ReadEx(entity.Id);
        if (entityReal != null && entityReal.Proposed == false)
        {
            SetLastError($"A trusted app can not be edited!");
            return false;
        }

        var entityRealWithSameName = await context.Apps.FirstOrDefaultAsync(app => app.Name == entity.Name);
        if (entityRealWithSameName != null && entity.Id != entityRealWithSameName.Id)
        {
            SetLastError($"The app \"{entity.Name}\" already exists!");
            return false;
        }

        return true;
    }

    public override async Task < AppEntity ? > Read(AppEntity ? entity)
    {
        ClearLastError();

        if (entity == null)
        {
            return null;
        }

        var entityReal = await FindByIdOrName(entity.Id, entity.Name);
        if (entityReal == null)
        {
            SetLastError("The app could not be found!");
        }

        return entityReal;
    }

    public override async Task < AppEntity ? > Delete(AppEntity ? entity)
    {
        ClearLastError();

        if (entity == null)
        {
            return null;
        }

        var entityReal = await FindByIdOrName(entity.Id, entity.Name);
        if (entityReal == null)
        {
            SetLastError("The app could not be found!");
        }

        return await DeleteEx(entityReal);
    }

    private async Task < AppEntity ? > FindByIdOrName(int id, string name)
    {
        ClearLastError();

        var entity = await context.Apps.FirstOrDefaultAsync(app => app.Name == name);
        if (entity != null)
        {
            return entity;
        }

        entity = await ReadEx(id);
        if (entity == null)
        {
            SetLastError("The app could not be found!");
        }

        return entity;
    }
}
}
