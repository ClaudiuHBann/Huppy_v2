namespace Shared.Requests
{
public class AppRequest
{
    public int Id { get; set; } = -1;

    public int Category { get; set; } = -1;

    public string Name { get; set; } = null!;

    public bool Proposed { get; set; } = true;

    public byte[] ImageRaw { get; set; } = null!;
    public string ImagePath { get; set; } = "";
}
}
