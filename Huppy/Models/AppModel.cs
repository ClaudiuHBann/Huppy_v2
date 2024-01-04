using System.IO;

using CommunityToolkit.Mvvm.ComponentModel;

using Shared.Models;
using Shared.Entities;

using Avalonia.Media.Imaging;

namespace Huppy.Models
{
public partial class AppModel : ObservableObject
{
    public AppEntity App { get; set; }

    [ObservableProperty]
    private Bitmap image;

    [ObservableProperty]
    public bool isChecked = false;

    [ObservableProperty]
    private bool isVisible;

    public AppModel(AppEntity app)
    {
        App = app;
        IsVisible = !App.Proposed;
        Image = new(new MemoryStream(App.ImageRaw));
    }

    public bool Update(SettingsEntity settings) => IsVisible = !App.Proposed || settings.ShowProposedApps;
}
}
