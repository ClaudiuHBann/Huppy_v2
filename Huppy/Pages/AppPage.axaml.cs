using Avalonia.Input;
using Avalonia.Controls;

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
            stackPanel.DataContext is not Models.AppView appView || DataContext is not AppViewModel appViewModel)
        {
            return;
        }

        appView.IsChecked = !appView.IsChecked;
        if (appView.IsChecked)
        {
            appViewModel.PackageAdd(appView);
        }
        else
        {
            appViewModel.PackageRemove(appView);
        }
    }
}
}
