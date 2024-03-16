using HD.Domain.Core.Entities;
using System.Linq.Expressions;

namespace HD.Domain.Core.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<List<TEntity>> GetAll();
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);

    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(Guid id);

    Task<int> SaveChanges();
}
