using System;
using System.Threading.Tasks;

using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Controls.ApplicationLifetimes;

namespace Huppy.Services
{
public class ClipboardService
{
    private IClipboard? _clipboard = null;

    public void Initialize()
    {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _clipboard = desktop.MainWindow?.Clipboard;
        }
        else if (Avalonia.Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            _clipboard = TopLevel.GetTopLevel(singleViewPlatform.MainView)?.Clipboard;
        }
        else
        {
            throw new NotImplementedException("ClipboardService is only for Desktop and Browser!");
        }
    }

    public async Task SetText(string? text)
    {
        if (_clipboard == null)
        {
            return;
        }

        await _clipboard.SetTextAsync(text);
    }
}
}
