using MessagePack;

namespace Shared.Requests
{
[MessagePackObject]
public class LinkRequest
{
    [Key(0)]
    public int Id { get; set; } = -1;
    [Key(1)]
    public int App { get; set; } = -1;
    [Key(2)]
    public string Url { get; set; } = "";

    public LinkRequest()
    {
    }
}
}
