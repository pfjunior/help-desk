using FluentValidation;
using HD.Domain.Users.Entities;

namespace HD.Domain.Users.Validations;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
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
