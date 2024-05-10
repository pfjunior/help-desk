using HD.Core.DomainObjects;
using HD.Tickets.Domain.Enum;

namespace HD.Tickets.Domain.Entities;

public class Ticket : Entity, IAggregateRoot
{
    public int TicketNumber { get; private set; }
    public string Subject { get; private set; }
    public string Description { get; private set; }
    public string? Solution { get; private set; }
    public TicketType Type { get; private set; }
    public TicketStatus Status { get; private set; }
    public TicketPriority Priority { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; }

    private readonly List<Comment>? _comments;
    public IReadOnlyCollection<Comment>? Comments => _comments;


    protected Ticket() { }

    public Ticket(Guid userId, string subject, string description, TicketType type, string userName, string email, string departmentName)
    {
        UserId = userId;
        Subject = subject;
        Description = description;
        Type = type;
        Status = TicketStatus.Pending;
        User = new User { UserName = userName, Email = email, DepartmentName = departmentName };
    }

    public void Update(string subject, string description)
    {
        Subject = subject;
        Description = description;
    }

    public void SetType(TicketType type) => Type = type;

    public void SetStatus(TicketStatus status) => Status = status;

    public void SetPriority(TicketPriority priority) => Priority = priority;

    public void AddComment(Comment comment) => _comments?.Add(comment);
}
