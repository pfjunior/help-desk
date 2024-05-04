using HD.Core.DomainObjects;

namespace HD.Users.Api.Domain.Entities;

public class User : Entity, IAggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public string? PhoneNumber { get; set; }
    public string? Extension { get; private set; }
    public bool Active { get; private set; }

    public Department Department { get; private set; }


    protected User() { }

    public User(Guid id, string firstName, string lastName, string email, string departmentCode, string departmentName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = new Email(email);
        Active = true;
        Department = new Department { Code = departmentCode, Name = departmentName };
    }


    public void UpdateBasicInfo(string fistName, string lastName, string email)
    {
        FirstName = fistName;
        LastName = lastName;
        Email = new Email(email);
    }

    public void SetPhoneNumber(string? phoneNumber, string? extension)
    {
        PhoneNumber = phoneNumber;
        Extension = extension;
    }

    public void SetDepartment(Department department) => Department = department;

    public void Activate() => Active = true;
    public void Deactivate() => Active = false;
}
