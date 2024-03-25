namespace HD.Api.ViewModels.Employees;

public class EmployeeViewModel
{
    public Guid Id { get; set; }
    public string Registration { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Extension { get; set; }
    public string? DepartmentName { get; set; }
}
