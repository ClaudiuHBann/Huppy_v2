using Avalonia.Controls;
using Avalonia.Interactivity;

using Huppy.Services;
using Huppy.Utilities;

namespace Huppy.Views
{
public partial class CreateEditView : UserControl
{
    public CreateEditView()
    {
        InitializeComponent();
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (DI.Services.GetService(typeof(NotificationService)) is not NotificationService notificationService ||
            DI.Services.GetService(typeof(ClipboardService)) is not ClipboardService clipboardService)
        {
            return;
        }

        // TODO: can we fix this workaround for the MainWindow being null when the services are created?
        notificationService.Initialize();
        clipboardService.Initialize();
    }
}
}
