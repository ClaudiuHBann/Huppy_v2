using System.Net;

using Microsoft.AspNetCore.Mvc;

using Shared.Responses;
using Shared.Utilities;

namespace Huppy.API.Controllers
{
[Controller]
public abstract class BaseController : ControllerBase
{
    protected ActionResult MakeBadRequest(string message)
    {
        var error = new ErrorResponse(HttpStatusCode.BadRequest, message);
        return BadRequest(error.ToMsgPack());
    }

    protected ActionResult MakeOk(object data)
    {
        return Ok(data.ToMsgPack());
    }
}
}
