using MessagePack;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class ErrorResponse : BaseResponse
{
    public string Message { get; set; } = "";

    public ErrorResponse(string message)
    {
        Message = message;
    }
}
}
