﻿using MessagePack;

using Shared.Utilities;

namespace Shared.Responses
{
[MessagePackObject]
public class CategoryResponse
{
    [Key(0)]
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
