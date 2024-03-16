using HD.Domain.Tickets.Entities;

namespace HD.Domain.Tickets.Interfaces;

public interface ITicketService : IDisposable
{
    Task Add(Ticket entity);
    Task Update(Ticket entity);
    Task Delete(Guid id);
}
