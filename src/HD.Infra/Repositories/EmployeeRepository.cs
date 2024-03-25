using HD.Domain.Employees.Entities;
using HD.Domain.Employees.Interfaces;
using HD.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace HD.Infra.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationContext context) : base(context) { }

    public async Task<Employee> GetByRegistration(string registration) => await Context.Employees.FirstOrDefaultAsync(p => p.Registration == registration);

    public async Task<IEnumerable<Employee>> GetEmployeesDepartments() => await Context.Employees.AsNoTrackingWithIdentityResolution().Include(p => p.Department).OrderBy(p => p.Registration).ToListAsync();
}
