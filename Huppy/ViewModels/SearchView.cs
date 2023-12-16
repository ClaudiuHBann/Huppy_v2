using CommunityToolkit.Mvvm.ComponentModel;

namespace Huppy.Models
{
public partial class SearchV : ObservableObject
{
    [ObservableProperty]
    public string query = "";

    [ObservableProperty]
    public Category? category = null;

    public static readonly Category CategoryAll = new() { Id = -1, Name = "All" };
}
}
