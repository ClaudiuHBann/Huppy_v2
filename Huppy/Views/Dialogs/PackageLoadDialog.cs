using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;

using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using FluentAvalonia.UI.Controls;

namespace Huppy.Views.Dialogs
{
public class PackageLoadDialog : Dialog
{
    public class Context
    {
        public Guid? Id { get; set; } = null;
        public string? Name { get; set; } = null;

        public Context(Guid id) => Id = id;
        public Context(string name) => Name = name;
    }

    private const string _header = "Load Package";
    private const string _headerSub = "Find your package by it's name or ID:";

    private readonly ComboBox _nameOrID = new();
    private readonly TextBlock _description = new();
    private readonly TextBox _packageName = new();
    private readonly TextBox _packageID = new();
    private readonly ContentControl _content = new();

    private const int _packageIDNameMaxLength = 36; // 36 for a string GUID

    public PackageLoadDialog(Visual? root) : base(root, _header, _headerSub)
    {
        _nameOrID.ItemsSource = new ObservableCollection<string>() { "Name", "ID" };
        _nameOrID.SelectionChanged += OnComboBoxSelectionChangedNameOrID;
        _nameOrID.SelectedIndex = 0;

        _description.VerticalAlignment = VerticalAlignment.Center;

        _packageName.TextChanged += OnTextBoxTextChangedPackageName;
        _packageID.TextChanged += OnTextBoxTextChangedPackageName;

        Buttons.Add(TaskDialogButton.OKButton);
        Buttons.Add(TaskDialogButton.CancelButton);
    }

    private void OnTextBoxTextChangedPackageName(object? sender, TextChangedEventArgs e)
    {
        if (_packageName.Text?.Length > _packageIDNameMaxLength)
        {
            _packageName.Text = _packageName.Text[.._packageIDNameMaxLength];
        }
    }

    private void OnComboBoxSelectionChangedNameOrID(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count <= 0)
        {
            return;
        }

        switch ((string?)e.AddedItems[0])
        {
        case "Name": {
            _content.Content = _packageName;
            _description.Text = "Package Name:";
        }
        break;

        case "ID": {
            _content.Content = _packageID;
            _description.Text = "Package ID:";
        }
        break;
        }
    }

    protected override Control CreateContent()
    {
        var gridNameOrID = new Grid() { ColumnDefinitions = new("Auto, 5, *") };

        gridNameOrID.Children.Add(new TextBlock() { Text = "Find by:", VerticalAlignment = VerticalAlignment.Center });
        gridNameOrID.Children.Add(_nameOrID);
        Grid.SetColumn(_nameOrID, 2);

        var gridContent = new Grid() { ColumnDefinitions = new("Auto, 5, *") };

        gridContent.Children.Add(_description);
        gridContent.Children.Add(_content);
        Grid.SetColumn(_content, 2);

        var stackPanel = new StackPanel() { Spacing = 10 };

        stackPanel.Children.Add(gridNameOrID);
        stackPanel.Children.Add(gridContent);

        return stackPanel;
    }

    // clang-format off
        public new async Task<Context?> Show()
        {
            var button = await base.Show();
            if (button is null || (TaskDialogStandardResult)button != TaskDialogStandardResult.OK)
            {
                return null;
            }

            return (string?)_nameOrID.SelectedValue switch
            {
                "Name" => new(_packageName.Text ?? ""),
                "ID" => new(Guid.TryParse(_packageID.Text, out Guid id) ? id : Guid.Empty),
                _ => null,
            };
        }
// clang-format on
}
}
