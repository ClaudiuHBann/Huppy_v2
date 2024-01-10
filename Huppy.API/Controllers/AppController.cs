using Huppy.API.Services;

using Microsoft.AspNetCore.Mvc;

using Shared.Requests;
using Shared.Responses;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class AppController
(AppService service) : BaseController
{
    [HttpPost(nameof(Create))]
    public async Task<ActionResult> Create([FromBody] AppRequest request)
    {
        var entity = await service.Create(new(request));
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new AppResponse(entity));
    }

    [HttpPost(nameof(Read))]
    public async Task<ActionResult> Read([FromBody] AppRequest request)
    {
        var entity = await service.Read(new(request));
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new AppResponse(entity));
    }

    [HttpPost(nameof(Update))]
    public async Task<ActionResult> Update([FromBody] AppRequest request)
    {
        var entity = await service.Update(new(request));
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new AppResponse(entity) { Updated = true });
    }

    [HttpPost(nameof(Delete))]
    public async Task<ActionResult> Delete([FromBody] AppRequest request)
    {
        var entity = await service.Delete(new(request));
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new AppResponse(entity) { Deleted = true });
    }
}
}
