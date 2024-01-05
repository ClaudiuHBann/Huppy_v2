namespace Shared.Requests
{
public class AppRequest
{
    public int Category { get; set; } = -1;
    public string Name { get; set; } = null!;
    public byte[] ImageRaw { get; set; } = null!;
    public string Url { get; set; } = null!;
}
}
