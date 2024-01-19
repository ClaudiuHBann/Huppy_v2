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
    public async Task<ActionResult> Create([FromBody] AppRequest request) =>
        await Try(async () => new AppResponse(await service.Create(new(request))));

    [HttpGet(nameof(Read))]
    public async Task<ActionResult> Read([FromBody] AppRequest request) =>
        await Try(async () => new AppResponse(await service.Read(new(request))));

    [HttpPut(nameof(Update))]
    public async Task<ActionResult> Update([FromBody] AppRequest request) =>
        await Try(async () => new AppResponse(await service.Update(new(request))) { Updated = true });

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult> Delete([FromBody] AppRequest request) =>
        await Try(async () => new AppResponse(await service.Delete(new(request))) { Deleted = true });
}
}
