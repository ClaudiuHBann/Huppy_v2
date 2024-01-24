using MessagePack;

using Shared.Models;
using Shared.Utilities;

namespace Shared.Responses
{
[MessagePackObject(true)]
public class CategoryResponse : BaseResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public List<CAL> CALs { get; set; } = new();

    public CategoryResponse()
    {
    }

    public CategoryResponse(List<CAL> cals)
    {
        CALs = cals;
    }

    public CategoryResponse(CategoryEntity entity)
    {
        Id = entity.Id;
    }
}
}
