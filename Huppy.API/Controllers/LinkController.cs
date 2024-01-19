using Huppy.API.Services;

using Microsoft.AspNetCore.Mvc;

using Shared.Requests;
using Shared.Responses;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class LinkController
(LinkService service) : BaseController
{
    [HttpPost(nameof(Create))]
    public async Task<ActionResult> Create([FromBody] LinkRequest request) =>
        await Try(async () => new LinkResponse(await service.Create(new(request))));

    [HttpGet(nameof(Read))]
    public async Task<ActionResult> Read([FromBody] LinkRequest request) =>
        await Try(async () => new LinkResponse(await service.Read(new(request))));

    [HttpPut(nameof(Update))]
    public async Task<ActionResult> Update([FromBody] LinkRequest request) =>
        await Try(async () => new LinkResponse(await service.Update(new(request))) { Updated = true });

    [HttpDelete(nameof(Delete))]
    public async Task<ActionResult> Delete([FromBody] LinkRequest request) =>
        await Try(async () => new LinkResponse(await service.Delete(new(request))) { Deleted = true });
}
}
