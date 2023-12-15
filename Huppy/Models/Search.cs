using CommunityToolkit.Mvvm.ComponentModel;

namespace Huppy.Models
{
public partial class Search : ObservableObject
{
    [ObservableProperty]
    public string query = "";

    [ObservableProperty]
    public Category? category = null;
}
}
