using System;
using System.Collections.ObjectModel;

using Avalonia.Controls.Notifications;
using Avalonia.Controls.ApplicationLifetimes;

namespace Huppy.Utilities
{
public class NotificationService
{
    private readonly WindowNotificationManager? _manager = null;

    private ObservableCollection<INotification>? _cache = [];

    public NotificationService()
    {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _manager = new(desktop.MainWindow);
        }
        else
        {
            throw new NotImplementedException();
        }

        _manager.Position = NotificationPosition.BottomRight;
        _manager.MaxItems = 2;

        _manager.TemplateApplied += (sender, args) =>
        {
            if (_cache == null)
            {
                return;
            }

            foreach (var notification in _cache)
            {
                _manager?.Show(notification);
            }

            _cache.Clear();
            _cache = null;
        };
    }

    private void Notify(Notification notification)
    {
        if (_cache != null)
        {
            _cache.Add(notification);
        }
        else
        {
            _manager?.Show(notification);
        }
    }

    public void NotifyI(string message) => Notify(new("Huppy", message, NotificationType.Information));
    public void NotifyE(string message) => Notify(new("Huppy", message, NotificationType.Error));
}
}
