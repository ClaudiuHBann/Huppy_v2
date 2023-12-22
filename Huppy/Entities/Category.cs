using System.Collections.Generic;

namespace Huppy.Models;

public partial class Category
{
    public int Id { get; set; } = -1;

    public string Name { get; set; } = null!;

    public string? Description { get; set; } = "";

    public int? Count { get; set; } = 0;

    public virtual ICollection<App> Apps { get; set; } = new List<App>();
}
