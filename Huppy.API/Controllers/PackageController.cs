using Microsoft.AspNetCore.Mvc;

using Shared.Requests;
using Shared.Responses;

using Huppy.API.Services;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class PackageController
(ILogger<PackageController> logger, PackageService service) : BaseController<PackageController>(logger)
{

    [HttpPost(nameof(Create))]
    public async Task<ActionResult> Create([FromBody] PackageRequest request)
    {
        var entity = await service.Create(request);
        if (entity == null)
        {
            return MakeAndLogBadRequest(service.LastError);
        }

        return Ok(new PackageResponse(entity));
    }

    [HttpPost(nameof(Update))]
    public async Task<ActionResult> Update([FromBody] PackageRequest request)
    {
        var entity = await service.Update(request);
        if (entity == null)
        {
            return MakeAndLogBadRequest(service.LastError);
        }

        return Ok(new PackageResponse(entity, true));
    }

    [HttpPost(nameof(Load))]
    public async Task<ActionResult> Load([FromBody] PackageRequest request)
    {
        var entity = await service.Load(request);
        if (entity == null)
        {
            return MakeAndLogBadRequest(service.LastError);
        }

        return Ok(new PackageResponse(entity));
    }
}
}
