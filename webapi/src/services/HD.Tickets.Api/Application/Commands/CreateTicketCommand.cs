using FluentValidation;
using HD.Core.Messages;
using HD.Tickets.Domain.Enum;

namespace HD.Tickets.Api.Application.Commands;

public class CreateTicketCommand : Command
{
    public Guid UserId { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public TicketType Type { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new CreateTicketValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class CreateTicketValidation : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketValidation()
    {
        RuleFor(t => t.UserId)
            .NotEmpty().WithMessage("User Id invalid");

        RuleFor(t => t.Subject)
            .NotEmpty().WithMessage("Subject is required")
            .Length(3, 100).WithMessage("Subject must have be between 3 and 100 characters");

        RuleFor(t => t.Description)
            .NotEmpty().WithMessage("Description is required")
            .Length(3, 100).WithMessage("Description must have be between 3 and 1000 characters");

        RuleFor(t => t.UserName)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(t => t.Email)
            .NotEmpty().WithMessage("Email is required");

        RuleFor(t => t.Department)
            .NotEmpty().WithMessage("Department is required");
    }
}