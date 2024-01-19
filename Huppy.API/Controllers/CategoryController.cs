using Microsoft.AspNetCore.Mvc;

using Huppy.API.Services;

using Shared.Responses;

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
}
}
