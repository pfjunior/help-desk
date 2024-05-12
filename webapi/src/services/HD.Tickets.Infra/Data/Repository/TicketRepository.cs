using HD.Core.Data;
using HD.Tickets.Domain.Entities;
using HD.Tickets.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HD.Tickets.Infra.Data.Repository;

public class TicketRepository : ITicketRepository
{
    private readonly TicketContext _context;

    public TicketRepository(TicketContext context) => _context = context;


    public IUnitOfWork UnitOfWork => _context;

    #region Ticket
    public async Task<IEnumerable<Ticket>> GetAllAsync() => await _context.Tickets.ToListAsync();

    public async Task<Ticket> GetByIdAsync(Guid id) => await _context.Tickets.FindAsync(id);

    public async Task<Ticket> GetTicketWithCommentsById(Guid id) => await _context.Tickets.Include(t => t.Comments).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<IEnumerable<Ticket>> SearchAsync(Expression<Func<Ticket, bool>> predicate) => await _context.Tickets.Where(predicate).ToListAsync();

    public async Task AddAsync(Ticket ticket) => await _context.Tickets.AddAsync(ticket);

    public void Update(Ticket ticket) => _context.Tickets.Update(ticket);

    public void Delete(Ticket ticket) => _context.Tickets.Remove(ticket);
    #endregion


    #region Comment
    public async Task AddCommentAsync(Comment comment) => await _context.Comments.AddAsync(comment);
    #endregion


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) => _context?.Dispose();
}
