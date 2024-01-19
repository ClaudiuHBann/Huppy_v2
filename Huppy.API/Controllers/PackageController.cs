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
    public async Task<ActionResult> Create([FromBody] PackageRequest request) =>
        await Try(async () => new PackageResponse(await service.Create(new(request))));

    [HttpGet(nameof(Read))]
    public async Task<ActionResult> Read([FromBody] PackageRequest request) =>
        await Try(async () => new PackageResponse(await service.Read(new(request))));

    [HttpPut(nameof(Update))]
    public async Task<ActionResult> Update([FromBody] PackageRequest request) =>
        await Try(async () => new PackageResponse(await service.Update(new(request))) { Updated = true });

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult> Delete([FromBody] PackageRequest request) =>
        await Try(async () => new PackageResponse(await service.Delete(new(request))) { Deleted = true });
}
}
