namespace HD.Domain.Tickets.ValueObjects;

public class Comment
{
    public Comment(string description, Guid userId)
    {
        Description = description;
        CreatedIn = DateTime.Now;
        UserId = userId;
    }

    public string Description { get; private set; }
    public DateTime CreatedIn { get; private set; }

    public Guid UserId { get; private set; }
}
