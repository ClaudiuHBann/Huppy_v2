using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

using Huppy.ViewModels;
using Huppy.Views.Dialogs;

namespace Huppy.Pages
{
public partial class PackageView : UserControl
{
    public PackageView()
    {
        InitializeComponent();
    }

    private void OnButtonClickRemove(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.DataContext is not Models.AppView appView ||
            DataContext is not PackageViewModel packageViewModel)
        {
            return;
        }

        appView.IsChecked = !appView.IsChecked;
        packageViewModel.Apps.Remove(appView);
    }

    private void OnClickButtonCreatePackage(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel || packageID.Text is null || packageName.Text is null)
        {
            return;
        }

        var id = packageViewModel.PackageCreate(packageName.Text);
        if (id == null)
        {
            return;
        }

        packageID.Text = id.ToString();
        buttonPackageEdit.IsEnabled = true;
    }

    private async void OnClickButtonEdit(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel || packageID.Text is null || packageName.Text is null)
        {
            return;
        }

        EditPackageDialog.Context context = new(packageName.Text);
        EditPackageDialog dialog = new(VisualRoot as Visual, context);
        var result = await dialog.Show();
        if (result == null)
        {
            return;
        }

        if (!packageViewModel.PackageUpdate(int.Parse(packageID.Text), result.PackageName))
        {
            return;
        }

        if (packageName.Text != result.PackageName)
        {
            buttonPackageSave.IsEnabled = true;
        }
        packageName.Text = result.PackageName;
    }

    private void OnClickButtonClear(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel || packageID.Text is null || packageName.Text is null)
        {
            return;
        }

        packageID.Text = "0";
        packageName.Text = "None";

        packageViewModel.PackageClear();

        buttonPackageCreate.IsEnabled = true;
        buttonPackageSave.IsEnabled = false;
        buttonPackageEdit.IsEnabled = false;
    }

    private void OnClickButtonSave(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel || packageID.Text is null || packageName.Text is null)
        {
            return;
        }

        packageViewModel.PackageUpdate(int.Parse(packageID.Text), packageName.Text);
        buttonPackageSave.IsEnabled = false;
    }
}
}
