using System.Collections.ObjectModel;

using Avalonia.Controls;
using Avalonia.Controls.Notifications;

namespace Huppy.Utilities
{
public static class Notifications
{
    private static WindowNotificationManager? _manager = null;

    private static ObservableCollection<INotification>? _cache = [];

    public static void Initialize(TopLevel topLevel)
    {
        _manager = new(topLevel) { Position = NotificationPosition.BottomRight, MaxItems = 2 };
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

    private static void Notify(Notification notification)
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

    public static void NotifyI(string message) => Notify(new("Huppy", message, NotificationType.Information));
    public static void NotifyE(string message) => Notify(new("Huppy", message, NotificationType.Error));
}
}
