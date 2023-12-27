using CommunityToolkit.Mvvm.ComponentModel;

namespace Huppy.Models
{
public partial class AppModel
(Shared.Models.AppEntity app) : ObservableObject
{
    public Shared.Models.AppEntity App { get; set; } = app;

    [ObservableProperty]
    public bool isVisible = true;

    [ObservableProperty]
    public bool isChecked = false;
}
}
