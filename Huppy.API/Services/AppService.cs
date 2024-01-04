using Huppy.API.Models;
using Huppy.API.Controllers;

using Shared.Requests;

using Microsoft.EntityFrameworkCore;

namespace Huppy.API.Services
{
public class AppService
(ILogger<PackageController> logger, HuppyContext context)
{
    public async Task<bool> Update(AppRequest appRequest)
    {
        var appEntity = await context.Apps.FirstOrDefaultAsync(app => app.Id == appRequest.Id);
        if (appEntity == null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(appRequest.ImagePath))
        {
            appEntity.ImageRaw = appRequest.ImageRaw;
        }
        else
        {
            // TODO: this works only locally for us to update the icons (DELETE ME WHEN DONE)
            appEntity.ImageRaw = await File.ReadAllBytesAsync(appRequest.ImagePath);
        }

        context.Apps.Update(appEntity);
        return await context.SaveChangesAsync() > 0;
    }
}
}
