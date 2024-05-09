using HD.Core.Data;
using HD.Tickets.Domain.Entities;
using System.Linq.Expressions;

namespace HD.Tickets.Domain.Interfaces;

public interface ITicketRepository : IRepository<Ticket>
{
    #region Ticket
    Task<Ticket> GetById(Guid id);
    Task<IEnumerable<Ticket>> GetAll();
    Task<IEnumerable<Ticket>> Search(Expression<Func<User, bool>> predicate);

    Task AddAsync(Ticket ticket);
    void Update(Ticket ticket);
    void Delete(Ticket ticket);
    #endregion

    #region Comment
    Task<IEnumerable<Comment>> GetCommentsByTicketId(Guid ticketId);

    Task AddCommentAsync(Comment comment);
    #endregion
}
