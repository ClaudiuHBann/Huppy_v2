using System;
using System.Collections.Generic;

namespace Huppy.Models
{
class Package
{
    public int Id { get; set; } = 0;
    public Guid Uuid { get; set; } = Guid.Empty;
    public required List<int> Apps { get; set; }
}
}
