using System.Net;

using MessagePack;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class ErrorResponse : BaseResponse
{
    public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
    public string Message { get; set; } = "";

    public ErrorResponse()
    {
    }

    public ErrorResponse(HttpStatusCode code, string message)
    {
        Code = code;
        Message = message;
    }
}
}
