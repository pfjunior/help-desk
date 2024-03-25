using HD.Domain.Employees.Entities;

namespace HD.Domain.Employees.Interfaces;

public interface IEmployeeService : IDisposable
{
    Task Add(Employee entity);
    Task Update(Employee entity);
    Task Delete(Guid id);
}