namespace HD.Tickets.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }

    public Guid TicketId { get; set; }
    public Ticket Ticket { get; set; }
}
