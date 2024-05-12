using HD.Departments.Api.Domain.Entities;

namespace HD.Departments.Api.Domain.Interfaces;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Department> GetByIdAsync(Guid id);
    Task<Department> GetByCodeAsync(string code);

    Task AddAsync(Department department);
    void Update(Department department);
    void Delete(Department department);
}
