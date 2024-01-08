using MessagePack;

using Shared.Utilities;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class CategoryResponse : BaseResponse
{
    public List<CAL> CALs { get; set; } = new();

    public CategoryResponse()
    {
    }

    public CategoryResponse(List<CAL> cals)
    {
        CALs = cals;
    }
}
}
