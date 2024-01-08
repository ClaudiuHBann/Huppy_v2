using Microsoft.AspNetCore.Mvc;

using Shared.Responses;
using Shared.Utilities;

namespace Huppy.API.Controllers
{
[Controller]
public abstract class BaseController<Type>(ILogger<Type> logger) : ControllerBase
{
    public ILogger<Type> Logger { get; } = logger;

    protected ActionResult MakeAndLogBadRequest(string message)
    {
        Logger.LogError(message);
        var error = new ErrorResponse(message);
        return BadRequest(error.ToMsgPack());
    }

    protected ActionResult MakeOk(object data)
    {
        return Ok(data.ToMsgPack());
    }
}
}
