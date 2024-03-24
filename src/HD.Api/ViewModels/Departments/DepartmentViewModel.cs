using HD.Api.ViewModels.Users;

namespace HD.Api.ViewModels.Departments;

public class DepartmentViewModel
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public IEnumerable<UserViewModel> Users { get; set; }
}
