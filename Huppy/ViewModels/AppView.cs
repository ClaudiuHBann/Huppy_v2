using CommunityToolkit.Mvvm.ComponentModel;

namespace Huppy.ViewModels
{
public partial class AppV
(Models.App app) : ObservableObject
{
    public Models.App App { get; set; } = app;

    [ObservableProperty]
    public bool isChecked;
}
}
