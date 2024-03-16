using FluentValidation;
using HD.Domain.Tickets.Entities;

namespace HD.Domain.Tickets.Validations;

public class TicketValidation : AbstractValidator<Ticket>
{
    public TicketValidation()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido")
            .Length(2, 500).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(p => p.Solution)
           .Length(2, 500).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(p => p.Priority)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido");
    }
}