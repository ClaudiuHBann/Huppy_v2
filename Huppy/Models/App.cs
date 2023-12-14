namespace Huppy.Models
{
class App
{
    public int Id { get; set; } = 0;
    public required Category Category { get; set; }
    public string Name { get; set; } = "";
    public bool Proposed { get; set; } = false;
    public string Image { get; set; } = "";
}
}
