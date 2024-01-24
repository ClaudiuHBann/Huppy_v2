using MessagePack;

namespace Shared.Requests
{
[MessagePackObject(true)]
public class CategoryRequest : BaseRequest
{
    public Guid Id { get; set; } = Guid.Empty;

    public CategoryRequest()
    {
    }
}
}
