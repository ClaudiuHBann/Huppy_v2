using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using FluentAvalonia.UI.Controls;

using Huppy.Models;

using Shared.Models;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Huppy.Views.Dialogs
{
public class ProposeAppDialog : Dialog
{
    public class Context
    (int category, string name, byte[] imageRaw, string url)
    {
        public int Category { get; set; } = category;
        public string Name { get; set; } = name;
        public byte[] ImageRaw { get; set; } = imageRaw;
        public string Url { get; set; } = url;
    }

    private readonly Visual? _root = null;

    private readonly List<CategoryEntity> _categoryModels;

    private MemoryStream _appIconRaw = new();

    private const string _header = "Propose App";
    private const string _headerSub = "Propose an app:";

    private readonly ComboBox _appCategory = new();
    private readonly TextBox _appName = new();
    private readonly TextBox _appLink = new();
    private readonly Button _appIconFind = new();
    private readonly Avalonia.Controls.Image _appIcon = new();

    private const int _appIconSize = 256; // 256x256 WEBP

    private static FilePickerFileType _imageFileType =
        new("All Images") { Patterns = new[] { "*.png", "*.jpg", "*.jpeg", "*.webp" },
                            AppleUniformTypeIdentifiers = new[] { "public.image" },
                            MimeTypes = new[] { "image/png", "image/jpeg", "image/webp" } };

    public ProposeAppDialog(Visual? root, List<CategoryEntity> categoryModels) : base(root, _header, _headerSub)
    {
        _root = root;
        _categoryModels = categoryModels;

        _appCategory.ItemsSource = categoryModels.Select(category => category.Name);
        var categoryModelOthers = categoryModels.First(category => category.Id == CategoryModel.CategoryOtherIndex);
        _appCategory.SelectedIndex = categoryModels.IndexOf(categoryModelOthers);

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

        var options = new FilePickerOpenOptions { Title = "Choose App Icon", AllowMultiple = false,
                                                  FileTypeFilter = new[] { _imageFileType } };
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(options);

        if (files.Count == 0)
        {
            return;
        }

        // TODO: why this shit is slow AF in browser?
        // https://github.com/dotnet/designs/blob/346cd7db900a917dde7ca67fbcd6785a73935709/accepted/2023/wasm-browser-threads.md
        var stream = await files[0].OpenReadAsync();
        using var image = await SixLabors.ImageSharp.Image.LoadAsync(stream);
        if (image == null)
        {
            return;
        }

        _appIconRaw.SetLength(0); // clear stream
        image.Mutate(config => config.Resize(_appIconSize, _appIconSize));
        await image.SaveAsWebpAsync(_appIconRaw);

        _appIconRaw.Position = 0; // reset position so it can be read
        _appIcon.Source = new Bitmap(_appIconRaw);
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

        Grid gridAppLink = new() { ColumnDefinitions = new("Auto, 5, *") };

        gridAppLink.Children.Add(new TextBlock() { Text = "App link:", VerticalAlignment = VerticalAlignment.Center });
        gridAppLink.Children.Add(_appLink);
        Grid.SetColumn(_appLink, 2);

        Grid grid = new() { RowDefinitions = new("Auto, 10, Auto, 10, Auto, 10, Auto, 10, Auto") };

        grid.Children.Add(gridAppCategory);
        grid.Children.Add(gridAppName);
        Grid.SetRow(gridAppName, 2);
        grid.Children.Add(gridAppLink);
        Grid.SetRow(gridAppLink, 4);

        grid.Children.Add(_appIconFind);
        Grid.SetRow(_appIconFind, 6);

        // text that shows until an icon is loaded
        var text =
            new TextBlock() { Text = "Your Icon Will Be Shown Here", VerticalAlignment = VerticalAlignment.Center,
                              HorizontalAlignment = HorizontalAlignment.Center };
        grid.Children.Add(text);
        Grid.SetRow(text, 8);

        grid.Children.Add(_appIcon);
        Grid.SetRow(_appIcon, 8);

        return grid;
    }

    // clang-format off
    public new async Task<Context?> Show()
    {
        var button = await base.Show();
        if (button is null || (TaskDialogStandardResult)button != TaskDialogStandardResult.OK)
        {
            return null;
        }

        if (_appIcon.Source == null)
        {
            return null;
        }

        return new(
            _categoryModels[_appCategory.SelectedIndex].Id,
            _appName.Text ?? "",
            _appIconRaw.ToArray(),
            _appLink.Text ?? ""
        );
    }
// clang-format on
}
}
