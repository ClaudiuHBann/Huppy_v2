using Microsoft.AspNetCore.Mvc;

using Huppy.API.Services;

using Shared.Responses;
using Shared.Requests;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class CategoryController
(CategoryService service) : BaseController
{
    [HttpGet(nameof(CategoriesToAppsWithLinks))]
    public async Task<ActionResult> CategoriesToAppsWithLinks() =>
        await Try(async () => new CategoryResponse(await service.CategoriesToAppsWithLinks()));

    [HttpGet(nameof(Read))]
    public async Task<ActionResult> Read([FromBody] CategoryRequest request) =>
        await Try(async () => new CategoryResponse(await service.Read(new(request))));
}
}
