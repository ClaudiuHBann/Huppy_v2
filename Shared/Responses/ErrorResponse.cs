using MessagePack;

namespace Shared.Responses
{
[MessagePackObject]
public class ErrorResponse
{
    [Key(0)]
    public string Message { get; set; } = "";

    public ErrorResponse(string message)
    {
        Message = message;
    }
}
}
