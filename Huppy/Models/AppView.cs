using CommunityToolkit.Mvvm.ComponentModel;

namespace Huppy.Models
{
public partial class AppView
(Models.App app) : ObservableObject
{
    public Models.App App { get; set; } = app;

    [ObservableProperty]
    public bool isVisible = false;

    [ObservableProperty]
    public bool isChecked = false;
}
}
