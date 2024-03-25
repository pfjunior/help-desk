using HD.Api.ViewModels.Employees;

namespace HD.Api.ViewModels.Departments;

public class DepartmentViewModel
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public IEnumerable<EmployeeViewModel> Employees { get; set; }
}
