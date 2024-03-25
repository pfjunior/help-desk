using HD.Domain.Employees.Entities;
using HD.Domain.Tickets.Enums;
using HD.Domain.Tickets.ValueObjects;
using Type = HD.Domain.Tickets.Enums.Type;

namespace HD.Api.ViewModels.Tickets;

public class TicketViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Type Type { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime CreatedIn { get; set; }
    public DateTime? CompletedIn { get; set; }
    public IReadOnlyCollection<Comment> Comments { get; set; }

    public Employee User { get; set; }
}
