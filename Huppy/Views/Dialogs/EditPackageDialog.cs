﻿using Avalonia;
using Avalonia.Controls;

using System.Threading.Tasks;

using FluentAvalonia.UI.Controls;

namespace Huppy.Views.Dialogs
{
public class EditPackageDialog
{
    public class Context
    (string packageName)
    {
        public string PackageName { get; set; } = packageName;
    }

    private readonly TaskDialog _dialog = new();
    private readonly Context _context;

    private readonly TextBox _packageName = new();
    private static readonly int _packageNameMaxLength = 36; // 36 for a string GUID

    public EditPackageDialog(Visual? root, Context context)
    {
        _dialog.XamlRoot = root;
        _context = context;

        _packageName.Text = _context.PackageName;
        _packageName.TextChanged += OnTextBoxTextChangedPackageName;

        _dialog.Header = "Edit Package";
        _dialog.SubHeader = "Choose a unique name:";
        _dialog.Content = CreateContent();

        _dialog.Buttons.Add(TaskDialogButton.OKButton);
        _dialog.Buttons.Add(TaskDialogButton.CancelButton);
    }

    private void OnTextBoxTextChangedPackageName(object? sender, TextChangedEventArgs e)
    {
        if (_packageName.Text?.Length > _packageNameMaxLength)
        {
            _packageName.Text = _packageName.Text[.._packageNameMaxLength];
        }
    }

    private Control CreateContent()
    {
        var stackPanel = new StackPanel() { Spacing = 5 };

        stackPanel.Children.Add(new TextBlock() { Text = "Package name:" });
        stackPanel.Children.Add(_packageName);

        return stackPanel;
    }

    public async Task<Context?> Show()
    {
        var button = await _dialog.ShowAsync(true);
        if (button is null || (TaskDialogStandardResult)button != TaskDialogStandardResult.OK)
        {
            return null;
        }

        return new(_packageName.Text ?? "");
    }
}
}
