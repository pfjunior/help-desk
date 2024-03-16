using HD.Domain.Core.Entities;
using HD.Domain.Tickets.Enums;
using HD.Domain.Tickets.ValueObjects;
using Type = HD.Domain.Tickets.Enums.Type;

namespace HD.Domain.Tickets.Entities;

public class Ticket : Entity
{
    public Ticket() => _comments = new List<Comment>();

    public Ticket(string title, string description, Type type)
    {
        Title = title;
        Description = description;
        Type = type;
        Status = Status.Pending;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Solution { get; private set; }
    public Type Type { get; private set; }
    public Status Status { get; private set; }
    public Priority Priority { get; private set; }
    public DateTime CreatedIn { get; private set; }
    public DateTime? CompletedIn { get; private set; }

    private readonly List<Comment> _comments;
    public IReadOnlyCollection<Comment> Comments => _comments;

    public Guid UserId { get; private set; }


    #region Methods
    public void AddComments(Comment comment) => _comments.Add(comment);

    public void AddSolution(string solution)
    {
        Solution = solution;
        CompletedIn = DateTime.Now;
    }

    public void SetType(Type type) => Type = type;

    public void SetStatus(Status status) => Status = status;

    public void SetPriority(Priority priority) => Priority = priority;
    #endregion
}
