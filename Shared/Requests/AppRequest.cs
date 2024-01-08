using MessagePack;

namespace Shared.Requests
{
[MessagePackObject]
public class AppRequest
{
    [Key(0)]
    public int Id { get; set; } = -1;
    [Key(1)]
    public int Category { get; set; } = -1;
    [Key(2)]
    public string Name { get; set; } = "";
    [Key(3)]
    public byte[] Image { get; set; } = Array.Empty<byte>();

    public AppRequest()
    {
    }
}
}
