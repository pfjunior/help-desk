namespace HD.Tickets.Domain.Entities;

public class Comment
{
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}
