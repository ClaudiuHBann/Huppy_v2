using Microsoft.AspNetCore.Mvc;

namespace Huppy.API.Controllers
{
[Controller]
public abstract class BaseController<Type>(ILogger<Type> logger) : ControllerBase
{
    public ILogger<Type> Logger { get; } = logger;

    protected ActionResult MakeAndLogBadRequest(string message)
    {
        Logger.LogError(message);
        return BadRequest(message);
    }
}
}
