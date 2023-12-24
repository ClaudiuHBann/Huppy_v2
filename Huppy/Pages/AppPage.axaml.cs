using Avalonia.Input;
using Avalonia.Controls;

using Huppy.Models;
using Huppy.ViewModels;

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
}
}
