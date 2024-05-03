namespace HD.Core.Data;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
