﻿using MessagePack;

using Shared.Models;

namespace Shared.Requests
{
[MessagePackObject(true)]
public class PackageRequest : BaseRequest
{
    public int Id { get; set; } = -1;

    public int[] Apps { get; set; } = Array.Empty<int>();

    public string Name { get; set; } = "";

    public PackageRequest()
    {
    }

    public PackageRequest(PackageEntity packageEntity)
    {
        Id = packageEntity.Id;
        Name = packageEntity.Name ?? "";
        Apps = packageEntity.Apps ?? Array.Empty<int>();
    }
}
}
