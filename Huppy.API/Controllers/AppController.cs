using Huppy.API.Services;

using Microsoft.AspNetCore.Mvc;

using Shared.Requests;

namespace Huppy.API.Controllers
{
[ApiController]
[Route("[controller]")]
public class AppController : BaseController<AppController>
{
    private readonly AppService _appService;

    public AppController(ILogger<AppController> logger, AppService appService) : base(logger)
    {
        _appService = appService;
    }

    [HttpPost(nameof(Update))]
    public async Task<ActionResult> Update([FromBody] AppRequest appRequest)
    {
        return await _appService.Update(appRequest) ? Ok() : BadRequest();
    }
}
}
