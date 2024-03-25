using HD.Domain.Core.Interfaces;
using HD.Domain.Employees.Entities;

namespace HD.Domain.Employees.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee> GetByRegistration(string registration);
    Task<IEnumerable<Employee>> GetEmployeesDepartments();
}
