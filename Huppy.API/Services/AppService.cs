using Huppy.API.Models;
using Huppy.API.Controllers;

using Microsoft.EntityFrameworkCore;

using Shared.Models;
using Shared.Requests;

namespace Huppy.API.Services
{
public class AppService
(ILogger<AppController> logger, HuppyContext context) : BaseService<AppController>(logger, context)
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

    public async Task < AppEntity ? > Load(AppRequest request)
    {
        ClearLastError();

        var entity = await FindByIdOrName(request.Id, request.Name);
        if (entity == null)
        {
            SetLastError("The app could not be loaded.");
        }

        return entity;
    }

    private async Task<bool> Validate(AppRequest request)
    {
        ClearLastError();

        if (!await Exists(request.Name))
        {
            SetLastError($"The app name \"{request.Name}\" already exists!");
            return false;
        }

        return true;
    }

    private async Task < AppEntity ? > FindByIdOrName(int id, string name)
    {
        ClearLastError();

        var entity = await FindBy<AppEntity>(id != -1 ? id : name);
        if (entity == null)
        {
            SetLastError("The app could not be found!");
        }

        return entity;
    }

    private async Task<bool> Exists(string name)
    {
        ClearLastError();

        var exists = await context.Apps.AnyAsync(app => app.Name == name);
        if (exists)
        {
            SetLastError($"The app name \"{name}\" already exists!");
        }

        return exists;
    }
}
}
