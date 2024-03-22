using HD.Domain.Users.Entities;

namespace HD.Api.ViewModels.Departments;

public class DepartmentUserViewModel
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<User> Users { get; set; }
}
