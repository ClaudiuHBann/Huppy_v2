using System.IO;

using CommunityToolkit.Mvvm.ComponentModel;

using Shared.Models;
using Shared.Entities;

using Avalonia.Platform;
using Avalonia.Media.Imaging;

namespace Huppy.Models
{
public partial class AppModel : ObservableObject
{
    public static Bitmap ImageDefault { get; } = new(AssetLoader.Open(new("avares://Huppy/Assets/Icons/App.png")));
    public const string UrlDefault = "https://www.example.com/";

    public AppEntity App { get; set; }

    [ObservableProperty]
    public string url;

    [ObservableProperty]
    private Bitmap image;

    [ObservableProperty]
    public bool isChecked = false;

    [ObservableProperty]
    private bool isVisible;

    public AppModel(AppEntity app, LinkEntity? link)
    {
        App = app;
        IsVisible = !App.Proposed;

        if (App.Image.Length == 0)
        {
            Image = ImageDefault;
        }
        else
        {
            Image = new(new MemoryStream(App.Image));
        }

        Url = link != null ? link.Url : UrlDefault;
    }

    public bool Update(SettingsEntity settings) => IsVisible = !App.Proposed || settings.ShowProposedApps;
}
}
