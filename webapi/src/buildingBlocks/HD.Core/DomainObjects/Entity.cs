using HD.Core.Messages;

namespace HD.Core.DomainObjects;
public abstract class Entity : IAggregateRoot
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    private List<Event> _notifications;
    public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();


    protected Entity() => Id = Guid.NewGuid();


    public void AddEvent(Event eventNotification)
    {
        _notifications = _notifications ?? new List<Event>();
        _notifications.Add(eventNotification);
    }

    public void RemoveEvent(Event eventNotification) => _notifications.Remove(eventNotification);

    public void ClearEvents() => _notifications?.Clear();
}
