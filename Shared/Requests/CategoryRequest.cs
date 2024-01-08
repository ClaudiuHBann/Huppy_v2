using MessagePack;

namespace Shared.Requests
{
[MessagePackObject(true)]
public class CategoryRequest : BaseRequest
{
    public CategoryRequest()
    {
    }
}
}
