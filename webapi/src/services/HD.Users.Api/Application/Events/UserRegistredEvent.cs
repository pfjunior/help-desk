using HD.Core.Messages;

namespace HD.Users.Api.Application.Events;

public class UserRegistredEvent : Event
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string DepartmentCode { get; private set; }
    public string DepartmentName { get; private set; }

    public UserRegistredEvent(Guid id, string firstName, string lastName, string email, string departmentCode, string departmentName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DepartmentCode = departmentCode;
        DepartmentName = departmentName;
    }
}
