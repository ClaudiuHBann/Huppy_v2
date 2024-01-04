using CommunityToolkit.Mvvm.ComponentModel;

using Shared.Models;
using Shared.Entities;

namespace Huppy.Models
{
public partial class AppModel
(AppEntity app) : ObservableObject
{
    public AppEntity App { get; set; } = app;

    [ObservableProperty]
    public bool isChecked = false;

    [ObservableProperty]
    private bool isVisible = !app.Proposed;

    public bool Update(SettingsEntity settings) => IsVisible = !App.Proposed || settings.ShowProposedApps;
}
}
