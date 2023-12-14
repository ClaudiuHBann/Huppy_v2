using System;

namespace Huppy.Models;

public partial class Package
{
    public int Id { get; set; }

    public Guid Uuid { get; set; }

    public int[] Apps { get; set; } = null!;
}
