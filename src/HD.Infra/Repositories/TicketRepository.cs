using HD.Domain.Tickets.Entities;
using HD.Domain.Tickets.Interfaces;
using HD.Infra.Context;

namespace HD.Infra.Repositories;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(ApplicationContext context) : base(context) { }
}
