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
        var entity = await service.Create(request);
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new AppResponse(entity));
    }

    [HttpPost(nameof(Read))]
    public async Task<ActionResult> Read([FromBody] AppRequest request)
    {
        var entity = await service.Read(request);
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new AppResponse(entity));
    }

    [HttpPost(nameof(Update))]
    public async Task<ActionResult> Update([FromBody] AppRequest request)
    {
        var entity = await service.Update(request);
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new AppResponse(entity, true));
    }
}
}
