using HD.Domain.Core.Entities;
using HD.Domain.Users.Entities;

namespace HD.Domain.Departments.Entities;

public class Department : Entity
{
    public Department() => _users = new();

    public string Code { get; set; }
    public string Name { get; set; }

    private List<User> _users;
    public IReadOnlyCollection<User> Users => _users;

    public Guid? UserId { get; set; }
}
