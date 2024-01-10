using Microsoft.AspNetCore.Mvc;

using Shared.Requests;
using Shared.Responses;

using Huppy.API.Services;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class PackageController
(PackageService service) : BaseController
{

    [HttpPost(nameof(Create))]
    public async Task<ActionResult> Create([FromBody] PackageRequest request)
    {
        var entity = await service.Create(new(request));
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new PackageResponse(entity));
    }

    [HttpPost(nameof(Read))]
    public async Task<ActionResult> Read([FromBody] PackageRequest request)
    {
        var entity = await service.Read(new(request));
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new PackageResponse(entity));
    }

    [HttpPost(nameof(Update))]
    public async Task<ActionResult> Update([FromBody] PackageRequest request)
    {
        var entity = await service.Update(new(request));
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new PackageResponse(entity) { Updated = true });
    }

    [HttpPost(nameof(Delete))]
    public async Task<ActionResult> Delete([FromBody] PackageRequest request)
    {
        var entity = await service.Delete(new(request));
        if (entity == null)
        {
            return MakeBadRequest(service.LastError);
        }

        return MakeOk(new PackageResponse(entity));
    }
}
}
