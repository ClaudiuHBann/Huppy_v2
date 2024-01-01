using Microsoft.AspNetCore.Mvc;

namespace Huppy.API.Controllers
{
[Controller]
public abstract class BaseController<T>(ILogger<T> logger) : ControllerBase
{
    public ILogger<T> Logger { get; } = logger;

    protected ActionResult MakeAndLogBadRequest(string message)
    {
        Logger.LogError(message);
        return BadRequest(message);
    }
}
}
