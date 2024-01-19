using System.Net;

using Microsoft.AspNetCore.Mvc;

using Shared.Responses;
using Shared.Utilities;
using Shared.Exceptions;

namespace Huppy.API.Controllers
{
[Controller]
public abstract class BaseController : ControllerBase
{
    // TODO: can we change the Task<object> to Task<XResponse>
    protected async Task<ActionResult> Try(Func<Task<object>> func)
    {
        try
        {
            return MakeOk(await func());
        }
        catch (DatabaseException exception)
        {
            return MakeResponse(exception.Error);
        }
        catch (Exception exception)
        {
            return MakeResponse(new(HttpStatusCode.InternalServerError, exception.Message));
        }
    }

    private OkObjectResult MakeOk(object data)
    {
        return Ok(data.ToMsgPack());
    }

    private ObjectResult MakeResponse(ErrorResponse error)
    {
        return error.Code switch {
            HttpStatusCode.BadRequest => MakeBadRequest(error.Message),
            HttpStatusCode.Unauthorized => Unauthorized(error.Message),
            HttpStatusCode.NotFound => NotFound(error.Message),
            HttpStatusCode.InternalServerError =>
                new ObjectResult(error.Message) { StatusCode = StatusCodes.Status500InternalServerError },
            _ => MakeBadRequest("IDK Brah..."),
        };
    }

    private BadRequestObjectResult MakeBadRequest(string message)
    {
        var error = new ErrorResponse(HttpStatusCode.BadRequest, message);
        return BadRequest(error.ToMsgPack());
    }
}
}
