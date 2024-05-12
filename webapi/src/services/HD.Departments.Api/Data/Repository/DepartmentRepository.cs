using HD.Departments.Api.Domain.Entities;
using HD.Departments.Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HD.Departments.Api.Data.Repository;

public class DepartmentRepository : IDepartmentRepository, IDisposable
{
    private readonly DepartmentContext _context;

    public DepartmentRepository(DepartmentContext context) => _context = context;


    public async Task<IEnumerable<Department>> GetAllAsync() => await _context.Departments.ToListAsync();

    public async Task<Department> GetByIdAsync(Guid id) => await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Department> GetByCodeAsync(string code) => await _context.Departments.FirstOrDefaultAsync(x => x.Code.ToUpper() == code);


    public async Task AddAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
    }

    public void Update(Department department)
    {
        _context.Departments.Update(department);
        _context.SaveChanges();
    }

    public void Delete(Department department)
    {
        _context.Departments.Remove(department);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) => _context?.Dispose();
}
