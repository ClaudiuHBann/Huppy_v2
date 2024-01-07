using System.Net;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Mvc;

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
        return BadRequest(message);
    }

    protected ActionResult MakeOk(object data)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(data.ToMsgPack()) };
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        return Ok(response);
    }
}
}
