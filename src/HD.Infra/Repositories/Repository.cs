using HD.Domain.Core.Entities;
using HD.Domain.Core.Interfaces;
using HD.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HD.Infra.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly ApplicationContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(ApplicationContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetById(Guid id) => await DbSet.FindAsync(id);

    public virtual async Task<IEnumerable<TEntity>> GetAll() => await DbSet.ToListAsync();

    public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate) => await DbSet.AsNoTrackingWithIdentityResolution().Where(predicate).ToListAsync();

    public virtual async Task Add(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveChanges();
    }

    public virtual async Task Update(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveChanges();
    }

    public virtual async Task Delete(Guid id)
    {
        DbSet.Remove(new TEntity { Id = id });
        await SaveChanges();
    }

    public async Task<int> SaveChanges() => await Context.SaveChangesAsync();

    public void Dispose() => Context?.Dispose();
}
