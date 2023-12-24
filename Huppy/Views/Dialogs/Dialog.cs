using System.Threading.Tasks;
using System.Collections.Generic;

using Avalonia;
using Avalonia.Controls;

using FluentAvalonia.UI.Controls;

namespace Huppy.Views.Dialogs
{
public abstract class Dialog
{
    private readonly TaskDialog _dialog = new();
    protected IList<TaskDialogButton> Buttons => _dialog.Buttons;

    protected Dialog(Visual? root, string header, string? subHeader = null)
    {
        _dialog.XamlRoot = root;

        _dialog.Header = header;
        _dialog.SubHeader = subHeader;
        _dialog.Content = CreateContent();
    }

    protected abstract Control CreateContent();

    protected async Task<object> Show()
    {
        return await _dialog.ShowAsync(true);
    }
}
}
