using Avalonia.Controls;
using Avalonia.Interactivity;

using Huppy.ViewModels;

namespace Huppy.Views
{
public partial class PackageView : UserControl
{
    public PackageView()
    {
        InitializeComponent();
    }

    private void OnButtonClickRemove(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.DataContext is not AppV appView ||
            DataContext is not PackageViewModel packageViewModel)
        {
            return;
        }

        appView.IsChecked = !appView.IsChecked;
        packageViewModel.Apps.Remove(appView);
    }
}
}
