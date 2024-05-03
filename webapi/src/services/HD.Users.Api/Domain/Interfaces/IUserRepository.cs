using HD.Core.Data;
using HD.Users.Api.Domain.Entities;
using System.Linq.Expressions;

namespace HD.Users.Api.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(Guid id);
    Task<IEnumerable<User>> Search(Expression<Func<User, bool>> predicate);

    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}
