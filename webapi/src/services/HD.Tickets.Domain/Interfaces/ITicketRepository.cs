using HD.Core.Data;
using HD.Tickets.Domain.Entities;
using System.Linq.Expressions;

namespace HD.Tickets.Domain.Interfaces;

public interface ITicketRepository : IRepository<Ticket>
{
    #region Ticket
    Task<Ticket> GetByIdAsync(Guid id);
    Task<Ticket> GetTicketWithCommentsById(Guid id);
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task<IEnumerable<Ticket>> SearchAsync(Expression<Func<Ticket, bool>> predicate);

    Task AddAsync(Ticket ticket);
    void Update(Ticket ticket);
    void Delete(Ticket ticket);
    #endregion

    #region Comment
    Task AddCommentAsync(Comment comment);
    #endregion
}
