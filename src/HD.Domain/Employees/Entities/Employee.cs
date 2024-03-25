using HD.Domain.Core.Entities;
using HD.Domain.Departments.Entities;

namespace HD.Domain.Employees.Entities;

public class Employee : Entity
{
    public Employee() { }

    public string Registration { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Extension { get; set; }

    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }

    public override string ToString() => $"{FirstName} {LastName}";
}
