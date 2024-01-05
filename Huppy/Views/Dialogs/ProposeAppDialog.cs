using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;

using System.IO;
using System.Threading.Tasks;

using FluentAvalonia.UI.Controls;

using Shared.Models;

namespace Huppy.Views.Dialogs
{
public class ProposeAppDialog : Dialog
{
    private readonly Visual? _root = null;

    private const string _header = "Propose App";
    private const string _headerSub = "Propose an app:";

    private readonly ComboBox _appCategory = new();
    private readonly TextBox _appName = new();
    private readonly Button _appIconFind = new();
    private readonly Image _appIcon = new();

    private const int _appIconSize = 256; // 256x256 WEBP

    public ProposeAppDialog(Visual? root) : base(root, _header, _headerSub)
    {
        _root = root;

        _appIconFind.Content = "Choose App Icon";
        _appIconFind.Click += OnClickButtonAppIconFind;
        _appIconFind.HorizontalAlignment = HorizontalAlignment.Center;

        _appIcon.Width = _appIconSize;
        _appIcon.Height = _appIconSize;

        Buttons.Add(TaskDialogButton.OKButton);
        Buttons.Add(TaskDialogButton.CancelButton);
    }

    private async void OnClickButtonAppIconFind(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(_root);
        if (topLevel == null)
        {
            return;
        }

        var options = new FilePickerOpenOptions { Title = "Choose App Icon", AllowMultiple = false };
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(options);

        if (files.Count == 0)
        {
            return;
        }

        var imageRaw = await File.ReadAllBytesAsync(files[0].Path.LocalPath);
        var image = new Bitmap(new MemoryStream(imageRaw));
        _appIcon.Source = image.CreateScaledBitmap(new(_appIconSize, _appIconSize));
    }

    protected override Control CreateContent()
    {
        Grid gridAppCategory = new() { ColumnDefinitions = new("Auto, 5, *") };

        gridAppCategory.Children.Add(
            new TextBlock() { Text = "App category:", VerticalAlignment = VerticalAlignment.Center });
        gridAppCategory.Children.Add(_appCategory);
        Grid.SetColumn(_appCategory, 2);

        Grid gridAppName = new() { ColumnDefinitions = new("Auto, 5, *") };

        gridAppName.Children.Add(new TextBlock() { Text = "App name:", VerticalAlignment = VerticalAlignment.Center });
        gridAppName.Children.Add(_appName);
        Grid.SetColumn(_appName, 2);

        Grid grid = new() { RowDefinitions = new("Auto, 10, Auto, 10, Auto, 10, Auto") };

        grid.Children.Add(gridAppCategory);
        grid.Children.Add(gridAppName);
        Grid.SetRow(gridAppName, 2);

        grid.Children.Add(_appIconFind);
        Grid.SetRow(_appIconFind, 4);

        // text that shows until an icon is loaded
        var text = new TextBlock() { Text = "Your Icon", VerticalAlignment = VerticalAlignment.Center,
                                     HorizontalAlignment = HorizontalAlignment.Center };
        grid.Children.Add(text);
        Grid.SetRow(text, 6);

        grid.Children.Add(_appIcon);
        Grid.SetRow(_appIcon, 6);

        return grid;
    }

    // clang-format off
        public new async Task<PackageEntity?> Show()
        {
            var button = await base.Show();
            if (button is null || (TaskDialogStandardResult)button != TaskDialogStandardResult.OK)
            {
                return null;
            }

            return null;
        }
// clang-format on
}
}
