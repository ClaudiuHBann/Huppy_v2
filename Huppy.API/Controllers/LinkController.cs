using Huppy.API.Services;

using Microsoft.AspNetCore.Mvc;

using Shared.Requests;
using Shared.Responses;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class LinkController
(ILogger<LinkController> logger, LinkService service) : BaseController<LinkController>(logger)
{
    [HttpPost(nameof(Create))]
    public async Task<ActionResult> Create([FromBody] LinkRequest request)
    {
        var entity = await service.Create(request);
        if (entity == null)
        {
            return MakeAndLogBadRequest(service.LastError);
        }

        return Ok(new LinkResponse(entity));
    }

    [HttpPost(nameof(Update))]
    public async Task<ActionResult> Update([FromBody] LinkRequest request)
    {
        var entity = await service.Update(request);
        if (entity == null)
        {
            return MakeAndLogBadRequest(service.LastError);
        }

        return Ok(new LinkResponse(entity, true));
    }

    [HttpPost(nameof(Load))]
    public async Task<ActionResult> Load([FromBody] LinkRequest request)
    {
        var entity = await service.Load(request);
        if (entity == null)
        {
            return MakeAndLogBadRequest(service.LastError);
        }

        return Ok(new LinkResponse(entity));
    }
}
}
