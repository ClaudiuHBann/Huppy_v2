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

        return MakeOk(new PackageResponse(entity));
    }

    [HttpPost(nameof(Update))]
    public async Task<ActionResult> Update([FromBody] PackageRequest request)
    {
        var entity = await service.Update(request);
        if (entity == null)
        {
            return MakeAndLogBadRequest(service.LastError);
        }

        return MakeOk(new PackageResponse(entity, true));
    }

    [HttpPost(nameof(Read))]
    public async Task<ActionResult> Read([FromBody] PackageRequest request)
    {
        var entity = await service.Read(request);
        if (entity == null)
        {
            return MakeAndLogBadRequest(service.LastError);
        }

        return MakeOk(new PackageResponse(entity));
    }
}
}
