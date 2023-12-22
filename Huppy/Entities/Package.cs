namespace Huppy.Models;

public partial class Package
{
    public int Id { get; set; }

    public int[] Apps { get; set; } = null!;

    public string Name { get; set; } = null!;
}
