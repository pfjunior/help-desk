using HD.Domain.Core.Interfaces;
using HD.Domain.Departments.Entities;

namespace HD.Domain.Departments.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department> GetByCode(string code);
    Task<IEnumerable<Department>> GetDepartmentsUsers();
}
