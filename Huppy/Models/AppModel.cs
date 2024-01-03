using CommunityToolkit.Mvvm.ComponentModel;

using Shared.Models;
using Huppy.Services;

namespace Huppy.Models
{
public partial class AppModel
(AppEntity app) : ObservableObject
{
    public AppEntity App { get; set; } = app;

    [ObservableProperty]
    public bool isChecked = false;

    private bool isVisible = !app.Proposed;
    public bool IsVisible
    {
        get => isVisible;

    private
        set => SetProperty(ref isVisible, value);
    }

    public bool SetVisibility(bool visible, SettingsService settings) => IsVisible =
        visible && (!App.Proposed || settings.Settings.ShowProposedApps);
}
}
