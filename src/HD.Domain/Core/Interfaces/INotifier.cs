using HD.Domain.Core.Notifications;

namespace HD.Domain.Core.Interfaces;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
}
