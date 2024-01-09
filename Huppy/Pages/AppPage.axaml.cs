using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;

using Huppy.Models;
using Huppy.ViewModels;
using Huppy.Views.Dialogs;

using Shared.Models;

namespace Huppy.Pages
{
public partial class AppView : UserControl
{
    public AppView()
    {
        InitializeComponent();
    }

    private void OnPointerReleasedApp(object? sender, PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton != MouseButton.Left || sender is not StackPanel stackPanel ||
            stackPanel.DataContext is not AppModel app || DataContext is not AppViewModel appViewModel)
        {
            return;
        }

        app.IsChecked = !app.IsChecked;
        if (app.IsChecked)
        {
            appViewModel.PackageAdd(app);
        }
        else
        {
            appViewModel.PackageRemove(app);
        }
    }

    private async void OnPointerReleasedImageEdit(object? sender, PointerReleasedEventArgs e)
    {
        if (sender is not Image image || image.DataContext is not AppModel appModel ||
            DataContext is not AppViewModel appViewModel)
        {
            return;
        }

        ProposeAppDialog.Context context =
            new(appModel.App.Category, appModel.App.Name, appModel.App.Image, appModel.Url);

        ProposeAppDialog dialog = new(VisualRoot as Visual, appViewModel.GetCategoryModels(), context);
        var result = await dialog.Show();
        if (result == null)
        {
            return;
        }

        var appEntity = new AppEntity() { Category = result.Category, Name = result.Name, Image = result.Image };
        var linkEntity = new LinkEntity() { Url = result.Url };

        var response = await appViewModel.AppUpdate(appEntity, linkEntity);
        if (response == null)
        {
            return;
        }

        appViewModel.AppUpdateCTA(response.Value.app, response.Value.link);
    }
}
}
