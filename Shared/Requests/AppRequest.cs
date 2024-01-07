using MessagePack;

namespace Shared.Requests
{
[MessagePackObject(true)]
public class AppRequest
{
    public int Id { get; set; } = -1;
    public int Category { get; set; } = -1;
    public string Name { get; set; } = null!;
    public byte[] Image { get; set; } = null!;
}
}
