using HD.Domain.Core.Entities;
using HD.Domain.Employees.Entities;

namespace HD.Domain.Departments.Entities;

public class Department : Entity
{
    public Department() => _employees = new();

    public string Code { get; set; }
    public string Name { get; set; }

    private List<Employee> _employees;
    public IReadOnlyCollection<Employee> Employees => _employees;
}
