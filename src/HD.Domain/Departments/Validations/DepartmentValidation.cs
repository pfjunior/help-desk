using FluentValidation;
using HD.Domain.Departments.Entities;

namespace HD.Domain.Departments.Validations;

public class DepartmentValidation : AbstractValidator<Department>
{
    public DepartmentValidation()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("O campo {ProperpetyName} precisa ser preenchido")
            .Length(2, 6).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O campo {ProperpetyName} precisa ser preenchido")
            .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} de caracteres");
    }
}
