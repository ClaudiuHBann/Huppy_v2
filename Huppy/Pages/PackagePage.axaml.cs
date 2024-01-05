using System.Linq;
using System.Collections.Specialized;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

using Huppy.Models;
using Huppy.ViewModels;
using Huppy.Views.Dialogs;

using Shared.Models;

namespace Huppy.Pages
{
public partial class PackageView : UserControl
{
    private int _packageCurrentID =>
        packageID.Content != null && int.TryParse(packageID.Content as string, out int id) ? id : 0;

    public PackageView()
    {
        InitializeComponent();
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel)
        {
            return;
        }

        packageViewModel.Apps.CollectionChanged += OnCollectionChangedApps;
        packageID.PropertyChanged += (s, e) => EnableButtonPackageEdit();
    }

    private void OnCollectionChangedApps(object? sender, NotifyCollectionChangedEventArgs e)
    {
        EnableButtonPackageSave(true);
        EnableButtonPackageCreate();
        EnableButtonPackageClear();
        EnableButtonPackageEdit();
    }

    private void EnableButtonPackageClear()
    {
        if (DataContext is not PackageViewModel packageViewModel)
        {
            return;
        }

        // can clear if at least an app is in the package
        // can clear if a package was create and it is in edit mode
        buttonPackageClear.IsEnabled = packageViewModel.Apps.Count > 0 || _packageCurrentID != 0;
    }

    private void EnableButtonPackageEdit()
    {
        // can edit if there is a package active
        buttonPackageEdit.IsEnabled = _packageCurrentID != 0;
    }

    private void EnableButtonPackageCreate()
    {
        if (DataContext is not PackageViewModel packageViewModel)
        {
            return;
        }

        // can create if there isn't a package active and at least an app in the package
        buttonPackageCreate.IsEnabled = packageViewModel.Apps.Count > 0 && _packageCurrentID == 0;
    }

    // TODO: if operations are done on apps and they end up the same we dont need to enable save
    // same with the package name
    private void EnableButtonPackageSave(bool enable)
    {
        // can save if something in the apps has changed
        // can save if the name has changed
        // there must be a package active
        buttonPackageSave.IsEnabled = _packageCurrentID != 0 && enable;
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

    private async void OnClickButtonLoadPackage(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel)
        {
            return;
        }

        PackageLoadDialog dialog = new(this);
        var resultDialog = await dialog.Show();
        if (resultDialog == null)
        {
            return;
        }

        var packageEntity = new PackageEntity() { Id = resultDialog.Id ?? -1, Name = resultDialog.Name ?? "" };

        var resultRequest = await packageViewModel.PackageLoad(packageEntity);
        if (resultRequest == null)
        {
            return;
        }

        PackageReset();
        PackageLoad(resultRequest);
    }

    private async void OnClickButtonCreatePackage(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel ||
            packageName.Content is not string packageNameContent)
        {
            return;
        }

        var apps = packageViewModel.Apps.Select(app => app.App.Id).ToArray();
        var packageEntity = await packageViewModel.PackageCreate(new() { Apps = apps, Name = packageNameContent });
        if (packageEntity == null)
        {
            return;
        }

        packageID.Content = packageEntity.Id.ToString();
        packageName.Content = packageEntity.Name;

        EnableButtonPackageCreate();
        EnableButtonPackageClear();
        EnableButtonPackageEdit();
    }

    private async void OnClickButtonEdit(object? sender, RoutedEventArgs e)
    {
        if (packageName.Content is not string packageNameContent)
        {
            return;
        }

        EditPackageDialog.Context context = new(packageNameContent);
        EditPackageDialog dialog = new(VisualRoot as Visual, context);
        var result = await dialog.Show();
        if (result == null)
        {
            return;
        }

        if (packageNameContent != result.PackageName)
        {
            EnableButtonPackageSave(true);
        }
        packageName.Content = result.PackageName;
    }

    private void OnClickButtonClear(object? sender, RoutedEventArgs e)
    {
        PackageReset();
        // updates buttons automatically in the Apps OnCollectionChanged event
    }

    private void PackageReset()
    {
        if (DataContext is not PackageViewModel packageViewModel)
        {
            return;
        }

        packageID.Content = PackageViewModel.PackageIDDefault;
        packageName.Content = PackageViewModel.PackageNameDefault;

        packageViewModel.PackageClear();
    }

    private void PackageLoad(PackageEntity packageEntity)
    {
        if (DataContext is not PackageViewModel packageViewModel)
        {
            return;
        }

        packageID.Content = packageEntity.Id.ToString();
        packageName.Content = packageEntity.Name;

        packageViewModel.PackageLoad(packageEntity.Apps);
    }

    private async void OnClickButtonSave(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not PackageViewModel packageViewModel || packageID.Content is null ||
            packageName.Content is not string packageNameContent)
        {
            return;
        }

        var apps = packageViewModel.Apps.Select(app => app.App.Id).ToArray();
        var packageEntity = new PackageEntity() { Id = _packageCurrentID, Apps = apps, Name = packageNameContent };

        var updated = await packageViewModel.PackageUpdate(packageEntity);
        if (updated == null || updated == false)
        {
            return;
        }

        EnableButtonPackageSave(false);
    }

    private void OnClickButtonPackageIDName(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button || DataContext is not PackageViewModel packageViewModel)
        {
            return;
        }

        switch (button.Name)
        {
        case "packageID":
            packageViewModel.ClipboardSaveText(packageID.Content as string);
            break;

        case "packageName":
            packageViewModel.ClipboardSaveText(packageName.Content as string);
            break;
        }
    }
}
}
