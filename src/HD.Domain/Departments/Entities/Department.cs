using HD.Domain.Core.Entities;
using HD.Domain.Users.Entities;

namespace HD.Domain.Departments.Entities;

public class Department : Entity
{
    public Department() => _users = new();

    public Department(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public string Code { get; private set; }
    public string Name { get; private set; }

    private List<User> _users;
    public IReadOnlyCollection<User> Users => _users;

    public Guid? UserId { get; private set; }

    public void SetId(Guid id) => Id = id;

    public void SetCode(string code) => Code = code;

    public void SetName(string name) => Name = name;
}
