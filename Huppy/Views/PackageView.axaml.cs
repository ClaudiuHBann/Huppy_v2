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

    private void OnClickButtonCreatePackage(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel || packageID.Text is null)
        {
            return;
        }

        packageViewModel.PackageCreate(int.Parse(packageID.Text), packageName.Text);
    }

    private void OnClickButtonEdit(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel || packageID.Text is null || packageName.Text is null)
        {
            return;
        }

        packageID.Text = string.Empty;
        packageName.Text = string.Empty;

        packageViewModel.PackageUpdate(int.Parse(packageID.Text), packageName.Text);
    }

    private void OnClickButtonClear(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel || packageID.Text is null || packageName.Text is null)
        {
            return;
        }

        packageID.Text = "None";
        packageName.Text = "None";

        packageViewModel.PackageClear();
    }
}
}
