using CommunityToolkit.Mvvm.ComponentModel;

namespace Huppy.Models
{
public partial class AppModel
(Shared.Models.App app) : ObservableObject
{
    public Shared.Models.App App { get; set; } = app;

    [ObservableProperty]
    public bool isVisible = true;

    [ObservableProperty]
    public bool isChecked = false;
}
}
