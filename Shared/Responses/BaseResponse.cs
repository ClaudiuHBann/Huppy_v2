using MessagePack;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class BaseResponse
{
    public bool Updated { get; set; } = false;
    public bool Deleted { get; set; } = false;

    public BaseResponse()
    {
    }
}
}
