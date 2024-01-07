using Huppy.API.Models;
using Huppy.API.Controllers;

using Shared.Models;
using Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace Huppy.API.Services
{
public class LinkService
(ILogger<LinkController> logger, HuppyContext context) : BaseService<LinkController>(logger, context)
{
    public async Task < LinkEntity ? > Create(LinkRequest request)
    {
        ClearLastError();

        if (!await Validate(request))
        {
            return null;
        }

        var entity = await Create(new LinkEntity(request));
        if (entity == null)
        {
            SetLastError("The link could not be created.");
        }

        return entity;
    }

    public async Task < LinkEntity ? > Update(LinkRequest request)
    {
        ClearLastError();

        if (!await Validate(request))
        {
            return null;
        }

        var entity = await Update(new LinkEntity(request));
        if (entity == null)
        {
            SetLastError("The link could not be updated.");
        }

        return entity;
    }

    public async Task < LinkEntity ? > Load(LinkRequest request)
    {
        ClearLastError();

        var entity = await FindBy<LinkEntity>(request.Id);
        if (entity == null)
        {
            SetLastError("The link could not be loaded.");
        }

        return entity;
    }

    private async Task<bool> Validate(LinkRequest request)
    {
        ClearLastError();

        if (!await context.Apps.AnyAsync(app => app.Id == request.App))
        {
            SetLastError("The link's app is not valid.");
            return false;
        }

        return true;
    }
}
}
