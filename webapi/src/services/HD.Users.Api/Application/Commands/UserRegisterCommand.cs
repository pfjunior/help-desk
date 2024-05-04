using FluentValidation;
using HD.Core.DomainObjects;
using HD.Core.Messages;

namespace HD.Users.Api.Application.Commands;

public class UserRegisterCommand : Command
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? PhoneNumber { get; set; }
    public string? Extension { get; private set; }

    public string DepartmentCode { get; private set; }
    public string DepartmentName { get; private set; }


    public UserRegisterCommand(Guid id, string firstName, string lastName, string email, string departmentCode, string departmentName)
    {
        AggregateId = id;
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DepartmentCode = departmentCode;
        DepartmentName = departmentName;
    }


    public override bool IsValid()
    {
        ValidationResult = new UserRegisterValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UserRegisterValidation : AbstractValidator<UserRegisterCommand>
{
    public UserRegisterValidation()
    {
        RuleFor(u => u.Id).NotEqual(Guid.Empty).WithMessage("User Id invalid");
        RuleFor(u => u.FirstName).NotEmpty().WithMessage("Fist Name is required");
        RuleFor(u => u.LastName).NotEmpty().WithMessage("Last Name is required");
        RuleFor(u => u.Email).Must(HaveValidEmail).WithMessage("E-mail invalid");
        RuleFor(u => u.DepartmentCode)
            .NotEmpty().WithMessage("Department Code is required")
            .Length(2, 8).WithMessage("Department Code must have between 2 and 8 characters");
        RuleFor(u => u.DepartmentName)
            .NotEmpty().WithMessage("Department Name is required")
            .Length(2, 60).WithMessage("Department Name must have between 2 and 60 characters");
    }

    protected static bool HaveValidEmail(string email) => Email.Validate(email);
}
