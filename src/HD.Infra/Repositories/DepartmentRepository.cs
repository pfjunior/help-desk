using HD.Domain.Departments.Entities;
using HD.Domain.Departments.Interfaces;
using HD.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace HD.Infra.Repositories;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(ApplicationContext context) : base(context) { }

    public async Task<Department> GetByCode(string code) => await Context.Departments.AsNoTrackingWithIdentityResolution().Include(p => p.Employees).FirstOrDefaultAsync(p => p.Code.ToLower() == code.ToLower());
}
