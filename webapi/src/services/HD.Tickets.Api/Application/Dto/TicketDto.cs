using HD.Tickets.Domain.Enum;

namespace HD.Tickets.Api.Application.Dto;

public class TicketDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public string? Solution { get; set; }
    public TicketType Type { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }

    public CommentDto? Comment { get; set; }
}
