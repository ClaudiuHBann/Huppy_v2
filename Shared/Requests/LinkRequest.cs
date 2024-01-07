using MessagePack;

namespace Shared.Requests
{
[MessagePackObject(true)]
public class LinkRequest
{
    public int Id { get; set; } = -1;
    public int App { get; set; } = -1;
    public string Url { get; set; } = null!;
}
}
