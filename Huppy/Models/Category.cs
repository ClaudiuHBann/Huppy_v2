using System.Collections.Generic;

namespace Huppy.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? Count { get; set; }

    public virtual ICollection<App> Apps { get; set; } = new List<App>();
}
