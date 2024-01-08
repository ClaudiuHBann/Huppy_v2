using Huppy.API.Models;

using Microsoft.EntityFrameworkCore;

using Shared.Models;
using Shared.Requests;

namespace Huppy.API.Services
{
public class AppService
(HuppyContext context) : BaseService<AppEntity>(context)
{
    public async Task < AppEntity ? > Create(AppRequest request)
    {
        ClearLastError();

        if (!await Validate(request))
        {
            return null;
        }

        var entity = await Create(new AppEntity(request));
        if (entity == null)
        {
            SetLastError("The app could not be created.");
        }

        return entity;
    }

    public async Task < AppEntity ? > Update(AppRequest request)
    {
        ClearLastError();

        if (!await Validate(request))
        {
            return null;
        }

        var entity = await Update(new AppEntity(request));
        if (entity == null)
        {
            SetLastError("The app could not be updated.");
        }

        return entity;
    }

    public async Task < AppEntity ? > Read(AppRequest request)
    {
        ClearLastError();

        var entity = await FindByIdOrName(request.Id, request.Name);
        if (entity == null)
        {
            SetLastError("The app could not be read.");
        }

        return entity;
    }

    private async Task<bool> Validate(AppRequest request)
    {
        ClearLastError();

        if (!await context.Categories.AnyAsync(category => category.Id == request.Category))
        {
            SetLastError($"The category is not valid!");
            return false;
        }

        var entityWithSameName = await context.Apps.FirstOrDefaultAsync(app => app.Name == request.Name);
        if (entityWithSameName != null && request.Id != entityWithSameName.Id)
        {
            SetLastError($"The app \"{request.Name}\" already exists!");
            return false;
        }

        return true;
    }

    private async Task < AppEntity ? > FindByIdOrName(int id, string name)
    {
        ClearLastError();

        var entity = await context.Apps.FirstOrDefaultAsync(app => app.Name == name);
        if (entity != null)
        {
            return entity;
        }

        entity = await FindByKeys(id);
        if (entity != null)
        {
            return entity;
        }

        SetLastError("The app could not be found!");
        return null;
    }
}
}
