using Microsoft.AspNetCore.Mvc;

using Huppy.API.Services;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class CategoryController
(ILogger<CategoryController> logger, CategoryService service) : BaseController<CategoryController>(logger)
{
    [HttpGet(nameof(GetCALs))]
    public async Task<ActionResult> GetCALs() => MakeOk(await service.GetCALs());
}
}
