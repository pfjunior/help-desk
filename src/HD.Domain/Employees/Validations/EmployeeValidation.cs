using FluentValidation;
using HD.Domain.Employees.Entities;

namespace HD.Domain.Employees.Validations;

public class EmployeeValidation : AbstractValidator<Employee>
{
    public EmployeeValidation()
    {
        RuleFor(p => p.Registration)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
            .Length(2, 8).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres");

        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
            .Length(2, 30).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres");

        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("O campo {PropertyName} deve ser preenchido")
            .Length(2, 30).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres");
    }
}
