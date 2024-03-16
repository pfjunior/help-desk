using HD.Domain.Core.Entities;
using HD.Domain.Departments.Entities;

namespace HD.Domain.Users.Entities;

public class User : Entity
{
    protected User() { }

    public User(string registration, string firstName, string lastName, string? extension)
    {
        Registration = registration;
        FirstName = firstName;
        LastName = lastName;
        Extension = extension;
    }

    public string Registration { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? Extension { get; private set; }

    public Guid DepartmentId { get; private set; }
    public Department Department { get; private set; }
}
