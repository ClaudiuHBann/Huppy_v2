﻿using MessagePack;

using Shared.Requests;
using Shared.Responses;

namespace Shared.Models
{
[MessagePackObject(true)]
public partial class LinkEntity
{
    public Guid Id { get; set; } = Guid.Empty;

    public Guid App { get; set; } = Guid.Empty;

    public string Url { get; set; } = "";

    [IgnoreMember]
    public virtual AppEntity AppNavigation { get; set; } = null!;

    public LinkEntity()
    {
    }

    public LinkEntity(LinkRequest request)
    {
        Id = request.Id;
        App = request.App;
        Url = request.Url;
    }

    public LinkEntity(LinkResponse response)
    {
        Id = response.Id;
        App = response.App;
        Url = response.Url;
    }
}
}
