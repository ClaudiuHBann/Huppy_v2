using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Huppy.Models;
using Huppy.Utilities;
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
        if (sender is not Button button || button.DataContext is not AppModel app ||
            DataContext is not PackageViewModel packageViewModel)
        {
            return;
        }

        app.IsChecked = !app.IsChecked;
        packageViewModel.Apps.Remove(app);
    }

    private void OnClickButtonCreatePackage(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel || packageID.Text is null || packageName.Text is null)
        {
            return;
        }

        var idAndName = packageViewModel.PackageCreate(packageName.Text);
        if (idAndName == null)
        {
            Notifications.NotifyE("Could not create a package!");
            return;
        }

        packageID.Text = idAndName.Value.id.ToString();
        packageName.Text = idAndName.Value.name;
        buttonPackageEdit.IsEnabled = true;
    }

    private async void OnClickButtonEdit(object? sender, RoutedEventArgs e)
    {
        if (packageID.Text is null || packageName.Text is null)
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
            Notifications.NotifyE("Could not update the package!");
            return;
        }

        if (!packageViewModel.PackageUpdate(int.Parse(packageID.Text), packageName.Text))
        {
            return;
        }

        buttonPackageSave.IsEnabled = false;
    }
}
}
