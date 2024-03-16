using HD.Domain.Departments.Entities;

namespace HD.Domain.Departments.Interfaces;

public interface IDepartmentService : IDisposable
{
    Task Add(Department entity);
    Task Update(Department entity);
    Task Delete(Guid id);
}
