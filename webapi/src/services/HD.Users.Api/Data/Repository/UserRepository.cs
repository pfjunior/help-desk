using HD.Core.Data;
using HD.Users.Api.Domain.Entities;
using HD.Users.Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HD.Users.Api.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserContext _context;


    public UserRepository(UserContext context) => _context = context;


    public IUnitOfWork UnitOfWork => _context;


    public async Task<IEnumerable<User>> GetAll() => await _context.Users.ToListAsync();

    public async Task<User> GetById(Guid id) => await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<User>> Search(Expression<Func<User, bool>> predicate) => await _context.Users.Where(predicate).ToListAsync();

    public async Task AddAsync(User user) => await _context.Users.AddAsync(user);
    public async Task UpdateAsync(User user) => _context.Users.Update(user);

    public async Task DeleteAsync(User user) => _context.Users.Remove(user);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) => _context?.Dispose();
}
