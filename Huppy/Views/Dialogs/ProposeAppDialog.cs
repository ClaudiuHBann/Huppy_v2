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

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Huppy.Views.Dialogs
{
public class ProposeAppDialog : Dialog
{
    public class Context
    (CategoryModel appCategory, string appName, byte[] appImageRaw)
    {
        public CategoryModel AppCategory { get; set; } = appCategory;
        public string AppName { get; set; } = appName;
        public byte[] AppImageRaw { get; set; } = appImageRaw;
    }

    private readonly Visual? _root = null;

    private readonly List<CategoryModel> _categoryModels;

    private MemoryStream _appIconRaw = new();

    private const string _header = "Propose App";
    private const string _headerSub = "Propose an app:";

    private readonly ComboBox _appCategory = new();
    private readonly TextBox _appName = new();
    private readonly Button _appIconFind = new();
    private readonly Avalonia.Controls.Image _appIcon = new();

    private const int _appIconSize = 256; // 256x256 WEBP

    private static FilePickerFileType _imageFileType =
        new("All Images") { Patterns = new[] { "*.png", "*.jpg", "*.jpeg", "*.webp" },
                            AppleUniformTypeIdentifiers = new[] { "public.image" },
                            MimeTypes = new[] { "image/png", "image/jpeg", "image/webp" } };

    public ProposeAppDialog(Visual? root, List<CategoryModel> categoryModels) : base(root, _header, _headerSub)
    {
        _root = root;
        _categoryModels = categoryModels;

        _appCategory.ItemsSource = categoryModels.Select(categoryModel => categoryModel.Category.Name);
        var categoryModelOthers = categoryModels.First(cm => cm.Category.Id == CategoryModel.CategoryOtherIndex);
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

        using var image = await SixLabors.ImageSharp.Image.LoadAsync(files[0].Path.LocalPath);
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

        Grid grid = new() { RowDefinitions = new("Auto, 10, Auto, 10, Auto, 10, Auto") };

        grid.Children.Add(gridAppCategory);
        grid.Children.Add(gridAppName);
        Grid.SetRow(gridAppName, 2);

        grid.Children.Add(_appIconFind);
        Grid.SetRow(_appIconFind, 4);

        // text that shows until an icon is loaded
        var text =
            new TextBlock() { Text = "Your Icon Will Be Shown Here", VerticalAlignment = VerticalAlignment.Center,
                              HorizontalAlignment = HorizontalAlignment.Center };
        grid.Children.Add(text);
        Grid.SetRow(text, 6);

        grid.Children.Add(_appIcon);
        Grid.SetRow(_appIcon, 6);

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
    
            return new(_categoryModels[_appCategory.SelectedIndex], _appName.Text ?? "", _appIconRaw.ToArray());
        }
// clang-format on
}
}
