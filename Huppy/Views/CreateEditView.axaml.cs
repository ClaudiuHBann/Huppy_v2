using Avalonia.Controls;
using Avalonia.Interactivity;

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
        if (DI.Services.GetService(typeof(NotificationService)) is not NotificationService notificationService)
        {
            return;
        }

        // TODO: can we fix this workaround for the MainWindow being null when the notification service is created?
        notificationService.Initialize();
    }
}
}
