using MessagePack;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class BaseResponse
{
    public bool Updated { get; set; } = false;

    public BaseResponse(bool updated = false)
    {
        Updated = updated;
    }
}
}
