using HD.Domain.Users.Entities;

namespace HD.Domain.Users.Interfaces;

public interface IUserService : IDisposable
{
    Task Add(User entity);
    Task Update(User entity);
    Task Delete(Guid id);
}