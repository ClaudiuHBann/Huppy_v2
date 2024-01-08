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
    [HttpGet(nameof(GetCALs))]
    public async Task<ActionResult> GetCALs() => MakeOk(new CategoryResponse(await service.GetCALs()));
}
}
