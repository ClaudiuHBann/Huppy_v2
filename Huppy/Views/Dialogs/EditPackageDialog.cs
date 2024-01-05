using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;

using System.Threading.Tasks;

using FluentAvalonia.UI.Controls;

namespace Huppy.Views.Dialogs
{
public class EditPackageDialog : Dialog
{
    public class Context
    (string packageName)
    {
        public string PackageName { get; set; } = packageName;
    }

    private const string _header = "Edit Package";
    private const string _headerSub = "Choose a unique name:";

    private readonly Context _context;

    private readonly TextBox _packageName = new();
    private const int _packageNameMaxLength = 36; // 36 for a string GUID

    public EditPackageDialog(Visual? root, Context context) : base(root, _header, _headerSub)
    {
        _context = context;

        _packageName.Text = _context.PackageName;
        _packageName.TextChanged += OnTextBoxTextChangedPackageName;

        Buttons.Add(TaskDialogButton.OKButton);
        Buttons.Add(TaskDialogButton.CancelButton);
    }

    private void OnTextBoxTextChangedPackageName(object? sender, TextChangedEventArgs e)
    {
        if (_packageName.Text?.Length > _packageNameMaxLength)
        {
            _packageName.Text = _packageName.Text[.._packageNameMaxLength];
        }
    }

    protected override Control CreateContent()
    {
        var grid = new Grid() { ColumnDefinitions = new("Auto, 5, *") };

        grid.Children.Add(new TextBlock() { Text = "Package name:", VerticalAlignment = VerticalAlignment.Center });
        grid.Children.Add(_packageName);
        Grid.SetColumn(_packageName, 2);

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

        return new(_packageName.Text ?? "");
    }
// clang-format on
}
}
