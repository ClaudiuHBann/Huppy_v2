using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;

using System.Threading.Tasks;
using System.Collections.ObjectModel;

using FluentAvalonia.UI.Controls;

using Shared.Models;

namespace Huppy.Views.Dialogs
{
public class ProposeAppDialog : Dialog
{
    private const string _header = "Propose App";
    private const string _headerSub = "Propose an app:";

    private readonly ComboBox _appCategory = new();
    private readonly TextBox _appName = new();
    private readonly Button _appIcon = new();

    private const int _appIconMaxSize = 512 * 512 * 4; // 512x512 RGBA

    public ProposeAppDialog(Visual? root) : base(root, _header, _headerSub)
    {
        Buttons.Add(TaskDialogButton.OKButton);
        Buttons.Add(TaskDialogButton.CancelButton);
    }

    protected override Control CreateContent()
    {
        return new();
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
